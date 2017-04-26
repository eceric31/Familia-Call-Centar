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
using System.Windows.Shapes;
using Familia_Call_Centar.Model;

namespace Familia_Call_Centar
{
    /// <summary>
    /// Interaction logic for OrderInfo.xaml
    /// </summary>
    public partial class OrderInfo : Window
    {
        narudzba narudzba;
        FamiliaContextClass db;
        public OrderInfo()
        {
            InitializeComponent();
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
               String.IsNullOrEmpty(vrijemeIspInput.Text) || String.IsNullOrEmpty(adresaInput.Text))
            {
                MessageBoxResult box = MessageBox.Show("Molimo unesite sve parametre!");
            }
            else
            {
                saveOrder();
            }
        }

        public void saveOrder()
        {
            narudzba = new narudzba();
            narudzba.ime_narucioca = imeInput.Text;
            narudzba.prezime_narucioca = prezimeInput.Text;
            narudzba.broj_telefona_narucioca = brTelInput.Text;
            narudzba.ime_firme = firmaInput.Text;
            narudzba.adresa_firme = adresaInput.Text;
            try
            {
                narudzba.ocekivano_vrijeme_isporuke = Convert.ToDateTime(vrijemeIspInput.Text);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                MessageBoxResult box = MessageBox.Show("Unijeli ste pogrešan vremenski format!");
                vrijemeIspInput.Clear();
            }
            db.narudzba.Add(narudzba);
            try
            {
                db.SaveChangesAsync();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void clearInput()
        {
            imeInput.Clear();
            prezimeInput.Clear();
            brTelInput.Clear();
            firmaInput.Clear();
            adresaInput.Clear();
            vrijemeIspInput.Clear();
        }
    }
}
