using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test0507.Core
{
    public class DirectoryHelper
    {
        public static void DeleteAllFilesAndDirectories(string folderPath)
        {
            DeleteAllFiles(folderPath);
            DeleteAllDirectories(folderPath);
        }

        public static void DeleteAllFiles(string folderPath)
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(folderPath);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }

        public static void DeleteAllDirectories(string folderPath)
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(folderPath);

            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }

    }
}
