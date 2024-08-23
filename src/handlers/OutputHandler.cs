using McGuard.src.listeners;
using McGuard.src.structures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace McGuard.src.handlers
{
    internal class OutputHandler
    {
        /// <summary>
        /// Instance of command listener
        /// </summary>
        private CommandListener commandListener;

        /// <summary>
        /// Instance of connection listener
        /// </summary>
        private ConnectionListener connectionListener;

        private readonly char commandPrefix = '!';

        public OutputHandler(Process serverProcess)
        {
            this.commandListener = new CommandListener(serverProcess);
            this.connectionListener = new ConnectionListener(serverProcess);
        }

        /// <summary>
        /// On data receive from console output
        /// </summary>
        /// <param name="dataReceivedEventArgs"></param>
        public void OnDataReceive(DataReceivedEventArgs dataReceivedEventArgs)
        {
            Console.WriteLine(dataReceivedEventArgs?.Data);
        }
    }
}
