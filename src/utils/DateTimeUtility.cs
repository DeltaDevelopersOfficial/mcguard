using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McGuard.src.utils
{
    internal class DateTimeUtility
    {
        /// <summary>
        /// Get only time in format xx:xx:xx (format 12-24hrs is by locale settings)
        /// </summary>
        /// <returns>Only time string in format xx:xx:xx</returns>
        public static string GetCurrentTime()
        {
            string time = DateTime.Now.TimeOfDay.ToString();

            if (time.Contains("."))
            {
                time = time.Split('.')[0];
            }

            return time;
        }
    }
}
