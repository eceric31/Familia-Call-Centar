using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Familia_Call_Centar.Model;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Familia_Call_Centar.View
{
    /// <summary>
    /// Interaction logic for OrderInfo.xaml
    /// </summary>
    public partial class OrderInfo : Page
    {
        narudzba narudzba;
        FamiliaContextClass db;
        public OrderInfo()
        {
            InitializeComponent();
            vrijemeIspInput.Text = DateTime.Now.ToString();
            narudzba = new narudzba();
            db = new FamiliaContextClass();
        }

        private void clearClick(object sender, RoutedEventArgs e)
        {
            clearInput();
        }

        private void daljeClick(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(imeInput.Text) || String.IsNullOrEmpty(prezimeInput.Text) ||
               String.IsNullOrEmpty(brTelInput.Text) || String.IsNullOrEmpty(firmaInput.Text) ||
               String.IsNullOrEmpty(adresaInput.Text))
            {
                MessageBoxResult box = MessageBox.Show("Molimo unesite sve parametre!");
            }
            else
            {
                narudzba.ime_narucioca = imeInput.Text;
                narudzba.prezime_narucioca = prezimeInput.Text;
                narudzba.broj_telefona_narucioca = brTelInput.Text;
                narudzba.ime_firme = firmaInput.Text;
                narudzba.adresa_firme = adresaInput.Text;
                narudzba.ocekivano_vrijeme_isporuke = (DateTime)vrijemeIspInput.SelectedDate;
                //ovo je id testnog entrya, tako da svaka narudzba koja nije isporucena ce imati ovaj id, sve dok se 
                //ne kreira nova voznja
                narudzba.voznjaID = 2;
                saveOrder();
                Page foodPicker = new FoodPick(narudzba.narudzbaID);
                NavigationService.Navigate(foodPicker);
            }
        }

        public void saveOrder()
        {
            db.narudzba.Add(narudzba);
            try
            {
                //db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                Console.WriteLine(e.StackTrace);
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine(@"Entity of type " + eve.Entry.Entity.GetType().Name
                        + " in state " + eve.Entry.State + " has the following validation errors: ");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException);
            }
        }

        private void clearInput()
        {
            imeInput.Clear();
            prezimeInput.Clear();
            brTelInput.Clear();
            firmaInput.Clear();
            adresaInput.Clear();
        }
    }
}
