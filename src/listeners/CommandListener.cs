using McGuard.src.handlers;
using McGuard.src.structures;
using McGuard.src.structures.chat;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McGuard.src.listeners
{
    internal class CommandListener : InputHandler
    {
        public CommandListener(Process process) : base(process)
        {
        }

        /// <summary>
        /// On player chat command receive
        /// </summary>
        /// <param name="player">Player insrance</param>
        /// <param name="command">Command instance</param>
        /// <returns>Return TRUE if was command successfully executed</returns>
        public bool OnPlayerCommand(structures.chat.Player player, Command command)
        {
            if (command.Name == "!killme")
            {
                SendInput("kill " + player.Name);

                return true;
            }

            else if (command.Name == "!whoami")
            {
                string[] listZprav =
                {
                    "",
                    "Info about you:",
                    "  Name: " + player.Name,
                    "  ID: " + player.Id,
                    "  Location: " + player.PositionX + ", " + player.PositionY + ", " + player.PositionZ,
                    "  Admin: " + player.IsOpped.ToString().ToLower(),
                    ""
                };

                foreach (var zprava in listZprav)
                {
                    SendMessageToPlayer(player, new Message(zprava, zprava.Length, structures.text.Color.White, structures.text.Style.None, false));
                }

                return true;
            }

            return false;
        }

        public void OnPlayerMessage(Message message)
        {
            SendMessageToAll(message);
        }
    }
}
