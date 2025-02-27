﻿using McGuard.src.content;
using McGuard.src.handlers;
using McGuard.src.io;
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
        public ConnectionListener(Process process) : base (process)
        {
        }

        /// <summary>
        /// On player connection to server
        /// </summary>
        /// <param name="player">Player instance</param>
        public void OnPlayerConnection(Player player)
        {
            string[] joinMessage = joinMessage = ConfigManager.GetValueByKey("joinmsg").Split('§');
            int messagesCount = joinMessage.Where(msg => msg.Trim().Length > 0).Select(msg => msg).Count();

            SendMessageToPlayer(player, new Message("", 0, structures.text.Color.White, structures.text.Style.None, false));
            SendMessageToPlayer(player, new Message("> " + StringManager.GetString(4), StringManager.GetString(4).Length, structures.text.Color.Gray, structures.text.Style.Bold, false));
            SendMessageToPlayer(player, new Message("", 0, structures.text.Color.White, structures.text.Style.None, false));

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

                SendMessageToPlayer(player, new Message("", 0, structures.text.Color.White, structures.text.Style.None, false));
            }

            SendMessageToAll(new Message(StringManager.GetString(3).Replace("%s", player.Name), StringManager.GetString(3).Replace("%s", player.Name).Length, structures.text.Color.Gold, structures.text.Style.Italic, false));

            FileManager.LogFileLine("connections.txt", "[" + DateTime.Now + "] " + player.Name + " / " + ((player.IpAddress.StartsWith("/")) ? player.IpAddress.Substring(1).Trim() : player.IpAddress) + " / " + player.Id);
        }
    }
}
