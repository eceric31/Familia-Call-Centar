using System;
using System.Collections.Generic;
using System.Data;
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
using Familia_Call_Centar.Utilities;
using Familia_Call_Centar.Servis;

namespace Familia_Call_Centar.View
{
    /// <summary>
    /// Interaction logic for TransportPick.xaml
    /// </summary>
    public partial class TransportPick : Page
    {
        Service service;
        DataTable isporukaTable;
        DBHandler handler;
        String selectedTransport;
        public TransportPick(DataTable isporuka, Service service)
        {
            InitializeComponent();
            isporukaTable = isporuka;

            this.service = service;
            handler = new DBHandler();
            handler.loadVozilaUrls();
            kombiImage.Source = Res.addKombiImage();
            mopedImage.Source = Res.addMopedImage();

            isporukaDataGrid.DataContext = isporukaTable.DefaultView;

            kolicinaTB.Text = "Za isporuku " + handler.loadCount().ToString() +
                              " jela, čija je ukupna cijena " + handler.loadPrice().ToString() + " KM";
        }

        private void nazadButton_Click(object sender, RoutedEventArgs e)
        {
            Page newDelivery = new NewDelivery(service);
            NavigationService.Navigate(newDelivery);
        }

        private void zavrsiButton_Click(object sender, RoutedEventArgs e)
        {
            if(String.IsNullOrEmpty(selectedTransport))
            {
                MessageBox.Show("Molimo odaberite način transporta dostave!");
            }
            else
            {
                service.Narudzbe = isporukaTable;
                service.IdVozila = handler.getVoziloID(selectedTransport);
                service.TipVozila = selectedTransport;
                //trebaju jos narudzba_itemsi
                Page dash = new Dashboard(service);
                NavigationService.Navigate(dash);
            }
        }

        private void kombiImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            selectedTransport = "Kombi";
        }

        private void mopedImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            selectedTransport = "Moped";
        }
    }
}
