using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McGuard.src.structures
{
    internal struct Configuration
    {
        /// <summary>
        /// Configuration key
        /// </summary>
        public string ConfigKey;

        /// <summary>
        /// Configuration value
        /// </summary>
        public string ConfigVal;

        /// <summary>
        /// It has no empty value
        /// </summary>
        public bool IsSet;
    }
}
