using McGuard.src.content;
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

        /// <summary>
        /// Join messages count
        /// </summary>
        private int messagesCount;

        public ConnectionListener(Process process) : base (process)
        {
            this.joinMessage = ConfigManager.GetValueByKey("joinmsg").Split('§');
            messagesCount = joinMessage.Where(msg => msg.Trim().Length > 0).Select(msg => msg).Count();
        }

        /// <summary>
        /// On player connection to server
        /// </summary>
        /// <param name="player">Player instance</param>
        public void OnPlayerConnection(Player player)
        {
            SendMessageToAll(new Message("", 0, structures.text.Color.White, structures.text.Style.None, false));
            SendMessageToAll(new Message("> " + StringManager.GetString(4), StringManager.GetString(4).Length, structures.text.Color.Gray, structures.text.Style.Bold, false));
            SendMessageToAll(new Message("", 0, structures.text.Color.White, structures.text.Style.None, false));

            if (messagesCount > 0)
            {
                foreach (var singleMessage in joinMessage)
                {
                    string msg = singleMessage.Trim();
                    int len = msg.Length;

                    if (len > 0)
                    {
                        SendMessageToPlayer(player, new Message(msg, len, structures.text.Color.White, structures.text.Style.None, true));
                    }
                }

                SendMessageToAll(new Message("", 0, structures.text.Color.White, structures.text.Style.None, false));
            }

            SendMessageToAll(new Message(StringManager.GetString(3).Replace("%s", player.Name), StringManager.GetString(3).Replace("%s", player.Name).Length, structures.text.Color.Gold, structures.text.Style.Italic, false));
        }
    }
}
