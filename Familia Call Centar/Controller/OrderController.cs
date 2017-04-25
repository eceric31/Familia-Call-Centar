using System;
using Familia_Call_Centar.Model;

namespace Familia_Call_Centar.Controller
{
    public class OrderController
    {
        public narudzba narudzba { get; set; }
        testnaEntities db { get; set; }

        public OrderController()
        {
            narudzba = new narudzba();
            db = new testnaEntities();
        }

        public void saveOrder()
        {
            if (narudzba.ime_narucioca != "")
            {
                Console.WriteLine("Spaseno u bazu...");
                db.narudzba.Add(narudzba);
            }
            try
            {
                db.SaveChangesAsync();
            }
            catch(System.Data.Entity.Validation.DbEntityValidationException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
