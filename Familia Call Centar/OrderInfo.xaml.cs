using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Familia_Call_Centar.Controller;

namespace Familia_Call_Centar
{
    /// <summary>
    /// Interaction logic for OrderInfo.xaml
    /// </summary>
    public partial class OrderInfo : Window
    {
        OrderController oc;
        public OrderInfo()
        {
            InitializeComponent();
            oc = new OrderController();
            DataContext = oc;
        }
     
        private void daljeClick(Object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(imeInput.Text) || String.IsNullOrEmpty(prezimeInput.Text) ||
                String.IsNullOrEmpty(brTelInput.Text) || String.IsNullOrEmpty(firmaInput.Text) ||
                String.IsNullOrEmpty(vrijemeIspInput.Text) || String.IsNullOrEmpty(adresaInput.Text))
            {
                MessageBoxResult box = MessageBox.Show("Molimo unesite sve parametre!!!");
            }
            else oc.saveOrder();
        }

        private void clearClick(Object sender, EventArgs e)
        {
            this.imeInput.Text = "";
            this.prezimeInput.Text = "";
            this.brTelInput.Text = "";
            this.firmaInput.Text = "";
            this.adresaInput.Text = "";
            this.vrijemeIspInput.Text = "";
        }

    }
}
