using System.Collections.Generic;
using System.Drawing;
using System.IO;


namespace BookStoreWinApp.Models
{
    public class TiffImage
    {
        public static string Extract(string Inputfile, string OutputFile)
        {
            var dd = System.IO.File.ReadAllBytes(Inputfile);
            byte[] pngByte = Freeware.Pdf2Png.Convert(dd, 1);
            System.IO.File.WriteAllBytes(OutputFile, pngByte);
            return OutputFile;
        }
    }
}
