using System;
using Familia_Call_Centar.Model;
using System.ComponentModel;

namespace Familia_Call_Centar.Controller
{
    public class OrderController
    {
        narudzba narudzba;

        public narudzba Narudzba
        {
            get
            {
                return narudzba;
            }

            set
            {
                narudzba = value;
            }
        }

        public OrderController()
        {
            narudzba = new narudzba();
        }

        public void saveOrder()
        {
            using (var db = new testnaEntities())
            {
                db.narudzba.Add(narudzba);
                Console.WriteLine("Uneseni su sljedeci podaci: ");
                Console.WriteLine(narudzba.ime_narucioca);
                Console.WriteLine(narudzba.prezime_narucioca);
                Console.WriteLine(narudzba.ime_firme);
                Console.WriteLine(narudzba.adresa_firme);
                Console.WriteLine(narudzba.broj_telefona_narucioca);
                Console.WriteLine(narudzba.ocekivano_vrijeme_isporuke);
                try
                {
                    db.SaveChanges();
                }
                catch(System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }
    }
}
