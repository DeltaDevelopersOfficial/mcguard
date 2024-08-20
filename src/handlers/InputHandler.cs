using McGuard.src.structures;
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

        /// <summary>
        /// Sends a message to a player
        /// </summary>
        /// <param name="player">Player to send to</param>
        /// <param name="message">Message content</param>
        /// <param name="color">Color of message (white default)</param>
        /// <param name="isServerMessage">Add server prefix to message</param>
        public void SendMessageToPlayer(Player player, string message, bool isServerMessage = true, string color = "white")
        {
            // to avoid presention of " in JSON
            // replaces " as \"
            message = message.Replace("\"", "\\\"");

            process.StandardInput.WriteLine("tellraw " + player.Name + " {\"text\":\"" + message.Trim() + "\", \"color\":\"" + color + "\"}");
        }
    }
}
