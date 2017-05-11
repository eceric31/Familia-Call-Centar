using System.Windows.Controls;
using Familia_Call_Centar.Model;
using Familia_Call_Centar.Utilities;
using System.Data;
using System;
using System.Collections.Generic;

namespace Familia_Call_Centar.View
{
    /// <summary>
    /// Interaction logic for NewDelivery.xaml
    /// </summary>
    public partial class NewDelivery : Page
    {
        FamiliaContextClass db;
        DBHandler handler;
        DataTable narudzbeTable;
        DataTable isporukaTable;
        List<int> indexi = new List<int>();

        public NewDelivery()
        {
            InitializeComponent();
            db = new FamiliaContextClass();
            handler = new DBHandler();
            isporukaTable = new DataTable();

            narudzbeTable = handler.fillDataTableNewOrders();
            narudzbaDataGrid.DataContext = narudzbeTable.DefaultView;

            isporukaTable.Columns.Add("Ime naručioca", typeof(string));
            isporukaTable.Columns.Add("Prezime naručioca", typeof(string));
            isporukaTable.Columns.Add("Broj telefona", typeof(string));
            isporukaTable.Columns.Add("Naziv firme", typeof(string));
            isporukaTable.Columns.Add("Adresa firme", typeof(string));
            isporukaTable.Columns.Add("Očekivano vrijeme isporuke", typeof(string));
        }

        private void narudzbaDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            narudzbeTable = ((DataView)narudzbaDataGrid.ItemsSource).ToTable();
            var dg = sender as DataGrid;
            var index = dg.SelectedIndex;

            addRow(index);
            indexi.Add(index);
            isporukaDataGrid.DataContext = isporukaTable.DefaultView;
        }

        private void copyAll_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            for(int index = 0; index < narudzbeTable.Rows.Count; index++)
            {
                if (indexi.Contains(index)) continue;
                addRow(index);
            }
            isporukaDataGrid.DataContext = isporukaTable.DefaultView;
        }

        private void addRow(int index)
        {
            try
            {
                DataRow row = isporukaTable.NewRow();
                row["Ime naručioca"] = narudzbeTable.Rows[index][0].ToString();
                row["Prezime naručioca"] = narudzbeTable.Rows[index][1].ToString();
                row["Broj telefona"] = narudzbeTable.Rows[index][2].ToString();
                row["Naziv firme"] = narudzbeTable.Rows[index][3].ToString();
                row["Adresa firme"] = narudzbeTable.Rows[index][4].ToString();
                row["Očekivano vrijeme isporuke"] = narudzbeTable.Rows[index][5].ToString();
                isporukaTable.Rows.Add(row);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException);
            }
        }

        private void clearButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            isporukaTable.Clear();
            isporukaDataGrid.DataContext = isporukaTable.DefaultView;
        }
    }
}
