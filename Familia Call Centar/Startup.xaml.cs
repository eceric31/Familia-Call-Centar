using System.Windows;
using Familia_Call_Centar.View;
using Familia_Call_Centar.Utilities;
using System;
using Familia_Call_Centar.Servis;

namespace Familia_Call_Centar
{
    /// <summary>
    /// Interaction logic for Startup.xaml
    /// </summary>
    public partial class Startup : Window
    {
        public Startup()
        {
            InitializeComponent();
            Service service = new Service();
            MainFrame.Navigate(new Dashboard(service));
        }
    }
}
