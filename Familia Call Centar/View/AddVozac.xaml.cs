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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Familia_Call_Centar.Model;
using Familia_Call_Centar.Utilities;
using Familia_Call_Centar.Servis;

namespace Familia_Call_Centar.View
{
    /// <summary>
    /// Interaction logic for AddVozac.xaml
    /// </summary>
    public partial class AddVozac : Page
    {
        Service service;
        DBHandler handler;
        public AddVozac(Service service)
        {
            InitializeComponent();
            handler = new DBHandler();
            this.service = service;
        }

        private void dodajButton_Click(object sender, RoutedEventArgs e)
        {
            if(!String.IsNullOrEmpty(imeTextBox.Text) ||
               !String.IsNullOrEmpty(prezimeTextBox.Text) ||
               !String.IsNullOrEmpty(idTextBox.Text) ||
               !String.IsNullOrEmpty(passwordTextBox.Password))
            {
                int id;
                if (Int32.TryParse(idTextBox.Text, out id))
                {
                    if (passwordTextBox.Password.Equals(passwordTextBox_Copy.Password))
                    {
                        vozac v = new vozac(imeTextBox.Text, prezimeTextBox.Text, id, passwordTextBox.Password);
                        using (FamiliaContextClass db = new FamiliaContextClass())
                        {
                            db.vozac.Add(v);
                            try
                            {
                                db.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                Console.Write(ex.StackTrace);
                                Console.WriteLine(ex.InnerException);
                            }
                            Page dash = new Dashboard(service);
                            NavigationService.Navigate(dash);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Unijete lozinke se ne poklapaju!");
                        passwordTextBox.Clear();
                        passwordTextBox_Copy.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Nekorektan tip identifikacijske oznake vozača!");
                    idTextBox.Clear();
                }
            }
            else MessageBox.Show("Unesite sve potrebne parametre!");
        }

        private void obrisiButton_Click(object sender, RoutedEventArgs e)
        {
            Int32 id;
            if(String.IsNullOrEmpty(idTextBox.Text))
            {
                if (Int32.TryParse(idTextBox.Text, out id))
                {
                    if (handler.checkIfEntryExists("vozac", id, null) == 1) handler.deleteEntry("vozac", id, null);
                    else MessageBox.Show("Vozač sa unesenom identifikacijskom oznakom ne postoji!");
                    idTextBox.Clear();
                }
                else
                {
                    MessageBox.Show("Nekorektan tip identifikacijske oznake vozača!");
                    idTextBox.Clear();
                }
            }
            else MessageBox.Show("Unesite identifikacijsku oznaku vozača za brisanje!");
        }

        private void nazadButton_Click(object sender, RoutedEventArgs e)
        {
            Page dash = new Dashboard(service);
            NavigationService.Navigate(dash);
        }
    }
}
