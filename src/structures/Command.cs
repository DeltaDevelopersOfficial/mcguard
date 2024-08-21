using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McGuard.src.structures
{
    internal struct Command
    {
        /// <summary>
        /// Command name
        /// </summary>
        public string Name;
           
        /// <summary>
        /// Command arguments
        /// </summary>
        public string[] Arguments;
        
        public Command(string name, string[] arguments)
        {
            Name = name;
            Arguments = arguments;
        }
    }
}
