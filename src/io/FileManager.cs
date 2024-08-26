using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McGuard.src.io
{
    internal class FileManager
    {
        /// <summary>
        /// Log line into specified log file
        /// </summary>
        /// <param name="fileName">Path to log file</param>
        /// <param name="newLine">New line to log</param>
        public static void LogFileLine(string fileName, string newLine)
        {
            newLine = newLine.Trim();

            if (!File.Exists(fileName))
            {
                File.WriteAllText(fileName, newLine + "\n");
            }
            else
            {
                string content = File.ReadAllText(fileName);
                content += newLine + "\n";
                File.WriteAllText(fileName, content);
            }
        }
    }
}
