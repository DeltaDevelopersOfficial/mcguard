using McGuard.src.handlers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace McGuard.src.structures
{
    internal class Macro
    {
        /// <summary>
        /// List of commands
        /// </summary>
        public List<string> commandList = new List<string>();

        public Macro(List<string> commandList)
        {
            this.commandList = commandList;
        }
    }
}
