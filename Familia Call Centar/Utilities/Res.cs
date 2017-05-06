using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Familia_Call_Centar.Utilities
{
    public static class Res
    {
        public static String grahUri = @"C:\Users\Edin\documents\visual studio 2015\Projects\Familia Call Centar\Familia Call Centar\Slike jela\grah.jpg";
        public static String kobasiceUri = @"C:\Users\Edin\documents\visual studio 2015\Projects\Familia Call Centar\Familia Call Centar\Slike jela\kobasice.jpg";
        public static String sarmaUri = @"C:\Users\Edin\documents\visual studio 2015\Projects\Familia Call Centar\Familia Call Centar\Slike jela\sarma.jpg";
        public static String connectionString = @"server=127.0.0.1;user id=Admin;pwd=admin;persistsecurityinfo=True;database=testna";

        public static BitmapImage addGrahImage()
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(Res.grahUri, UriKind.RelativeOrAbsolute);
            bitmap.EndInit();
            return bitmap;
        }

        public static BitmapImage addKobasiceImage()
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(Res.kobasiceUri, UriKind.RelativeOrAbsolute);
            bitmap.EndInit();
            return bitmap;
        }

        public static BitmapImage addSarmaImage()
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(Res.sarmaUri, UriKind.RelativeOrAbsolute);
            bitmap.EndInit();
            return bitmap;
        }
    }
}
