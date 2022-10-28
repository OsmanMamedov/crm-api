using System;
using System.IO;

namespace General.Helper
{
    public static class ConvertHelper
    {
        public static void ImageFromBase64String(string imageBase64, string path)
        {
            File.WriteAllBytes(path, Convert.FromBase64String(imageBase64));
        }
    }
}
