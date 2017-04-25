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
using Familia_Call_Centar.Controller;

namespace Familia_Call_Centar
{
    /// <summary>
    /// Interaction logic for OrderInfo.xaml
    /// </summary>
    public partial class OrderInfo : Window
    {
        OrderController oc;
        public OrderInfo()
        {
            InitializeComponent();
            oc = new OrderController();
        }        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource narudzbaViewSource = 
                ((System.Windows.Data.CollectionViewSource)(this.FindResource("narudzbaViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // narudzbaViewSource.Source = [generic data source]
        }

        private void clearClick(object sender, RoutedEventArgs e)
        {
            this.imeInput.Text = "";
            this.prezimeInput.Text = "";
            this.brTelInput.Text = "";
            this.firmaInput.Text = "";
            this.adresaInput.Text = "";
            this.vrijemeIspInput.Text = "";
        }

        private void daljeClick(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(imeInput.Text) || String.IsNullOrEmpty(prezimeInput.Text) ||
               String.IsNullOrEmpty(brTelInput.Text) || String.IsNullOrEmpty(firmaInput.Text) ||
               String.IsNullOrEmpty(vrijemeIspInput.Text) || String.IsNullOrEmpty(adresaInput.Text))
            {
                MessageBoxResult box = MessageBox.Show("Molimo unesite sve parametre!!!");
            }
            else
            {
                oc.narudzba.ime_narucioca = imeInput.Text;
                oc.narudzba.prezime_narucioca = prezimeInput.Text;
                oc.narudzba.broj_telefona_narucioca = brTelInput.Text;
                oc.narudzba.ime_firme = firmaInput.Text;
                oc.narudzba.adresa_firme = adresaInput.Text;
                oc.narudzba.ocekivano_vrijeme_isporuke = Convert.ToDateTime(vrijemeIspInput.Text);
                oc.saveOrder();
            }
        }
    }
}
