using BookStore.Services.SettingsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace BookStoreWinApp.Models
{
    public class ManageFiles
    {
        public static Dictionary<string, List<string>> GetAllFiles(string path, string ext)
        {
            var files = Directory.GetFiles(path, "*." + ext, SearchOption.AllDirectories);
            var direName = DirectoryName(path);
            var filesDictionary = new Dictionary<string, List<string>>();
            var filesList = new List<string>();
          
            foreach ( var file in files)
            {
                if(IsFile(file))
                {
                    direName = DirectoryName(ParentDirectory(file));
                    if (filesDictionary.ContainsKey(direName))
                    {
                        filesDictionary[direName].Add(file);
                    }
                    else
                    {
                        filesDictionary[direName] = new List<string>();
                        filesDictionary[direName].Add(file);
                    }
                }
            }

            return filesDictionary;
        }


        public static string ParentDirectory(string path)
        {
            return Directory.GetParent(path).ToString();
        }
        public static string DirectoryName(string path)
        {
            var split = path.TrimEnd('\\').ToString().Split("\\");
            var len = split.Length;
            var name = split[len - 1];
            return name;
        }

        public static string FileName(string path)
        {
            return DirectoryName(path);
        }

        public static string GetTitleFromPath(string path)
        {
            var filename = FileName(path);
            var info = new FileInfo(filename);
            var fileNameNoExt = info.Name.Replace(".pdf", "");
            return fileNameNoExt;
        }

        public static bool IsFile(string path)
        {
            // get the file attributes for file or directory
            FileAttributes attr = File.GetAttributes(path);

            if (attr.HasFlag(FileAttributes.Directory))
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public static string GetCleanImageFileName(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            var justFileName = fileInfo.Name;
            var ext = fileInfo.Extension;
            justFileName = justFileName.Replace(ext, "");

            Regex rgs = new Regex("[^a-zA-Z0-9]");
            var str = rgs.Replace(justFileName, "-");
            var subStr = str.ToLower();

            if (subStr.Length > 100 )
            {
                subStr = subStr.Substring(0, 100);
            }
            //if(ext != string.Empty)
            //{
            //    return $"{subStr}.{ext}";
            //}
            return subStr;
        }

        public static string GetFullPath(Dictionary<string, string> settings, string subPath)
        {
            return Path.Combine(settings["ImagesLocation"], subPath );
        }

        public static bool CreateDirectoryOfPath(string path)
        {
            var dir = ParentDirectory(path);
            var obj = Directory.CreateDirectory(dir);
            return obj.ToString() != string.Empty;
        }
    }
}
