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
        public static String kombiUri;
        public static String mopedUri;
        public static String connectionString = @"server=127.0.0.1;user id=Admin;pwd=admin;persistsecurityinfo=True;database=testna;Allow Zero Datetime=true";

        public static BitmapImage addKombiImage()
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(Res.kombiUri, UriKind.RelativeOrAbsolute);
            bitmap.EndInit();
            return bitmap;
        }

        public static BitmapImage addMopedImage()
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(Res.mopedUri, UriKind.RelativeOrAbsolute);
            bitmap.EndInit();
            return bitmap;
        }

        public static BitmapImage addImage(string path)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
            bitmap.EndInit();
            return bitmap;
        }
    }
}
