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
using Familia_Call_Centar.Servis;
using Familia_Call_Centar.Model;
using Familia_Call_Centar.Utilities;
using System.IO;
using Microsoft.Win32;

namespace Familia_Call_Centar.View
{
    /// <summary>
    /// Interaction logic for Vozilo.xaml
    /// </summary>
    public partial class Vozilo : Page
    {
        Service service;
        DBHandler handler;
        String path = null;
        public Vozilo(Service service)
        {
            InitializeComponent();
            this.service = service;
            handler = new DBHandler();
        }

        private void dodajButton_Click(object sender, RoutedEventArgs e)
        {
            if(!(String.IsNullOrEmpty(idTextBox.Text) ||
               String.IsNullOrEmpty(tipTextBox.Text) ||
               String.IsNullOrEmpty(nosivostTextBox.Text)))
            {
                int id;
                if (Int32.TryParse(idTextBox.Text, out id))
                {
                    if (handler.checkIfEntryExists("vozilo", id, null) == 1)
                    {
                        MessageBox.Show("Vozilo sa unesenom identifikacijskom oznakom već postoji!");
                        idTextBox.Clear();
                    }
                    else
                    {
                        int nosivost;
                        if (Int32.TryParse(nosivostTextBox.Text, out nosivost))
                        {
                            if(path == null)
                            {
                                MessageBox.Show("Unesite sliku vozila!");
                            }
                            else
                            {
                                vozilo v = new vozilo(id, tipTextBox.Text, nosivost);
                                using (FamiliaContextClass db = new FamiliaContextClass())
                                {
                                    db.vozilo.Add(v);
                                    try
                                    {
                                        db.SaveChanges();
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.Write(ex.StackTrace);
                                        Console.WriteLine(ex.InnerException);
                                    }
                                }
                                handler.updateEntry("vozilo", path, id, -1);
                                path = null;
                                Page dash = new Dashboard(service);
                                NavigationService.Navigate(dash);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Nevalidna nosivost vozila!");
                            nosivostTextBox.Clear();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Nevalidna identifikacijska oznaka vozila!");
                    idTextBox.Clear();
                }
            }
            else
            {
                MessageBox.Show("Unesite sve potrebne parametre!");
            }
        }

        private void obrisiButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            if (!String.IsNullOrEmpty(idTextBox.Text))
            {
                if (Int32.TryParse(idTextBox.Text, out id))
                {
                    if (handler.checkIfEntryExists("vozilo", id, null) == 1)
                        handler.deleteEntry("vozilo", id, null);
                    else
                        MessageBox.Show("Vozilo sa unesenim identifikacijskim oznakama ne postoji!");
                    idTextBox.Clear();
                }
                else
                {
                    MessageBox.Show("Nevalidna identifikacijska oznaka vozila!");
                    idTextBox.Clear();
                }
            }
            else MessageBox.Show("Unesite identifikacijsku oznaku vozila za brisanje!");
        }

        private void nazadButton_Click(object sender, RoutedEventArgs e)
        {
            Page dash = new Dashboard(service);
            NavigationService.Navigate(dash);
        }

        private void addImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Odabir slike vozila";
            if(ofd.ShowDialog() == true)
            {
                path = ofd.FileName;
                path = path.Replace(@"\", "\\\\");
                image.Source = Res.addImage(path);
            }
        }
    }
}
