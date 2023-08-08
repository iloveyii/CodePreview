using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreWinApp.Models
{
    public class Helper
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(HandleRef hWnd, out Rectangle lpRect);
        public static void LocateFile(string filePath)
        {
            string argument = "/select, \"" + filePath + "\"";
            var p = System.Diagnostics.Process.Start("explorer.exe", argument);
            /*var str = p.MainWindowTitle;
            var myRect = new Rectangle();
            Rectangle rect;
            GetWindowRect(new HandleRef(p, p.Handle), out rect );
            rect.Width = 200;
            rect.Height = 200;
           rect.Size = new Size(200, 200); */

        }

        public static void OpenFile(string filePath)
        {
            using Process fileopener = new Process();

            fileopener.StartInfo.FileName = "explorer";
            fileopener.StartInfo.Arguments = "\"" + filePath + "\"";
            fileopener.Start();
        }

        public static void Log(string id, string message)
        {
            var path = ConfigurationManager.AppSettings.Get("LOG_PATH");
            string line = $"{id} : {message} \n";
            File.AppendAllText(path, line);
        }

    }
}
