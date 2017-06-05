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
using System.IO;
using Microsoft.Win32;

namespace Familia_Call_Centar.View
{
    /// <summary>
    /// Interaction logic for AddJelo.xaml
    /// </summary>
    public partial class AddJelo : Page
    {
        DBHandler handler;
        Service service;
        String path = null;

        public AddJelo(Service service)
        {
            InitializeComponent();
            handler = new DBHandler();
            this.service = service;
        }

        private void dodajButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(String.IsNullOrEmpty(nazivJelaTextBox.Text) ||
                String.IsNullOrEmpty(opisJelaTextBox.Text) ||
                String.IsNullOrEmpty(tipJelaTextBox.Text) ||
                String.IsNullOrEmpty(cijenaJelaTextBox.Text)))
            {
                Double cijena = 0.0;
                if (Double.TryParse(cijenaJelaTextBox.Text, out cijena))
                {
                    String nazivJela = nazivJelaTextBox.Text;
                    if (handler.checkIfEntryExists("jelo", 0, nazivJela) == 1)
                    {
                        MessageBox.Show("Postoji jelo sa datim nazivom!");
                        nazivJelaTextBox.Clear();
                    }
                    else
                    {
                        if(path == null)
                        {
                            MessageBox.Show("Odaberite sliku jela!");
                        }
                        else
                        {
                            jelo j = new jelo(nazivJela, opisJelaTextBox.Text, tipJelaTextBox.Text, cijena);
                            using (FamiliaContextClass db = new FamiliaContextClass())
                            {
                                db.jelo.Add(j);
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
                            handler.updateEntry("jelo", path, j.jeloID, -1);
                            path = null;
                            Page dash = new Dashboard(service);
                            NavigationService.Navigate(dash);
                        }
                    }
                }
                else {
                    MessageBox.Show("Neodgovarajuća cijena!");
                    cijenaJelaTextBox.Clear();
                }
            }
            else MessageBox.Show("Unesite sve potrebne parametre!");
        }

        private void obrisiButton_Click(object sender, RoutedEventArgs e)
        {
            String naziv = nazivJelaTextBox.Text;
            if (!String.IsNullOrEmpty(naziv))
            {
                if (handler.checkIfEntryExists("jelo", 0, naziv) == 1)
                    handler.deleteEntry("jelo", 0, naziv);
                else
                    MessageBox.Show("Jelo sa unesenim nazivom ne postoji!");
                nazivJelaTextBox.Clear();
            }
            else MessageBox.Show("Unesite naziv jela za brisanje!");
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            String naziv = nazivJelaTextBox.Text;
            if (!String.IsNullOrEmpty(naziv))
            {
                if (handler.checkIfEntryExists("jelo", 0, naziv) == 1)
                {
                    if (!String.IsNullOrEmpty(cijenaJelaTextBox.Text))
                    {
                        Double cijena;
                        if (Double.TryParse(cijenaJelaTextBox.Text, out cijena))
                        {
                            handler.editJelo(naziv, "cijena", null, cijena);
                            nazivJelaTextBox.Clear();
                            cijenaJelaTextBox.Clear();
                        }
                        else
                        {
                            MessageBox.Show("Neodgovarajuća cijena!");
                            cijenaJelaTextBox.Clear();
                        }
                    }
                    else MessageBox.Show("Unesite polje za editovanje!");
                }
                else
                {
                    MessageBox.Show("Jelo sa unesenim nazivom ne postoji!");
                    nazivJelaTextBox.Clear();
                }
            }
            else MessageBox.Show("Unesite naziv jela za editovanje!");    
        }

        private void nazadButton_Click(object sender, RoutedEventArgs e)
        {
            Page dash = new Dashboard(service);
            NavigationService.Navigate(dash);
        }

        private Boolean checkIfImageExists(string path)
        {
            return File.Exists(path);
        }

        private void addImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Odabir slike jela";
            if (ofd.ShowDialog() == true)
            {
                path = ofd.FileName;
                path = path.Replace(@"\", "\\\\");
                image.Source = Res.addImage(path);
            }
        }
    }
}
