using System;
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
                String statement = "SELECT * FROM testna.jelo";
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
                String statement = "SELECT * FROM testna.narudzba";
                MySqlCommand cmd = new MySqlCommand(statement, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    narudzbe.Add(new narudzba(reader["ime_narucioca"].ToString(),
                        reader["prezime_narucioca"].ToString(),
                        reader["broj_telefona_narucioca"].ToString(),
                        reader["ime_firme"].ToString(),
                        reader["adresa_firme"].ToString(), DateTime.Now));
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
                                   "where n.narudzbaID = n_i.narudzbaID and n.voznjaID = 2 " +
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
                String statement = "UPDATE testna.narudzba SET ocekivano_vrijeme_isporuke = (@1)"
                    + " where narudzbaID = " + narudzbaID;
                MySqlCommand cmd = new MySqlCommand(statement, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@1", ocekVrijeme);
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

        public void loadJelaUrls()
        {
            List<String> jelaUrls = new List<string>();
            try
            {
                connection.Open();
                String statement = "SELECT url_slike_jela FROM testna.jelo";
                MySqlCommand cmd = new MySqlCommand(statement, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    jelaUrls.Add(reader["url_slike_jela"].ToString());
                }

                reader.Dispose();
                Res.grahUri = jelaUrls[0];
                Res.kobasiceUri = jelaUrls[1];
                Res.sarmaUri = jelaUrls[2];
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

        public Int32 loadCount()
        {
            Int32 count = 0;
            try
            {
                connection.Open();
                String statement = "SELECT sum(n_i.kvantitet) as broj_jela" + 
                                   " FROM narudzba_item n_i, narudzba n" +
                                   " where n_i.narudzbaID = n.narudzbaID and n.voznjaID = 2";
                MySqlCommand countGetter = new MySqlCommand(statement, connection);
                countGetter.CommandType = System.Data.CommandType.Text;
                count = Convert.ToInt32(countGetter.ExecuteScalar());
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

        public Double loadPrice()
        {
            Double price = 0.0;
            try
            {
                connection.Open();
                String statement = "SELECT sum(n_i.ukupna_cijena) as cijena" +
                                   " FROM narudzba_item n_i, narudzba n" +
                                   " where n_i.narudzbaID = n.narudzbaID and n.voznjaID = 2";
                MySqlCommand priceGetter = new MySqlCommand(statement, connection);
                priceGetter.CommandType = System.Data.CommandType.Text;
                price = Convert.ToDouble(priceGetter.ExecuteScalar());
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
                                   "where n.narudzbaID = n_i.narudzbaID and j.jeloID = n_i.jeloID and n.voznjaID = 2 " +
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
    }
}
