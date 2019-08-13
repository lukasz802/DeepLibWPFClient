using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AppData
{
    public static class RouteData
    {
        public static Stream GetImageStream()
        {
            Random rnd = new Random();
            int img_num = rnd.Next(1, 6);
            Assembly asm = Assembly.GetExecutingAssembly();
            Stream strm = asm.GetManifestResourceStream("AppData.Images.Image_" + img_num.ToString() + ".jpg");
            return strm;
        }

        public static KeyValuePair<Stream, string> GetImageWithBrushPair()
        {
            Random rnd = new Random();
            int img_num = rnd.Next(1, 6);
            Assembly asm = Assembly.GetExecutingAssembly();
            Stream strm = asm.GetManifestResourceStream("AppData.Images.Image_" + img_num.ToString() + ".jpg");
            string img_hex = string.Empty;

            switch (img_num)
            {
                case 1:
                    img_hex = "#FFB45527";
                    break;
                case 2:
                    img_hex = "#FF2E352E";
                    break;
                case 3:
                    img_hex = "#FF1774A3";
                    break;
                case 4:
                    img_hex = "#FF566951";
                    break;
                case 5:
                default:
                    img_hex = "#FF3A6199";
                    break;
            }

            KeyValuePair<Stream, string> result = new KeyValuePair<Stream, string>(strm, img_hex);
            return result;
        }
    }
}
