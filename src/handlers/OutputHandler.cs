﻿using McGuard.src.listeners;
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
            if (dataReceivedEventArgs != null && dataReceivedEventArgs.Data != null)
            {
                string outputData = dataReceivedEventArgs.Data.Trim();

                if (outputData.Contains("<") && outputData.Contains(">") && outputData.Contains("> " + commandPrefix))
                {
                    string playerName = outputData.Split(new[] { "> " + commandPrefix }, StringSplitOptions.None)[1].Trim();
                    string issuedCommand = outputData.Split(new[] { "> " + commandPrefix }, StringSplitOptions.None)[1].Trim();

                    this.commandListener.OnPlayerCommand(playerName, issuedCommand);

                    return;
                }
                else if (outputData.Contains("logged in"))
                {
                    string pattern = @"(?<playerName>\w+)\[/((?<ipAddress>\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})|.+?):\d+\] logged in with entity id (?<entityId>\d+) at \(\[(?<worldName>\w+)\] (?<xCoord>-?\d+(\.\d+)?), (?<yCoord>-?\d+(\.\d+)?), (?<zCoord>-?\d+(\.\d+)?)\)";

                    Regex regex = new Regex(pattern);

                    Match match = regex.Match(outputData);

                    if (match.Success)
                    {
                        string playerName = match.Groups["playerName"].Value;
                        string ipAddress = match.Groups["ipAddress"].Value;
                        int entityId = int.Parse(match.Groups["entityId"].Value);

                        string xCoordString = match.Groups["xCoord"].Value;
                        string yCoordString = match.Groups["yCoord"].Value;
                        string zCoordString = match.Groups["zCoord"].Value;

                        if (double.TryParse(xCoordString, NumberStyles.Float, CultureInfo.InvariantCulture, out double xCoord) && double.TryParse(yCoordString, NumberStyles.Float, CultureInfo.InvariantCulture, out double yCoord) && double.TryParse(zCoordString, NumberStyles.Float, CultureInfo.InvariantCulture, out double zCoord))
                        {
                            Player player = new Player(entityId, playerName, ipAddress, xCoord, yCoord, zCoord);
                            connectionListener.OnPlayerConnection(player);
                        }
                    }
                    else
                    {
                        Console.WriteLine("The log entry did not match the expected format.");
                    }
                }

                Console.WriteLine(outputData);
            }
        }
    }
}
