using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using Familia_Call_Centar.Utilities;
using Familia_Call_Centar.Model;
using Familia_Call_Centar.Servis;
using System.Windows.Media.Imaging;

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
        List<string> urls;
        TextRange tr;
        DBHandler dbHandler;
        Service service;
        List<TextBox> tbs = new List<TextBox>();
        List<Image> imgs = new List<Image>();
        List<int> tbIndexes = new List<int>();
        List<int> tbEntries = new List<int>();
        List<Label> kolicine = new List<Label>();

        public FoodPick(int nID, Service service)
        {
            InitializeComponent();

            this.service = service;
            narudzbaID = nID;
            //inicijalizacija resursa
            jela = new List<jelo>();
            db = new FamiliaContextClass();
            dbHandler = new DBHandler();
            tr = new TextRange(opisJela.Document.ContentStart, opisJela.Document.ContentEnd);
            //ucitavanje jela iz baze
            jela = dbHandler.loadJela();
            urls = dbHandler.loadJelaUrls();
            //scroll view jela
            createScroll();
            //scroll view kolicine jela
            createScrollForDishes();
        }

        private void saveOrderItems()
        {
            List<int> indexCopies = new List<int>();
            List<int> entryCopies = new List<int>();

            for (int i = tbIndexes.Count - 1; i >= 0; i--) {
                if (!indexCopies.Contains(tbIndexes[i]))
                {
                    indexCopies.Add(tbIndexes[i]);
                    entryCopies.Add(tbEntries[i]);
                }
            }

            for(int i = 0; i < indexCopies.Count; i++)
            {
                jelo j = jela[indexCopies[i]];
                int kol = entryCopies[i];
                db.narudzba_item.Add(new narudzba_item(kol, (Double)(j.cijena * kol),
                    j.jeloID, narudzbaID));
                saveChanges();
            }
        } 
        
        private void clear_Click(object sender, RoutedEventArgs e)
        {
            tr.Text = "";
            tbIndexes.Clear();
            tbEntries.Clear();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            saveOrderItems();
            Page dash = new Dashboard(service);
            NavigationService.Navigate(dash);
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

        private void createScroll()
        {
            int colNum = 0;
            int rowNum = 0;
            RowDefinition def = new RowDefinition();
            def.MinHeight = 130;
            scrollGrid.RowDefinitions.Add(def);

            for (int i = 0; i < urls.Count; i++)
            {
                if (colNum == 3) {
                    RowDefinition rowDef = new RowDefinition();
                    rowDef.MinHeight = 120;
                    scrollGrid.RowDefinitions.Add(rowDef);
                    colNum = 0;
                    rowNum++;
                    i--;
                    continue;
                }
                
                Image img = new Image();
                img.Height = 110;
                img.Width = 140;
                img.Margin = new Thickness(5);
                img.VerticalAlignment = VerticalAlignment.Top;
                img.Source = Res.addImage(urls[i]);
                scrollGrid.Children.Add(img);
                Grid.SetRow(img, rowNum);
                Grid.SetColumn(img, colNum);
                imgs.Add(img);

                TextBox tb = new TextBox();
                tb.HorizontalContentAlignment = HorizontalAlignment.Center;
                tb.VerticalContentAlignment = VerticalAlignment.Center;
                tb.Height = 26;
                tb.Width = 140;
                tb.VerticalAlignment = VerticalAlignment.Bottom;
                tb.Margin = new Thickness(5);
                tb.Padding = new Thickness(5);
                scrollGrid.Children.Add(tb);
                Grid.SetRow(tb, rowNum);
                Grid.SetColumn(tb, colNum);
                tb.TextChanged += textChanged;
                tbs.Add(tb);
                
                colNum++;
            }
        }

        private void createScrollForDishes()
        {
            for (int i = 0; i < jela.Count; i++)
            {
                RowDefinition def = new RowDefinition();
                def.Height = GridLength.Auto;
                stanjeJelaGrid.RowDefinitions.Add(def);

                Label tb = new Label();
                tb.HorizontalContentAlignment = HorizontalAlignment.Left;
                tb.Height = 26;
                tb.Width = 60;
                tb.Content = jela[i].naziv;
                stanjeJelaGrid.Children.Add(tb);
                Grid.SetRow(tb, i);
                Grid.SetColumn(tb, 0);

                Label kolicina = new Label();
                kolicina.Height = 26;
                kolicina.Width = 60;
                stanjeJelaGrid.Children.Add(kolicina);
                Grid.SetRow(kolicina, i);
                Grid.SetColumn(kolicina, 1);
                kolicine.Add(kolicina);
            }
        }        

        private void textChanged(object sender, TextChangedEventArgs e)
        {
            var tb = sender as TextBox;
            int index = Grid.GetRow(tb) * 3 + Grid.GetColumn(tb);
            int value;

            if (String.IsNullOrEmpty(tb.Text))
            {
                tbIndexes.Add(index);
                tbEntries.Add(0);
                kolicine[index].Content = "";
            }
            else if(Int32.TryParse(tb.Text, out value))
            {
                tbIndexes.Add(index);
                tbEntries.Add(value);
                kolicine[index].Content = value.ToString();
            }
            else
            {
                MessageBox.Show("Nevalidna količina hrane!");
            }
            tr.Text = jela[index].opis;
        }
    }
}
