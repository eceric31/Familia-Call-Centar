using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Familia_Call_Centar.View
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void zaprimiNaruzbuButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Uri("/View/OrderInfo.xaml", UriKind.RelativeOrAbsolute));
            }
            catch(UriFormatException ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
