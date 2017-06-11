using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Familia_Call_Centar.Utilities;
using System.Collections.Specialized;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Familia_Call_Centar.Servis
{
    public class Service 
    {
        DBHandler handler;
        HttpListener listener;
        DataTable narudzbe;
        DataTable jela;

        int idVozila;
        String tipVozila;

        public Service()
        {
            handler = new DBHandler();
            Narudzbe = new DataTable();
            Jela = new DataTable();

            String prefix = @"http://*:5000/";
            listener = new HttpListener();
            listener.Prefixes.Clear();
            listener.Prefixes.Add(prefix);
        }

        public async void listen()
        {
            try {
                listener.Start();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException);
            }
            while (true)
            {
                HttpListenerContext context = await listener.GetContextAsync();
                String reqData = getRequestData(context);
                string[] reqValues = reqData.Split(' ');

                //if bogus data is sent
                if (reqData == null) continue;
                //if login request
                else if (reqValues[0].Equals("login"))
                    await Task.Factory.StartNew(() => handleLogin(context, Convert.ToInt32(reqValues[1]), reqValues[2]));
                else
                {
                    //if nothing to send
                    if (reqValues[0].Equals("isporuka") && (String.IsNullOrEmpty(TipVozila) || narudzbe.Rows.Count == 0))
                        await Task.Factory.StartNew(() => handleEmptyData(context));
                    else if (reqValues[0].Equals("isporuka") && !(String.IsNullOrEmpty(TipVozila) || narudzbe.Rows.Count == 0))
                        await Task.Factory.StartNew(() => handleIsporuka(context));
                    else if (reqValues[0].Equals("otkazano"))
                        await Task.Factory.StartNew(() => handleDelivery(context, "otkazano", 
                            Convert.ToInt32(reqValues[1]), 3, Convert.ToInt32(reqValues[2])));
                    else if (reqValues[0].Equals("izmijenjeno"))
                        await Task.Factory.StartNew(() => handleDelivery(context, "izmijenjeno", 
                            Convert.ToInt32(reqValues[1]), 2, Convert.ToInt32(reqValues[2])));
                    else if (reqValues[0].Equals("dostavljeno"))
                        await Task.Factory.StartNew(() => handleDelivery(context, "dostavljeno", -1, 2, 
                            Convert.ToInt32(reqValues[1])));
                }
            }
        }

        private string getRequestData(HttpListenerContext ctx)
        {
            String requestData = null;
            //read header
            NameValueCollection headers = ctx.Request.QueryString;
            List<String> values = new List<String>();
            for (int i = 0; i < headers.Count; i++)
            {
                values.Add(headers.Get(headers.GetKey(i)));
            }

            if (values.Contains("login")) requestData = values[0] + " " + values[1] + " " + values[2];
            else if (values.Contains("isporuka")) requestData = values[0] + " " + values[1];
            else if (values.Contains("otkazano") || values.Contains("izmijenjeno"))
                requestData = values[0] + " " + values[1] + " " + values[2];
            else if (values.Contains("dostavljeno")) requestData = values[0] + " " + values[1];

            Console.WriteLine("Url parameters: " + requestData);

            return requestData;
        }

        private async void handleDelivery(HttpListenerContext ctx, string type, 
            int deliveryID, int deliveryStatus, int driverID)
        {
            try
            {
                if (type.Equals("otkazano"))
                {
                    handler.updateEntry("narudzba", null, deliveryID, deliveryStatus);
                }
                else
                {
                    var requestContent = ctx.Request.InputStream;
                    string body = null;
                    using (StreamReader reader = new StreamReader(requestContent, Encoding.BigEndianUnicode, true, 1024, true))
                    {
                        body = reader.ReadToEnd();
                    }

                    if (type.Equals("izmijenjeno"))
                    {
                        JObject json = JObject.Parse(body);
                        List<JProperty> jsonJela = json.Properties().ToList();
                        List<JToken> jsonKolicine = json.PropertyValues().ToList();
                        for(int i = 0; i < jsonJela.Count(); i++)
                        {
                            handler.updateEntry("izmjena_kolicina",
                                 jsonJela[i].Name, deliveryID, jsonKolicine[i].Value<int>());
                        }
                    }
                    else
                    {
                        JArray json = JArray.Parse(body);
                        List<JToken> vals = json.Children().ToList();
                        foreach(var val in vals)
                        {
                            handler.updateEntry("narudzba", null, Convert.ToInt32(val), deliveryStatus);
                            handler.updateEntry("odgovorni_vozac", null, Convert.ToInt32(val), driverID);
                            handler.updateEntry("vrijeme_isporuke", getDateTime(), Convert.ToInt32(val), -1);
                        }
                    }
                }
                handler.updateEntry("odgovorni_vozac", null, deliveryID, driverID);

                var output = ctx.Response.OutputStream;
                ctx.Response.StatusCode = 200;
                byte[] empty = new byte[0];
                await output.WriteAsync(empty, 0, 0);
                await output.FlushAsync();
                output.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException);
            }
        }

        private async void handleEmptyData(HttpListenerContext ctx)
        {
            try
            {
                var output = ctx.Response.OutputStream;
                ctx.Response.StatusCode = 404;
                byte[] empty = new byte[0];
                await output.WriteAsync(empty, 0, 0);
                await output.FlushAsync();
                output.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException);
            }
        }

        private async void handleLogin(HttpListenerContext ctx, int uID, String pass)
        {
            if(handler.checkUserCredentials(uID, pass) == 1) ctx.Response.StatusCode = 200;
            else ctx.Response.StatusCode = 404;
            try
            {
                var output = ctx.Response.OutputStream;
                //just return the reponse code
                byte[] empty = new byte[0];
                await output.WriteAsync(empty, 0, 0);
                await output.FlushAsync();
                output.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException);
            }
        }

        private async void handleIsporuka(HttpListenerContext ctx)
        {
            try
            {
                var output = ctx.Response.OutputStream;
                byte[] json = Encoding.BigEndianUnicode.GetBytes(toJson());
                await output.WriteAsync(json, 0, json.Length);
                await output.FlushAsync();
                output.Dispose();
                clearValues();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException);
            }
        }

        public DataTable Narudzbe
        {
            get
            {
                return narudzbe;
            }

            set
            {
                narudzbe = value;
            }
        }

        public int IdVozila
        {
            get
            {
                return idVozila;
            }

            set
            {
                idVozila = value;
            }
        }

        public string TipVozila
        {
            get
            {
                return tipVozila;
            }

            set
            {
                tipVozila = value;
            }
        }

        public DataTable Jela
        {
            get
            {
                return jela;
            }

            set
            {
                jela = value;
            }
        }
        
        private String toJson()
        {
            StringBuilder json = new StringBuilder();

            json.Append("{\n");
            json.Append("\"id_vozila\": \"" + idVozila.ToString() + "\"");
            json.Append(",\n\"tip_vozila\": \"" + tipVozila + "\"");
            json.Append(",\n\"narudzbe\": [\n");

            for (int i = 0; i < narudzbe.Rows.Count; i++)
            {
                json.Append("{\n");
                json.Append("\"id_narudzbe\": \"" + narudzbe.Rows[i][7].ToString() + "\",\n");
                json.Append("\"ime_narucioca\": \"" + narudzbe.Rows[i][0].ToString() + "\",\n");
                json.Append("\"prezime_narucioca\": \"" + narudzbe.Rows[i][1].ToString() + "\",\n");
                json.Append("\"broj_telefona\": \"" + narudzbe.Rows[i][2].ToString() + "\",\n");
                json.Append("\"firma\": \"" + narudzbe.Rows[i][3].ToString() + "\",\n");
                json.Append("\"adresa\": \"" + narudzbe.Rows[i][4].ToString() + "\",\n");
                json.Append("\"ocekivano_vrijeme_isporuke\": \"" + narudzbe.Rows[i][5].ToString() + "\",\n");
                json.Append("\"jela\":[\n");

                for (int j = 0; j < jela.Rows.Count; j++)
                {
                    if (Convert.ToInt32(jela.Rows[j][2]).Equals(Convert.ToInt32(narudzbe.Rows[i][7].ToString())))
                    {
                        json.Append("{\n");

                        json.Append("\"naziv_jela\": \"" + jela.Rows[j][0].ToString() + "\",\n");
                        json.Append("\"kvantitet\": \"" + jela.Rows[j][1].ToString() + "\"\n");
                        if ((j == jela.Rows.Count - 1) || 
                            (!Convert.ToInt32(jela.Rows[j][2]).Equals(Convert.ToInt32(jela.Rows[j + 1][2]))))
                        {
                            json.Append("}\n");
                        }
                        else json.Append("},\n");
                    }
                }
                if(i != narudzbe.Rows.Count - 1) json.Append("],\n\"cijena\": \"" + narudzbe.Rows[i][6].ToString() + "\"\n},");
                else json.Append("],\n\"cijena\": \"" + narudzbe.Rows[i][6].ToString() + "\"\n}\n]\n}");
            }
            return json.ToString();
        }

        private void clearValues()
        {
            idVozila = 0;
            tipVozila = null;
            narudzbe.Clear();
            jela.Clear();
        }

        private String getDateTime()
        {
            return new DateTime(DateTime.Now.Year,
                DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 
                DateTime.Now.Minute, 0, DateTimeKind.Unspecified).ToString();
        }
    }
}
