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

            return false;
        }

        public void OnPlayerMessage(Message message)
        {
            SendMessageToAll(message);
        }
    }
}
