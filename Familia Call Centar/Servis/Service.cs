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

namespace Familia_Call_Centar.Servis
{
    public class Service 
    {
        DBHandler handler;
        HttpListener listener;
        DataTable narudzbe;

        int idVozila;
        String tipVozila;

        public Service()
        {
            handler = new DBHandler();
            listener = new HttpListener();
            Narudzbe = new DataTable();

            listener.Start();
            listen();
        }

        private async void listen()
        {
            while(true)
            {
                HttpListenerContext context = await listener.GetContextAsync();
                String reqData = getRequestData(context);
                string[] reqValues = reqData.Split(' ');

                //if bogus data is sent
                if (reqData == null) continue;
                //if nothing to send
                else if (String.IsNullOrEmpty(TipVozila) || narudzbe.Rows.Count == 0)
                    await Task.Factory.StartNew(() => handleEmptyData(context));
                else
                {
                    if (reqValues[0].Equals("login"))
                        await Task.Factory.StartNew(() => handleLogin(context, Convert.ToInt32(reqValues[1]), reqValues[2]));
                    else if (reqValues[0].Equals("isporuka"))
                        await Task.Factory.StartNew(() => handleIsporuka(context));
                }
            }
        }

        private string getRequestData(HttpListenerContext ctx)
        {
            String requestData = null;
            
            //read header
            NameValueCollection headers = ctx.Request.Headers;
            List<String> keys = new List<String>();
            List<String> values = new List<String>();
            for (int i = 0; i < headers.Count; i++)
            {
                keys.Add(headers.GetKey(i));
                values.Add(headers.Get(headers.GetKey(i)));
            }

            if (values.Contains("login")) requestData = values[0] + " " + values[1] + " " + values[2];
            else if (values.Contains("isporuka")) requestData = values[0] + " " + values[1];

            Console.WriteLine(requestData);

            return requestData;
        }

        private async void handleEmptyData(HttpListenerContext ctx)
        {
            try
            {
                var output = ctx.Response.OutputStream;
                ctx.Response.StatusCode = 404;
                byte[] empty = new byte[0];
                await output.WriteAsync(empty, 0, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException);
            }
        }

        private async void handleLogin(HttpListenerContext ctx, int uID, String pass)
        {
            if(handler.checkUserCredentials(uID, pass)) ctx.Response.StatusCode = 200;
            else ctx.Response.StatusCode = 404;
            try
            {
                var output = ctx.Response.OutputStream;
                //just return the reponse code
                byte[] empty = new byte[0];
                await output.WriteAsync(empty, 0, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException);
            }
        }

        private async void handleIsporuka(HttpListenerContext ctx)
        {

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
    }
}
