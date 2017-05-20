using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Familia_Call_Centar.Servis;

namespace Familia_Call_Centar.View
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        Service service;

        public Dashboard(Service service)
        {
            InitializeComponent();
            this.service = service;
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
    }
}
