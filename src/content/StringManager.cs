using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McGuard.src.content
{
    internal class StringManager
    {
        private static List<string> stringsList = new List<string>();

        public static void Setup()
        {
            AddString("Insufficient administrator privileges");
        }

        /// <summary>
        /// Add string to list
        /// </summary>
        /// <param name="str"></param>
        public static void AddString(string str)
        {
            stringsList.Add(str);
        }

        /// <summary>
        /// Gets string from list
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GetString(int index)
        {
            if (index < stringsList.Count && index >= 0)
            {
                return stringsList[index];
            }

            return null;
        }
    }
}
