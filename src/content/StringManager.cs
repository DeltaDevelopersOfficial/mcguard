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
            #region Basic string adding
            AddString(0, "Insufficient administrator privileges");
            AddString(1, "OK");
            AddString(2, "No players found");
            AddString(3, "%s joined the game");
            AddString(4, "McGuard v1.0 active - by https://pavelkalas.xyz");
            AddString(5, "Executing macro macros/%s");
            AddString(6, "Macro macros/%s not found.");
            AddString(7, "Macro macros/%s does not contain any commands.");
            AddString(8, "Missing arguments");
            AddString(9, "McGuard v1.0 active");
            AddString(10, "Unexpected error, please report this with code %s");
            AddString(11, "%s is %d defined.");
            AddString(12, "Undefined command %s");
            AddString(13, "Invalid context format");
            AddString(14, "Not available for %s");
            AddString(15, "Unknown mcguard command: %s");
            AddString(16, "Reloading the configuration..");
            AddString(17, "MCGuard has %a %p from the server (%w)"); // %a = action | %p = player | %r = who (for now only "Administrator")
            #endregion
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
