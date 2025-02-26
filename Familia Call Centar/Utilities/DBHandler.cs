﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Familia_Call_Centar.Model;
using MySql;
using MySql.Data.MySqlClient;
using System.Data;

namespace Familia_Call_Centar.Utilities
{
    public class DBHandler
    {
        MySqlConnection connection;
        public DBHandler()
        {
            connection = new MySqlConnection(Res.connectionString);
        }

        public List<jelo> loadJela()
        {
            List<jelo> jela = new List<jelo>();
            try
            {
                connection.Open();
                String statement = "SELECT * FROM jelo";
                MySqlCommand cmd = new MySqlCommand(statement, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    jela.Add(new jelo(Convert.ToInt32(reader["jeloID"]), reader["naziv"].ToString(), reader["opis"].ToString(),
                        reader["tip_jela"].ToString(), Convert.ToDouble(reader["cijena"])));
                }
                reader.Dispose();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException);
            }
            finally
            {
                connection.Close();
            }
            return jela;
        }

        public List<narudzba> loadNarudzbe()
        {
            List<narudzba> narudzbe = new List<narudzba>();
            try
            {
                connection.Open();
                String statement = "SELECT * FROM narudzba";
                MySqlCommand cmd = new MySqlCommand(statement, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    narudzbe.Add(new narudzba(reader["ime_narucioca"].ToString(),
                        reader["prezime_narucioca"].ToString(),
                        reader["broj_telefona_narucioca"].ToString(),
                        reader["ime_firme"].ToString(),
                        reader["adresa_firme"].ToString()));
                    //ocekivano vrijeme isporuke ionako ne treba ni za sta
                }

                reader.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException);
            }
            finally
            {
                connection.Close();
            }
            return narudzbe;
        }

        public DataTable fillDataTable()
        {
            DataTable table = new DataTable();
            try
            {
                connection.Open();
                String statement = "SELECT DISTINCT ime_narucioca, prezime_narucioca, broj_telefona_narucioca, " +
                    "ime_firme, adresa_firme FROM testna.narudzba order by ocekivano_vrijeme_isporuke desc";
                MySqlCommand cmd = new MySqlCommand(statement, connection);
                using(MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException);
            }
            finally
            {
                connection.Close();
            }
            return table;
        }

        public DataTable fillDataTableNewOrders()
        {
            DataTable table = new DataTable();
            try
            {
                connection.Open();
                String statement = "select n.ime_narucioca, n.prezime_narucioca, n.broj_telefona_narucioca, " +
                                   "n.ime_firme, n.adresa_firme, n.ocekivano_vrijeme_isporuke, " +
                                   "sum(n_i.ukupna_cijena) " +
                                   "from narudzba n, narudzba_item n_i " +
                                   "where n.narudzbaID = n_i.narudzbaID and n.voznjaID = 2 and n.status_narudzbe = 0 " +
                                   "group by n_i.narudzbaID " +
                                   "order by n.adresa_firme, n.ocekivano_vrijeme_isporuke desc";

                MySqlCommand cmd = new MySqlCommand(statement, connection);
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException);
            }
            finally
            {
                connection.Close();
            }
            return table;
        }

        public void updateOcekivanoVrijemeIsporuke(DateTime ocekVrijeme, int narudzbaID)
        {
            try
            {
                connection.Open();
                String statement = "UPDATE narudzba SET ocekivano_vrijeme_isporuke = \"" + ocekVrijeme
                    + "\" where narudzbaID = " + narudzbaID;
                MySqlCommand cmd = new MySqlCommand(statement, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException);
            }
            finally
            {
                connection.Close();
            }
        }

        public List<string> loadJelaUrls()
        {
            List<String> jelaUrls = new List<string>();
            try
            {
                connection.Open();
                String statement = "SELECT url_slike_jela FROM jelo";
                MySqlCommand cmd = new MySqlCommand(statement, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    jelaUrls.Add(reader["url_slike_jela"].ToString());
                }

                reader.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException);
            }
            finally
            {
                connection.Close();
            }
            return jelaUrls;
        }

        public void loadVozilaUrls()
        {
            List<String> vozilaUrls = new List<string>();
            try
            {
                connection.Open();
                String statement = "SELECT url_slike_vozila FROM testna.vozilo";
                MySqlCommand cmd = new MySqlCommand(statement, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    vozilaUrls.Add(reader["url_slike_vozila"].ToString());
                }
                reader.Dispose();
                Res.kombiUri = vozilaUrls[0];
                Res.mopedUri = vozilaUrls[1];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException);
            }
            finally
            {
                connection.Close();
            }
        }

        public Int32 loadCount(List<int> ids)
        {
            Int32 count = 0;
            try
            {
                connection.Open();
                MySqlCommand countGetter = null;
                for (int i = 0; i < ids.Count; i++)
                {
                    String statement = "SELECT sum(n_i.kvantitet) as broj_jela" +
                                       " FROM narudzba_item n_i, narudzba n" +
                                       " where n_i.narudzbaID = n.narudzbaID and n.voznjaID = 2" +
                                       " and n.narudzbaID = " + ids[i];
                    countGetter = new MySqlCommand(statement, connection);
                    countGetter.CommandType = System.Data.CommandType.Text;
                    count += Convert.ToInt32(countGetter.ExecuteScalar());
                }
                countGetter.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException);
            }
            finally
            {
                connection.Close();
            }
            return count;
        }

        public Double loadPrice(List<int> ids)
        {
            Double price = 0.0;
            try
            {
                connection.Open();
                MySqlCommand priceGetter = null;
                for (int i = 0; i < ids.Count; i++)
                {
                    String statement = "SELECT sum(n_i.ukupna_cijena) as cijena" +
                                       " FROM narudzba_item n_i, narudzba n" +
                                       " where n_i.narudzbaID = n.narudzbaID and n.voznjaID = 2" +
                                       " and n.narudzbaID = " + ids[i];
                    priceGetter = new MySqlCommand(statement, connection);
                    priceGetter.CommandType = System.Data.CommandType.Text;
                    price += Convert.ToInt32(priceGetter.ExecuteScalar());
                }
                priceGetter.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException);
            }
            finally
            {
                connection.Close();
            }
            return price;
        }

        public int checkUserCredentials(int id, String pass)
        {
            int br = 0;
            try
            {
                connection.Open();
                String statement = "SELECT * FROM vozac where vozacID = " + id + " and passsword = \"" + pass + "\"";
                MySqlCommand cmd = new MySqlCommand(statement, connection);
                cmd.CommandType = System.Data.CommandType.Text;

                MySqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read()) br++;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException);
            }
            finally
            {
                connection.Close();
            }
            return br;
        }

        public Int32 getVoziloID(String tip)
        {
            Int32 idVozila = 0;
            try
            {
                connection.Open();
                String statement = "SELECT voziloID FROM vozilo where tip_vozila = \"" + tip + "\"";
                MySqlCommand cmd = new MySqlCommand(statement, connection);
                cmd.CommandType = System.Data.CommandType.Text;

                idVozila = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException);
            }
            finally
            {
                connection.Close();
            }
            return idVozila;
        }

        public DataTable getOrderIDs(DataTable narudzbe)
        {
            try
            {
                DataTable table = new DataTable();
                connection.Open();
                String statement = "select * from narudzba where voznjaID = 2";
                MySqlCommand cmd = new MySqlCommand(statement, connection);
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }

                for(int i = 0; i < narudzbe.Rows.Count; i++)
                {
                    for(int j = 0; j < table.Rows.Count; j++)
                    {
                        if (Convert.ToDateTime(narudzbe.Rows[i][5]).Equals(Convert.ToDateTime(table.Rows[j][6])))
                            narudzbe.Rows[i][7] = Convert.ToInt32(table.Rows[j][0]);
                    }
                }
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException);
            }
            finally
            {
                connection.Close();
            }
            return narudzbe;
        }

        public DataTable fillDataTableMeals(DataTable jela, DataTable narudzbe)
        {
            DataTable table = new DataTable();
            try
            {
                connection.Open();
                String statement = "select j.naziv, n_i.kvantitet, n.narudzbaID " +
                                   "from jelo j, narudzba n, narudzba_item n_i " +
                                   "where n.narudzbaID = n_i.narudzbaID and j.jeloID = n_i.jeloID and n.status_narudzbe = 0 " +
                                   "group by n_i.narudzbaID, n_i.jeloID " +
                                   "order by n.adresa_firme, n.ocekivano_vrijeme_isporuke desc";

                MySqlCommand cmd = new MySqlCommand(statement, connection);
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }

                for (int i = 0; i < narudzbe.Rows.Count; i++)
                {
                    for (int j = 0; j < table.Rows.Count; j++)
                    {
                        if (Convert.ToInt32(narudzbe.Rows[i][7]) == Convert.ToInt32(table.Rows[j][2]))
                        {
                            DataRow row = jela.NewRow();
                            row["naziv"] = table.Rows[j][0].ToString();
                            row["kvantitet"] = Convert.ToInt32(table.Rows[j][1]);
                            row["narudzbaID"] = Convert.ToInt32(table.Rows[j][2]);
                            jela.Rows.Add(row);
                        }
                    }
                }

                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException);
            }
            finally
            {
                connection.Close();
            }
            return jela;
        }

        public int authenticateAdmin(String password)
        {
            int br = 0;
            try
            {
                connection.Open();
                String statement = "SELECT * FROM roles where pass = \"" + password + "\"";
                MySqlCommand cmd = new MySqlCommand(statement, connection);
                cmd.CommandType = System.Data.CommandType.Text;

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) br++;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException);
            }
            finally
            {
                connection.Close();
            }
            return br;
        }

        public void deleteEntry(string tip, int id, string naziv)
        {
            String statement = null;
            if (tip.Equals("jelo")) statement = "Delete from jelo where naziv = \"" + naziv + "\"";
            else if (tip.Equals("vozac")) statement = "Delete from vozac where vozacID = " + id;
            else statement = "Delete from vozilo where voziloID = " + id;

            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(statement, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException);
            }
            finally
            {
                connection.Close();
            }
        }

        public void editJelo(string naziv, string poljeZaEdit, string vrijednost, Double cijena)
        {
            String statement = null;
            if (poljeZaEdit.Equals("cijena")) statement = "update jelo set  cijena = " + cijena + " where naziv = "
                     + " \"" + naziv + "\""; 
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(statement, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException);
            }
            finally
            {
                connection.Close();
            }
        }

        public int checkIfEntryExists(string tip, int id, string naziv)
        {
            int br = 0;
            String statement = null;
            if(tip.Equals("jelo")) statement = "SELECT * FROM jelo where naziv = \"" + naziv + "\"";
            else if(tip.Equals("vozac")) statement = "SELECT * FROM vozac where vozacID = " + id;
            else statement = "SELECT * FROM vozilo where voziloID = " + id;

            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(statement, connection);
                cmd.CommandType = System.Data.CommandType.Text;

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) br++;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException);
            }
            finally
            {
                connection.Close();
            }
            return br;
        }

        public void updateEntry(string tip, string path, int id, int status)
        {
            String statement = null;
            if (tip.Equals("jelo")) statement = "update jelo set url_slike_jela = \"" + path + "\" where jeloID = " + id;
            else if (tip.Equals("narudzba")) statement = "update narudzba set status_narudzbe = "
                    + status + " where narudzbaID = " + id;
            else if (tip.Equals("odgovorni_vozac")) statement = "update narudzba set odgovorni_vozac = "
                    + status + " where narudzbaID = " + id;
            else if (tip.Equals("izmjena_kolicina"))
            {
                statement =
                    "update narudzba_item n_i join jelo j on (j.jeloID = n_i.jeloID)" +
                    " set n_i.kvantitet = " + status + " , n_i.ukupna_cijena = j.cijena * " + status +
                    " where j.naziv = \"" + path + "\" and n_i.narudzbaID = " + id;
            }
            else if (tip.Equals("vrijeme_isporuke")) statement = "update narudzba set vrijeme_isporuke = \""
                     + path + "\" where narudzbaID = " + id;
            else statement = "update vozilo set url_slike_vozila = \"" + path + "\" where voziloID = " + id;

            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(statement, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException);
            }
            finally
            {
                connection.Close();
            }
        }

        public DataTable fillDataTableJelaForStorage()
        {
            DataTable table = new DataTable();
            try
            {
                connection.Open();
                String statement = "Select jeloID, naziv, cijena from jelo";
                MySqlCommand cmd = new MySqlCommand(statement, connection);
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException);
            }
            finally
            {
                connection.Close();
            }
            return table;
        }

        public void kreirajPredIsporuku(DataTable table)
        {
            String statement = null;
            try
            {
                connection.Open();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    statement = "insert into skladiste(datum, jeloID, kolicina) values"
                        + "(\"" + DateTime.Now.Day.ToString() + "." + DateTime.Now.Month.ToString()
                        + "." + DateTime.Now.Year.ToString() + "\", "
                        + table.Rows[i][0] + ", " + table.Rows[i][3] + ")";

                    MySqlCommand cmd = new MySqlCommand(statement, connection);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
