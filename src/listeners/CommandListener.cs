using McGuard.src.content;
using McGuard.src.core;
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

            else if (command.Name == "!help")
            {
                if (player.IsOpped)
                {
                    string[] listZprav =
                    {
                        "",
                        "Available commands:",
                        " for everyone:",
                        "   !killme - Kills you",
                        "   !whoami - Show information about you",
                        "",
                        " for admins:",
                        "   !help - Show all available commands",
                        "   !kick [name] - Kicks player from server",
                        "",
                    };

                    foreach (var zprava in listZprav)
                    {
                        SendMessageToPlayer(player, new Message(zprava, zprava.Length, structures.text.Color.White, structures.text.Style.None, false));
                    }

                    return true;
                }
                else
                {
                    SendMessageToPlayer(player, new Message(StringManager.GetString(0), StringManager.GetString(0).Length, structures.text.Color.White, structures.text.Style.None, true));
                }

                return true;
            }

            else if (command.Name.Contains("!kick"))
            {
                List<structures.Player> selectedPlayers = PlayerManager.FindPlayer(command.Arguments[1]);

                if (player.IsOpped)
                {
                    if (selectedPlayers.Count > 0)
                    {
                        SendMessageToPlayer(player, new Message(StringManager.GetString(1), StringManager.GetString(1).Length, structures.text.Color.White, structures.text.Style.None, true));
                        SendInput("kick " + command.Arguments[1]);
                    }
                    else
                    {
                        SendMessageToPlayer(player, new Message(StringManager.GetString(2), StringManager.GetString(2).Length, structures.text.Color.White, structures.text.Style.None, true));
                    }
                }
                else
                {
                    SendMessageToPlayer(player, new Message(StringManager.GetString(0), StringManager.GetString(0).Length, structures.text.Color.White, structures.text.Style.None, true));
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// On player chat message receive
        /// </summary>
        /// <param name="message"></param>
        public void OnPlayerMessage(Message message)
        {
            //
            // send message to all other players
            //
            SendMessageToAll(message);
        }
    }
}
