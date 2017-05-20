using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Familia_Call_Centar.Model;
using System.Diagnostics;
using System.Data;
using Familia_Call_Centar.Utilities;
using System.Globalization;
using Familia_Call_Centar.Servis;

namespace Familia_Call_Centar.View
{
    /// <summary>
    /// Interaction logic for OrderInfo.xaml
    /// </summary>
    public partial class OrderInfo : Page
    {
        String ime, prezime, brTel, firma, adresa;
        bool dataRowSelected = false;

        narudzba narudzba;
        FamiliaContextClass db;
        DBHandler handler;
        Service service;
        public OrderInfo(Service service)
        {
            InitializeComponent();
            this.service = service;
            String date = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" +
                DateTime.Now.Year.ToString();
            vrijemeIspInput.Text = date;
            db = new FamiliaContextClass();
            handler = new DBHandler();

            DataTable table = handler.fillDataTable();
            narudzbaDataGrid.DataContext = table.DefaultView;
        }

        private void clearClick(object sender, RoutedEventArgs e)
        {
            clearInput();
        }

        private void daljeClick(object sender, RoutedEventArgs e)
        {
            if (!dataRowSelected)
            {
                if (String.IsNullOrEmpty(imeInput.Text) || String.IsNullOrEmpty(prezimeInput.Text) ||
                   String.IsNullOrEmpty(brTelInput.Text) || String.IsNullOrEmpty(firmaInput.Text) ||
                   String.IsNullOrEmpty(adresaInput.Text))
                {
                    MessageBoxResult box = MessageBox.Show("Molimo unesite sve parametre!");
                }
                else
                {
                    save();
                }
            }
            else
            {
                save();
            }
        }

        private void save()
        {
            if(dataRowSelected)
            {
                narudzba = new narudzba(ime, prezime, brTel, firma, adresa, getDateTime());
                dataRowSelected = false;
            }
            else
            {
                narudzba = new narudzba(imeInput.Text, prezimeInput.Text, brTelInput.Text, firmaInput.Text, adresaInput.Text,
                    getDateTime());
                
            }
            saveOrder();
            Page foodPicker = new FoodPick(narudzba.narudzbaID, service);
            NavigationService.Navigate(foodPicker);
        }

        private void nazadButton_Click(object sender, RoutedEventArgs e)
        {
            Page dash = new Dashboard(service);
            NavigationService.Navigate(dash);
        }

        private void saveOrder()
        {
            db.narudzba.Add(narudzba);
            try
            {
                db.SaveChanges();
                handler.updateOcekivanoVrijemeIsporuke(getDateTime(), narudzba.narudzbaID);
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
            podaciONaruciocu.Content = "Podaci o naručiocu";
            prezimeLabel.Content = "";
            brTelLabel.Content = "";
            firmaLabel.Content = "";
            adresaLabel.Content = "";
            vrijemeLabel.Content = "";
        }

        private void narudzbaDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataTable table = new DataTable();
            table = ((DataView)narudzbaDataGrid.ItemsSource).ToTable();
            var dg = sender as DataGrid;
            var index = dg.SelectedIndex;
            
            ime = table.Rows[index][0].ToString();
            prezime = table.Rows[index][1].ToString();
            brTel = table.Rows[index][2].ToString();
            firma = table.Rows[index][3].ToString();
            adresa = table.Rows[index][4].ToString();
            dataRowSelected = true;

            imeInput.Text = ime;
            prezimeInput.Text = prezime;
            brTelInput.Text = brTel;
            firmaInput.Text = firma;
            adresaInput.Text = adresa;

            MessageBox.Show("Molimo unesite očekivano vrijeme isporuke");
        }

        private DateTime getDateTime()
        {
            String time = vrijemeIspInput.Text;
            string[] dateTimeSplit = time.Split();
            string[] timeSplit = dateTimeSplit[1].Split(':');
            int hours = Convert.ToInt32(timeSplit[0]);
            int minutes = Convert.ToInt32(timeSplit[1]);
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hours, minutes, 0, DateTimeKind.Unspecified);
        }

        private void imeInput_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            podaciONaruciocu.Content = "Ime naručioca";
        }

        private void prezimeInput_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            prezimeLabel.Content = "Prezime naručioca";
        }

        private void brTelInput_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            brTelLabel.Content = "Broj telefona";
        }

        private void firmaInput_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            firmaLabel.Content = "Naziv firme";
        }

        private void adresaInput_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            adresaLabel.Content = "Adresa firme";
        }

        private void vrijemeIspInput_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            vrijemeLabel.Content = "Očekivano vrijeme isporuke";
        }
    }
}
