using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McGuard.src.content
{
    internal class StringManager
    {
        private static Dictionary<int, string> stringsList = new Dictionary<int, string>();

        public static void Setup()
        {
            AddString(0, "Insufficient administrator privileges");
            AddString(1, "OK");
            AddString(2, "No players found");
            AddString(3, "%s joined the game");
            AddString(4, "McGuard v1.0 active - github.com/pavelkalas/mcguard");
        }

        /// <summary>
        /// Add string to list
        /// </summary>
        /// <param name="str"></param>
        public static void AddString(int index, string str)
        {
            stringsList.Add(index, str);
        }

        /// <summary>
        /// Gets string from list
        /// </summary>
        /// <param name="index"></param>
        /// <returns>String</returns>
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
