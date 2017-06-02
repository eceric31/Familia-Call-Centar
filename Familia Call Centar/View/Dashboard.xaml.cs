using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Familia_Call_Centar.Servis;
using Familia_Call_Centar.Utilities;

namespace Familia_Call_Centar.View
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        Service service;
        DBHandler handler;
        String password;
        int clicked;

        public Dashboard(Service service)
        {
            InitializeComponent();
            this.service = service;
            handler = new DBHandler();
        }

        private void zaprimiNaruzbuButton_Click(object sender, RoutedEventArgs e)
        {
            Page newNarudzba = new OrderInfo(service);
            NavigationService.Navigate(newNarudzba);
        }

        private void pokreniDostavu_Click(object sender, RoutedEventArgs e)
        {
            Page newDelivery = new NewDelivery(service);
            NavigationService.Navigate(newDelivery);
        }

        private void jelovnikButton_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = Visibility.Visible;
            clicked = 1;
        }

        private void vozilaButton_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = Visibility.Visible;
            clicked = 2;
        }

        private void vozaciButton_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = Visibility.Visible;
            clicked = 3;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            password = InputTextBox.Password;
            if (handler.authenticateAdmin(password) == 1)
            {
                Page page = null;
                switch (clicked)
                {
                    case 1:
                        page = new AddJelo(service);
                        break;
                    case 2:
                        page = new Vozilo(service); 
                        break;
                    case 3:
                        page = new AddVozac(service);
                        break;
                }
                password = null;
                NavigationService.Navigate(page);
            }
            else {
                MessageBoxResult box = MessageBox.Show("Netačna administratorska lozinka!");
                password = null;
                InputTextBox.Clear();
            }
            InputBox.Visibility = Visibility.Collapsed;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = Visibility.Collapsed;
            password = null;
            InputTextBox.Clear();            
        }

        private void napuniSkladiste_Click(object sender, RoutedEventArgs e)
        {
            Page fillStorage = new FillStorage(service);
            NavigationService.Navigate(fillStorage);
        }
    }
}
