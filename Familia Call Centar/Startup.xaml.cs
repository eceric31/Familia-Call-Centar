using System.Windows;
using Familia_Call_Centar.View;
using Familia_Call_Centar.Utilities;
using System;
using Familia_Call_Centar.Servis;
using System.Threading;

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
            Thread th = new Thread(new ThreadStart(service.listen));
            th.Start();
            MainFrame.Navigate(new Dashboard(service));
        }
    }
}
