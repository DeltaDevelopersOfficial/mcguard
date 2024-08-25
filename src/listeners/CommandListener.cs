using McGuard.src.content;
using McGuard.src.core;
using McGuard.src.core.providers;
using McGuard.src.handlers;
using McGuard.src.structures;
using McGuard.src.structures.chat;
using McGuard.src.utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace McGuard.src.listeners
{
    internal class CommandListener : InputHandler
    {
        /// <summary>
        /// Process instance
        /// </summary>
        private Process process;

        /// <summary>
        /// Startup of server
        /// </summary>
        private DateTime startupTime;

        public CommandListener(Process process) : base(process)
        {
            this.process = process;
            this.startupTime = DateTime.Now;
        }

        /// <summary>
        /// On player chat command receive
        /// </summary>
        /// <param name="player">Player insrance</param>
        /// <param name="command">Command instance</param>
        /// <returns>Return TRUE if was command successfully executed</returns>
        public bool OnPlayerCommand(structures.chat.Player player, Command command)
        {
            #region Commands for everyone

            //
            // !killme
            //
            // Kills you, it's also available for non OP players, because for
            // origin kill you need have OP rights
            //
            if (command.Name == "!killme")
            {
                SendInput("kill " + player.Name);

                return true;
            }

            //
            // !info
            //
            // Shows you quick information about server
            //
            if (command.Name == "!info")
            {
                TimeSpan difference = DateTime.Now - startupTime;

                int totalDays = difference.Days;
                int totalHours = (int)difference.TotalHours;
                int totalMinutes = (int)difference.TotalMinutes;

                int days = totalDays;
                int hours = totalHours % 24;
                int minutes = totalMinutes % 60;

                string[] listZprav =
                {
                    "",
                    "Server info:",
                    "  Server tool: " + StringManager.GetString(9),
                    "  Uptime: " + days + " days " + hours + " hours " + minutes + " minutes",
                    ""
                };

                foreach (var zprava in listZprav)
                {
                    SendMessageToPlayer(player, new Message(zprava, zprava.Length, structures.text.Color.White, structures.text.Style.None, false));
                }

                return true;
            }

            //
            // !whoami
            //
            // Command to get informations about YOU (like coords, name, and if u had OP)
            //
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

            #endregion

            #region Commands for admins

            //
            // !help
            //
            // Just a command for show all available commands
            //
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
                        "   !info - Show information about server",
                        "",
                        " for admins:",
                        "   !help - Show all available commands",
                        "   !kick [name] - Kicks player from server",
                        "   !macro [filename] - Kicks player from server",
                        "   !setloc [savename] - Create point to late teleport to it",
                        "   !tp [savename] - Teleports you to saved point",
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

            //
            // !kick [playername]
            //
            // Command for kick player, simple for use
            //
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

            //
            // !macro [filename]
            //
            // Macro is a simple sequence of commands presented in file.
            // Script files are stored in macros/ directory. (only accepted extension is *.txt)
            //
            else if (command.Name.StartsWith("!macro"))
            {
                if (player.IsOpped)
                {
                    if (command.Arguments.Length > 1)
                    {
                        string fileName = command.Arguments[1].ToLower();

                        if (!fileName.EndsWith(".txt"))
                        {
                            fileName += ".txt";
                        }

                        MacroProvider mp = new MacroProvider(process, fileName);

                        if (mp.Exists())
                        {
                            if (mp.IsEmpty())
                            {
                                SendMessageToPlayer(player, new Message(StringManager.GetString(7).Replace("%s", fileName), StringManager.GetString(7).Replace("%s", fileName).Length, structures.text.Color.White, structures.text.Style.None, true));
                            }
                            else
                            {
                                SendMessageToPlayer(player, new Message(StringManager.GetString(5).Replace("%s", fileName), StringManager.GetString(5).Replace("%s", fileName).Length, structures.text.Color.White, structures.text.Style.None, true));
                                mp.Execute();
                            }
                        }
                        else
                        {
                            SendMessageToPlayer(player, new Message(StringManager.GetString(6).Replace("%s", fileName), StringManager.GetString(6).Replace("%s", fileName).Length, structures.text.Color.White, structures.text.Style.None, true));
                        }
                    }
                    else
                    {
                        SendMessageToPlayer(player, new Message(StringManager.GetString(8), StringManager.GetString(8).Length, structures.text.Color.White, structures.text.Style.None, true));
                    }
                }
                else
                {
                    SendMessageToPlayer(player, new Message(StringManager.GetString(0), StringManager.GetString(0).Length, structures.text.Color.White, structures.text.Style.None, true));
                }

                return true;
            }

            //
            // !setloc [name]
            //
            // Sets a specificed location to late teleport to it
            //
            else if (command.Name.StartsWith("!setloc"))
            {
                if (player.IsOpped)
                {
                    if (command.Arguments.Length > 1)
                    {
                        string locationName = command.Arguments[1].ToLower();

                        bool rewrite = false;

                        if (command.Arguments.Length > 2)
                        {
                            if (command.Arguments[2].ToLower().Contains("--rewrite"))
                            {
                                rewrite = true;
                            }
                        }

                        // just for security
                        locationName = CryptographyStrategy.GetSha1FromText(locationName);

                        if (!Directory.Exists(Environment.CurrentDirectory + "\\locations"))
                        {
                            Directory.CreateDirectory(Environment.CurrentDirectory + "\\locations");
                        }

                        string filePath = Environment.CurrentDirectory + "\\locations\\" + locationName;

                        if (!rewrite)
                        {
                            if (!File.Exists(filePath))
                            {
                                try
                                {
                                    File.WriteAllText(filePath, player.PositionX + "," + player.PositionY + "," + player.PositionZ);
                                    SendMessageToPlayer(player, new Message(StringManager.GetString(1), StringManager.GetString(1).Length, structures.text.Color.White, structures.text.Style.None, true));
                                }
                                catch
                                {
                                    SendMessageToPlayer(player, new Message(StringManager.GetString(10).Replace("%s", "1972"), StringManager.GetString(10).Replace("%s", "1972").Length, structures.text.Color.White, structures.text.Style.None, true));
                                }
                            }
                            else
                            {
                                SendMessageToPlayer(player, new Message(StringManager.GetString(11).Replace("%s", "Location").Replace("%d", "already"), StringManager.GetString(11).Replace("%s", "Location").Replace("%d", "already").Length, structures.text.Color.White, structures.text.Style.None, true));
                            }
                        }
                        else
                        {
                            File.WriteAllText(filePath, player.PositionX + "," + player.PositionY + "," + player.PositionZ);
                            SendMessageToPlayer(player, new Message(StringManager.GetString(1), StringManager.GetString(1).Length, structures.text.Color.White, structures.text.Style.None, true));
                        }
                    }
                    else
                    {
                        SendMessageToPlayer(player, new Message(StringManager.GetString(8), StringManager.GetString(8).Length, structures.text.Color.White, structures.text.Style.None, true));
                    }
                }
                else
                {
                    SendMessageToPlayer(player, new Message(StringManager.GetString(0), StringManager.GetString(0).Length, structures.text.Color.White, structures.text.Style.None, true));
                }

                return true;
            }

            //
            // !tp [name]
            //
            // Teleports you to saved location by command !setloc [name]
            //
            else if (command.Name.StartsWith("!tp"))
            {
                if (player.IsOpped)
                {
                    if (command.Arguments.Length > 1)
                    {
                        string locationName = command.Arguments[1].ToLower();

                        // just for security
                        locationName = CryptographyStrategy.GetSha1FromText(locationName);

                        if (!Directory.Exists(Environment.CurrentDirectory + "\\locations"))
                        {
                            Directory.CreateDirectory(Environment.CurrentDirectory + "\\locations");
                        }

                        string filePath = Environment.CurrentDirectory + "\\locations\\" + locationName;

                        if (File.Exists(filePath))
                        {
                            string plainCoords = File.ReadAllText(filePath);

                            // get coords, X, Y, Z, split by ","
                            string[] coords = plainCoords.Split(',');

                            // checks if it's valid format of coords
                            if (coords.Length == 3)
                            {
                                SendInput("tp " + player.Name + " " + coords[0] + " " + coords[1] + " " + coords[2]);
                                SendMessageToPlayer(player, new Message(StringManager.GetString(1), StringManager.GetString(1).Length, structures.text.Color.White, structures.text.Style.None, true));
                            }
                            else
                            {
                                SendMessageToPlayer(player, new Message(StringManager.GetString(13), StringManager.GetString(13).Length, structures.text.Color.White, structures.text.Style.None, true));
                            }
                        }
                        else
                        {
                            SendMessageToPlayer(player, new Message(StringManager.GetString(11).Replace("%s", "Location").Replace("%d", "not"), StringManager.GetString(11).Replace("%s", "Location").Replace("%d", "already").Length, structures.text.Color.White, structures.text.Style.None, true));
                        }
                    }
                    else
                    {
                        SendMessageToPlayer(player, new Message(StringManager.GetString(8), StringManager.GetString(8).Length, structures.text.Color.White, structures.text.Style.None, true));
                    }
                }
                else
                {
                    SendMessageToPlayer(player, new Message(StringManager.GetString(0), StringManager.GetString(0).Length, structures.text.Color.White, structures.text.Style.None, true));
                }

                return true;
            }

            #endregion

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
