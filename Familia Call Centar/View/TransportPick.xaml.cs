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

        DataTable narudzbe;
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

            narudzbe = isporukaTable;
            narudzbe.Columns.Add("orderID", typeof(int));
            narudzbe = handler.getOrderIDs(narudzbe);

            List<int> ids = new List<int>();
            for (int i = 0; i < narudzbe.Rows.Count; i++)
                ids.Add(Convert.ToInt32(narudzbe.Rows[i][7]));

            narudzbe.Columns.Remove(narudzbe.Columns[7]);

            kolicinaTB.Content = "Za isporuku " + handler.loadCount(ids).ToString() +
                              " jela, čija je ukupna cijena " + handler.loadPrice(ids).ToString() + " KM";
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
                service.IdVozila = handler.getVoziloID(selectedTransport);
                service.TipVozila = selectedTransport;

                narudzbe.Columns.Add("orderID", typeof(int));
                narudzbe = handler.getOrderIDs(narudzbe);

                DataTable jela = new DataTable();
                jela.Columns.Add("naziv", typeof(string));
                jela.Columns.Add("kvantitet", typeof(int));
                jela.Columns.Add("narudzbaID", typeof(int));
                jela = handler.fillDataTableMeals(jela, narudzbe);
                
                service.Jela = jela;
                service.Narudzbe = isporukaTable;
                for(int i = 0; i < isporukaTable.Rows.Count; i++)
                    handler.updateEntry("narudzba", null, Convert.ToInt32(isporukaTable.Rows[i][7]), 1);

                Page dash = new Dashboard(service);
                NavigationService.Navigate(dash);
            }
        }

        private void kombiImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(!String.IsNullOrEmpty(selectedTransport) && selectedTransport.Equals("Moped"))
            {
                selectedTransport = null;
                mopedLabel.Foreground = Brushes.Black;
                mopedLabel.FontWeight = FontWeights.Thin;
            }
            selectedTransport = "Kombi";
            kombiLabel.Foreground = Brushes.DeepSkyBlue;
            kombiLabel.FontWeight = FontWeights.Bold;
        }

        private void mopedImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!String.IsNullOrEmpty(selectedTransport) && selectedTransport.Equals("Kombi"))
            {
                selectedTransport = null;
                kombiLabel.Foreground = Brushes.Black;
                kombiLabel.FontWeight = FontWeights.Thin;
            }
            selectedTransport = "Moped";
            mopedLabel.Foreground = Brushes.DeepSkyBlue;
            mopedLabel.FontWeight = FontWeights.Bold;
        }
    }
}
