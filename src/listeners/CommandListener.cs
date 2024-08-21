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
        /// <param name="playerName"></param>
        /// <param name="issuedCommand"></param>
        public void OnPlayerCommand(structures.chat.Player player, Command command)
        {
            if (command.Name == "test")
                SendInput("say OK!");
        }
    }
}
