using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class FileLogger : ILoger
    {
        private static string logPath;
        public FileLogger()
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(path, "log.txt");
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }
            logPath = filePath;
        }

        public void WriteLine(string s)
        {
            //using (FileStream stream = new FileStream(logPath, FileMode.Append))
            //using (StreamWriter writer = new StreamWriter(stream))
            //{
            //    //writer.WriteLine(GetTimeString());
            //    writer.Write(s);
            //}
            File.AppendAllText(logPath, s);
        }

        private string GetTimeString()
        {
            string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            return dateTime + ":";
        }
    }
}
