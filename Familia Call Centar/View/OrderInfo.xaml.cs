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
                narudzba = new narudzba();
                narudzba.ime_narucioca = imeInput.Text;
                narudzba.prezime_narucioca = prezimeInput.Text;
                narudzba.broj_telefona_narucioca = brTelInput.Text;
                narudzba.ime_firme = firmaInput.Text;
                narudzba.adresa_firme = adresaInput.Text;
                narudzba.ocekivano_vrijeme_isporuke = (DateTime)vrijemeIspInput.SelectedDate;
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
                db.SaveChanges();
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
        }
    }
}
