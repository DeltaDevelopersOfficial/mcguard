using McGuard.src.handlers;
using McGuard.src.structures;
using McGuard.src.utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McGuard.src.listeners
{
    internal class ConnectionListener : InputHandler
    {
        /// <summary>
        /// Array of join messages
        /// </summary>
        private readonly string[] joinMessage;

        public ConnectionListener(Process process) : base (process)
        {
            this.joinMessage = ConfigManager.GetValueByKey("joinmsg").Split('§');
        }

        /// <summary>
        /// On player connection to server
        /// </summary>
        /// <param name="player">Player instance</param>
        public void OnPlayerConnection(Player player)
        {
            foreach (var singleMessage in joinMessage)
            {
                SendMessageToPlayer(player, new Message(singleMessage, singleMessage.Length, structures.text.Color.White, structures.text.Style.None, true));
            }
        }
    }
}
