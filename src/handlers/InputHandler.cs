using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McGuard.src.handlers
{
    internal class InputHandler
    {
        /// <summary>
        /// Server process instance
        /// </summary>
        private readonly Process process;

        public InputHandler(Process process)
        {
            this.process = process;
        }

        /// <summary>
        /// Sends input to an process.
        /// </summary>
        /// <param name="input"></param>
        public void SendInput(string input)
        {
            process.StandardInput.WriteLine(input);
        }
    }
}
