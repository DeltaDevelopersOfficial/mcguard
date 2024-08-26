using System;

namespace McGuard.src.io
{
    internal class ConsoleWindow
    {
        /// <summary>
        /// Writes data to console with prefix if specified
        /// </summary>
        /// <param name="message">Text to print</param>
        /// <param name="prefix">Prefix is text before text</param>
        public static void WriteLine(string message, string prefix = null)
        {
            if (prefix == null)
            {
                Console.WriteLine(message);
            }
            else
            {
                Console.WriteLine(prefix + " " + message);
            }
        }
    }
}
