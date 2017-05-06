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
                String statement = "SELECT * FROM testna.jelo";
                MySqlCommand cmd = new MySqlCommand(statement, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    jela.Add(new jelo(Convert.ToInt32(reader["jeloID"]), reader["naziv"].ToString(), reader["opis"].ToString(),
                        reader["tip_jela"].ToString(), Convert.ToDouble(reader["cijena"])));
                }
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
                String statement = "SELECT ime_narucioca, prezime_narucioca, broj_telefona_narucioca, " +
                    "ime_firme, adresa_firme FROM testna.narudzba order by ocekivano_vrijeme_isporuke desc";
                MySqlCommand cmd = new MySqlCommand(statement, connection);
                using(MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
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
            return table;
        }
    }
}
