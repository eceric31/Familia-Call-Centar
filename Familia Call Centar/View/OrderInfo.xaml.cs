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
using System.Threading.Tasks;
using System.ComponentModel;

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
        DataTable _table;
        
        public OrderInfo(Service service)
        {
            InitializeComponent();
            this.service = service;
            String date = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" +
                DateTime.Now.Year.ToString();
            db = new FamiliaContextClass();
            handler = new DBHandler();

            _table = handler.fillDataTable();
            _table.Columns[0].ColumnName = "Ime naručioca";
            _table.Columns[1].ColumnName = "Prezime naručioca";
            _table.Columns[2].ColumnName = "Broj telefona";
            _table.Columns[3].ColumnName = "Naziv firme";
            _table.Columns[4].ColumnName = "Adresa firme";
            narudzbaDataGrid.DataContext = _table.DefaultView;

            addComboBoxValues(date);
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
            combobox.Text = "";

            podaciONaruciocu.Content = "Podaci o naručiocu";
            prezimeLabel.Content = "";
            brTelLabel.Content = "";
            firmaLabel.Content = "";
            adresaLabel.Content = "";
            vrijemeLabel.Content = "";

            dataRowSelected = false;
            narudzbaDataGrid.UnselectAllCells();
            narudzbaDataGrid.DataContext = _table.DefaultView;
        }

        private void narudzbaDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataTable table = new DataTable();
            table = ((DataView)narudzbaDataGrid.ItemsSource).ToTable();
            var dg = sender as DataGrid;
            var index = dg.SelectedIndex;

            //ima neki bug sa ovim selektovanjem
            bool bug = false;
            if (index == -1)
            {
                index = 0;
                imeInput.Clear();
                prezimeInput.Clear();
                brTelInput.Clear();
                firmaInput.Clear();
                adresaInput.Clear();
                bug = true;
            }
            if (!bug)
            {
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
        }

        private void imeInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable tmp = new DataTable();
            tmp.Columns.Add("Ime naručioca", typeof(string));
            tmp.Columns.Add("Prezime naručioca", typeof(string));
            tmp.Columns.Add("Broj telefona", typeof(string));
            tmp.Columns.Add("Naziv firme", typeof(string));
            tmp.Columns.Add("Adresa firme", typeof(string));
            String input = imeInput.Text;

            for (int i = 0; i < _table.Rows.Count; i++)
            {
                if (_table.Rows[i][0].ToString().Contains(input))
                {
                    DataRow row = tmp.NewRow();
                    row["Ime naručioca"] = _table.Rows[i][0].ToString();
                    row["Prezime naručioca"] = _table.Rows[i][1].ToString();
                    row["Broj telefona"] = _table.Rows[i][2].ToString();
                    row["Naziv firme"] = _table.Rows[i][3].ToString();
                    row["Adresa firme"] = _table.Rows[i][4].ToString();
                    tmp.Rows.Add(row);
                }
            }
            if(!dataRowSelected)
                narudzbaDataGrid.DataContext = tmp.DefaultView;
        }

        private DateTime getDateTime()
        {
            string[] timeSplit = combobox.Text.Split()[1].Split(':');
            int hours = Convert.ToInt32(timeSplit[0]);
            int minutes = Convert.ToInt32(timeSplit[1]);
            return new DateTime(DateTime.Now.Year, 
                DateTime.Now.Month, DateTime.Now.Day, hours, minutes, 0, DateTimeKind.Unspecified);
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

        private void addComboBoxValues(String date)
        {
            combobox.Items.Add(date + " " + "09:00");
            combobox.Items.Add(date + " " + "09:15");
            combobox.Items.Add(date + " " + "09:30");
            combobox.Items.Add(date + " " + "09:45");
            combobox.Items.Add(date + " " + "10:00");
            combobox.Items.Add(date + " " + "10:15");
            combobox.Items.Add(date + " " + "10:30");
            combobox.Items.Add(date + " " + "10:45");
            combobox.Items.Add(date + " " + "11:00");
            combobox.Items.Add(date + " " + "11:15");
            combobox.Items.Add(date + " " + "11:30");
            combobox.Items.Add(date + " " + "11:45");
            combobox.Items.Add(date + " " + "12:00");
            combobox.Items.Add(date + " " + "12:15");
            combobox.Items.Add(date + " " + "12:30");
            combobox.Items.Add(date + " " + "12:45");
            combobox.Items.Add(date + " " + "13:00");
            combobox.Items.Add(date + " " + "12:15");
            combobox.Items.Add(date + " " + "12:30");
            combobox.Items.Add(date + " " + "12:45");
            combobox.Items.Add(date + " " + "13:00");
            combobox.Items.Add(date + " " + "13:15");
            combobox.Items.Add(date + " " + "13:30");
            combobox.Items.Add(date + " " + "13:45");
            combobox.Items.Add(date + " " + "14:00");
            combobox.Items.Add(date + " " + "14:15");
            combobox.Items.Add(date + " " + "14:30");
            combobox.Items.Add(date + " " + "14:45");
            combobox.Items.Add(date + " " + "15:00");
        }
    }
}
