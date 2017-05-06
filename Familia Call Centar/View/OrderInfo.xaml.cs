using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Familia_Call_Centar.Model;
using System.Diagnostics;
using System.Data;
using Familia_Call_Centar.Utilities;

namespace Familia_Call_Centar.View
{
    /// <summary>
    /// Interaction logic for OrderInfo.xaml
    /// </summary>
    public partial class OrderInfo : Page
    {
        narudzba narudzba;
        FamiliaContextClass db;
        DBHandler handler;
        public OrderInfo()
        {
            InitializeComponent();
            vrijemeIspInput.Text = DateTime.Now.ToString();
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
            if (String.IsNullOrEmpty(imeInput.Text) || String.IsNullOrEmpty(prezimeInput.Text) ||
               String.IsNullOrEmpty(brTelInput.Text) || String.IsNullOrEmpty(firmaInput.Text) ||
               String.IsNullOrEmpty(adresaInput.Text))
            {
                MessageBoxResult box = MessageBox.Show("Molimo unesite sve parametre!");
            }
            else
            {
                narudzba = new narudzba(imeInput.Text, prezimeInput.Text, brTelInput.Text, firmaInput.Text, adresaInput.Text, (DateTime)vrijemeIspInput.SelectedDate);
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
