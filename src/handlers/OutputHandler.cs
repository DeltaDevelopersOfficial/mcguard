using McGuard.src.listeners;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McGuard.src.handlers
{
    internal class OutputHandler
    {
        /// <summary>
        /// Instance of command listener
        /// </summary>
        private CommandListener commandListener;

        private readonly char commandPrefix = '!';

        public OutputHandler(Process serverProcess)
        {
            this.commandListener = new CommandListener(serverProcess);
        }

        /// <summary>
        /// On data receive from console output
        /// </summary>
        /// <param name="dataReceivedEventArgs"></param>
        public void OnDataReceive(DataReceivedEventArgs dataReceivedEventArgs)
        {
            if (dataReceivedEventArgs != null && dataReceivedEventArgs.Data != null)
            {
                string outputData = dataReceivedEventArgs.Data.Trim();

                if (outputData.Contains("<") && outputData.Contains(">") && outputData.Contains("> " + commandPrefix))
                {
                    string playerName = outputData.Split(new[] { "> " + commandPrefix }, StringSplitOptions.None)[1].Trim();
                    string issuedCommand = outputData.Split(new[] { "> " + commandPrefix }, StringSplitOptions.None)[1].Trim();

                    this.commandListener.OnPlayerCommand(playerName, issuedCommand);
                }
            }
        }
    }
}
