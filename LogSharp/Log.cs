using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDDComparison.LogSharp
{
    public class Log
    {
        public Log(LogFormat format, string fileName = null)
        {
            logFormatting = format;
            if (string.IsNullOrEmpty(fileName))
            {
                FileName = $"{DateTimeOffset.Now.ToUnixTimeSeconds()}.txt";
            }
            else
                FileName = fileName;
        }

        private string FileName;
        private LogFormat logFormatting;

        private string FormattedTime()
        {
            if (logFormatting == LogFormat.DMY)
                return DateTime.Now.ToString("d-M-y H:m");
            else if (logFormatting == LogFormat.MDY)
                return DateTime.Now.ToString("M-d-y H:m");

            return null;
        }

        public void AppendLog(string content)
        {
            using (StreamWriter sw = new StreamWriter(FileName, append: true))
            {
                sw.WriteLine($"[{FormattedTime()}] " + content);
                sw.Close();
            }
        }
    }
}
