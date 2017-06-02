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
using Familia_Call_Centar.Utilities;
using System.Data;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text;

namespace Familia_Call_Centar.View
{
    /// <summary>
    /// Interaction logic for FillStorage.xaml
    /// </summary>
    public partial class FillStorage : Page
    {
        Service service;
        DBHandler handler;
        DataTable table;

        public FillStorage(Service service)
        {
            InitializeComponent();
            this.service = service;
            handler = new DBHandler();

            table = handler.fillDataTableJelaForStorage();
            table.Columns[0].ColumnName = "Numerička oznaka jela";
            table.Columns[1].ColumnName = "Naziv jela";
            table.Columns[2].ColumnName = "Cijena";
            table.Columns.Add("Količina");

            jelaGrid.ItemsSource = table.DefaultView;
        }

        private void genRep_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Da li ste sigurni da želite ovakve postavke?", "", MessageBoxButton.YesNo);
            if(res == MessageBoxResult.Yes)
            {
                DataTable tab = ((DataView)jelaGrid.ItemsSource).ToTable();
                generatePDF(tab);
                handler.kreirajPredIsporuku(tab);
                
                Page dash = new Dashboard(service);
                NavigationService.Navigate(dash);
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            Page dash = new Dashboard(service);
            NavigationService.Navigate(dash);
        }

        public void generatePDF(DataTable tab)
        {
            using (FileStream stream = new FileStream("C:\\Users\\Edin\\Desktop\\Izvjestaj "
                    + DateTime.Now.Day.ToString() + "." + DateTime.Now.Month.ToString()
                    + "." + DateTime.Now.Year.ToString()+ ".pdf", FileMode.Create)) 
            {
                Document doc = new Document(PageSize.A4, 10, 10, 10, 10);
                PdfWriter writer = PdfWriter.GetInstance(doc, stream);
                doc.Open();
                doc.AddTitle("Izvjestaj za dan " + DateTime.Now.Day.ToString() + "." + DateTime.Now.Month.ToString()
                            + "." + DateTime.Now.Year.ToString());

                iTextSharp.text.Paragraph p = new iTextSharp.text.Paragraph("Izvještaj za dan "
                    + DateTime.Now.Day.ToString() + "." + DateTime.Now.Month.ToString()
                    + "." + DateTime.Now.Year.ToString());
                doc.Add(p);

                PdfPTable pdfTable = new PdfPTable(2);
                pdfTable.DefaultCell.Border = 0;
                pdfTable.WidthPercentage = 80;

                for (int i = 0; i < tab.Rows.Count; i++)
                {
                    pdfTable.AddCell(tab.Rows[i][1].ToString());
                    pdfTable.AddCell(tab.Rows[i][3].ToString());
                }
                doc.Add(pdfTable);
                
                doc.Close();
            }
        }
        
    }
}
