using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using Familia_Call_Centar.Utilities;
using Familia_Call_Centar.Model;

namespace Familia_Call_Centar.View
{
    /// <summary>
    /// Interaction logic for FoodPick.xaml
    /// </summary>
    public partial class FoodPick : Page
    {
        int narudzbaID;
        FamiliaContextClass db;
        List<jelo> jela;
        TextRange tr;
        DBHandler dbHandler;

        public FoodPick(int nID)
        {
            InitializeComponent();
            narudzbaID = nID;
            //inicijalizacija resursa
            jela = new List<jelo>();
            db = new FamiliaContextClass();
            dbHandler = new DBHandler();
            tr = new TextRange(opisJela.Document.ContentStart, opisJela.Document.ContentEnd);
            //prikazivanje slika
            slika1.Source = Res.addGrahImage();
            slika2.Source = Res.addKobasiceImage();
            slika3.Source = Res.addSarmaImage();
            //ucitavanje jela iz baze
            jela = dbHandler.loadJela();
            //prikazivanje jela u labelama
            jelo1.Content = jela[0].naziv;
            jelo2.Content = jela[1].naziv;
            jelo3.Content = jela[2].naziv;
        }

        private void saveOrderItems()
        {
            List<narudzba_item> items = new List<narudzba_item>();
            
            if(!String.IsNullOrEmpty(kolicina1.Text))
            {
                jelo j = jela.Find(item => item.naziv == "Grah");
                int kol = Convert.ToInt32(kolicina1.Text);

                db.narudzba_item.Add(new narudzba_item(kol, (Double)(j.cijena * kol),
                    j.jeloID, narudzbaID));

                saveChanges();
            }
            if (!String.IsNullOrEmpty(kolicina2.Text))
            {
                jelo j = jela.Find(item => item.naziv == "Kobasice");
                int kol = Convert.ToInt32(kolicina2.Text);

                db.narudzba_item.Add(new narudzba_item(kol, (Double)(j.cijena * kol),
                    j.jeloID, narudzbaID));

                saveChanges();
            }
            if (!String.IsNullOrEmpty(kolicina3.Text))
            {
                jelo j = jela.Find(item => item.naziv == "Sarma");
                int kol = Convert.ToInt32(kolicina3.Text);

                db.narudzba_item.Add(new narudzba_item(kol, (Double)(j.cijena * kol),
                    j.jeloID, narudzbaID));

                saveChanges();
            }
            //sad za sad tri jela
        } 
        
        private void clear_Click(object sender, RoutedEventArgs e)
        {
            kolicina1.Clear();
            kolicina2.Clear();
            kolicina3.Clear();
            tr.Text = "";
            jelo1Kolicina.Content = "";
            jelo2Kolicina.Content = "";
            jelo3Kolicina.Content = "";
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            saveOrderItems();
            Page dash = new Dashboard();
            NavigationService.Navigate(dash);
        }

        private void kolicina1_TextChanged(object sender, TextChangedEventArgs e)
        {
            jelo1Kolicina.Content = kolicina1.Text;
        }

        private void kolicina2_TextChanged(object sender, TextChangedEventArgs e)
        {
            jelo2Kolicina.Content = kolicina2.Text;
        }

        private void kolicina3_TextChanged(object sender, TextChangedEventArgs e)
        {
            jelo3Kolicina.Content = kolicina3.Text;
        }

        private void slika1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tr.Text = jela.Find(item => item.naziv == "Grah").opis;
        }

        private void slika2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tr.Text = jela.Find(item => item.naziv == "Kobasice").opis;
        }

        private void slika3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tr.Text = jela.Find(item => item.naziv == "Sarma").opis;
        }

        private void saveChanges()
        {
            try { db.SaveChanges(); }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException);
            }
        }

    }
}
