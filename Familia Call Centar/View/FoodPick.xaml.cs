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
using Familia_Call_Centar.Utilities;
using Familia_Call_Centar.Model;

namespace Familia_Call_Centar.View
{
    /// <summary>
    /// Interaction logic for FoodPick.xaml
    /// </summary>
    public partial class FoodPick : Page
    {
        narudzba_item item;
        FamiliaContextClass db;
        List<jelo> jela;
        int narudzbaID;
        TextRange tr;

        public FoodPick(int nID)
        {
            InitializeComponent();
            narudzbaID = nID;
            item = new narudzba_item();
            db = new FamiliaContextClass();

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            addGrahImage(bitmap);
            addKobasiceImage(bitmap);
            bitmap.EndInit();

            loadJela();

            tr = new TextRange(opisJela.Document.ContentStart, opisJela.Document.ContentEnd);
        }

        private void saveOrderItems()
        {
            item.narudzbaID = narudzbaID;
            if(!String.IsNullOrEmpty(kolicina1.Text))
            {
                jelo j = jela.Find(item => item.naziv == "grah");
                item.kvantitet = Convert.ToInt32(kolicina1.Text);
                item.jeloID = j.jeloID;
                item.ukupna_cijena = j.cijena * item.kvantitet;
                tr.Text = j.opis;
                jelo1.Content = j.naziv;
                jelo1Kolicina.Content = kolicina1.Text;
                db.narudzba_item.Add(item);
                db.SaveChanges();
            }
            if (!String.IsNullOrEmpty(kolicina2.Text))
            {
                jelo j = jela.Find(item => item.naziv == "kobasice");
                item.kvantitet = Convert.ToInt32(kolicina2.Text);
                item.jeloID = j.jeloID;
                item.ukupna_cijena = j.cijena * item.kvantitet;
                tr.Text = j.opis;
                jelo2.Content = j.naziv;
                jelo2Kolicina.Content = kolicina2.Text;
                db.narudzba_item.Add(item);
                db.SaveChanges();
            }
            //i tako dalje
            //vidjeti kako za ovaj infinite scroll
        } 

        private void addGrahImage(BitmapImage bitmap)
        {
            bitmap.UriSource = new Uri(Res.grahUri, UriKind.Absolute);
            slika1.Source = bitmap;
        }

        private void addKobasiceImage(BitmapImage bitmap)
        {
            bitmap.UriSource = new Uri(Res.kobasiceUri, UriKind.Absolute);
            slika2.Source = bitmap;
        }

        private void loadJela()
        {
            jela = db.jelo.ToList();
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            kolicina1.Clear();
            kolicina2.Clear();
            kolicina3.Clear();
            tr.Text = "";
            jelo1.Content = "";
            jelo2.Content = "";
            jelo1Kolicina.Content = "";
            jelo2Kolicina.Content = "";
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            saveOrderItems();
            Page dash = new Dashboard();
            NavigationService.Navigate(dash);
        }
        
    }
}
