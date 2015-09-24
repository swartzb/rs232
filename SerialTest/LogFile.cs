using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SerialTest
{
    public class LogFile
    {
        private string _fullFileName;

        public LogFile(string baseName)
        {
            // subdirectory of %LOCALAPPDATA%
            string folderPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                @"SerialTest\Data"
            );
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string fileName = baseName + ".txt";
            _fullFileName = Path.Combine(folderPath, fileName);

            using (FileStream fs = new FileStream(_fullFileName, FileMode.OpenOrCreate))
            {
                fs.SetLength(0);
            }
        }

        public void Append(string data)
        {
            using (StreamWriter sw = File.AppendText(_fullFileName))
            {
                sw.WriteLine(data);
            }
        }
    }
}
