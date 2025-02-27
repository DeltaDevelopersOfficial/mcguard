﻿using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using McGuard.src.listeners;
using System.Diagnostics;
using McGuard.src.structures;
using McGuard.src.structures.chat;
using McGuard.src.core;
using McGuard.src.content;
using McGuard.src.structures.enums;

namespace McGuard.src.utils
{
    internal class IPC
    {
        /// <summary>
        /// Process id as named pipe name
        /// </summary>
        private readonly string pipeName;

        /// <summary>
        /// Command listener instance
        /// </summary>
        private CommandListener commandListener;

        /// <summary>
        /// Player manager instance
        /// </summary>
        private PlayerManager playerManager;

        /// <summary>
        /// Connection listener instance
        /// </summary>
        private ConnectionListener connectionListener;

        public IPC(Process process, string pipeName)
        {
            this.pipeName = pipeName;
            this.commandListener = new CommandListener(process);
            this.playerManager = new PlayerManager();
            this.connectionListener = new ConnectionListener(process);
        }

        /// <summary>
        /// Starts the named pipe communication
        /// </summary>
        public void Start()
        {
            while (true)
            {
                try
                {
                    using (NamedPipeServerStream pipeServer = new NamedPipeServerStream(pipeName, PipeDirection.In, 1))
                    {
                        pipeServer.WaitForConnection();

                        using (StreamReader reader = new StreamReader(pipeServer, Encoding.UTF8))
                        {
                            string message;

                            while ((message = reader.ReadLine()) != null)
                            {
                                ProcessDynamicJson(message);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Errno! " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Parse dynamic JSON string
        /// </summary>
        /// <param name="jsonString"></param>
        void ProcessDynamicJson(string jsonString)
        {
            using (JsonDocument doc = JsonDocument.Parse(jsonString))
            {
                JsonElement root = doc.RootElement;

                if (root.TryGetProperty("action", out JsonElement actionElement))
                {
                    string action = actionElement.GetString();

                    switch (action)
                    {
                        case "handle_chat":
                            HandlePlayerChat(root);
                            break;

                        case "handle_playerjoin":
                            HandlePlayerJoin(root);
                            break;

                        case "handle_playerquit":
                            HandlePlayerQuit(root);
                            break;

                        default:
                            Console.WriteLine("Unknown action");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("No action key found in JSON.");
                }
            }
        }

        /// <summary>
        /// Handle player join server
        /// </summary>
        /// <param name="root">JSON element</param>
        void HandlePlayerJoin(JsonElement root)
        {
            string playerName = root.TryGetProperty("player_name", out JsonElement playerNameElement) ? playerNameElement.GetString() : "Unknown";
            string playerId = root.TryGetProperty("player_id", out JsonElement playerIdElement) ? playerIdElement.GetString() : "Unknown";
            string playerIp = root.TryGetProperty("player_ip", out JsonElement playerIpElement) ? playerIpElement.GetString() : "Unknown";
            string playerHasOp = root.TryGetProperty("player_has_op", out JsonElement playerHasOpElement) ? playerHasOpElement.GetString() : "unknown";
            bool isPlayerOpped = playerHasOp.Equals("true", StringComparison.OrdinalIgnoreCase);

            double coordsX = int.Parse(root.GetProperty("coords_x").ToString().Split('.')[0].Trim());
            double coordsY = int.Parse(root.GetProperty("coords_y").ToString().Split('.')[0].Trim());
            double coordsZ = int.Parse(root.GetProperty("coords_z").ToString().Split('.')[0].Trim());

            if (int.TryParse(playerId, out int id))
            {
                structures.Player player = new structures.Player(id, playerName, playerIp, coordsX, coordsY, coordsZ);
                PlayerManager.AddPlayer(player);
                connectionListener.OnPlayerConnection(player);
            }
        }

        /// <summary>
        /// Handle player quit server
        /// </summary>
        /// <param name="root">JSON element</param>
        void HandlePlayerQuit(JsonElement root)
        {
            string playerName = root.TryGetProperty("player_name", out JsonElement playerNameElement) ? playerNameElement.GetString() : "Unknown";
            string playerId = root.TryGetProperty("player_id", out JsonElement playerIdElement) ? playerIdElement.GetString() : "Unknown";
            
            if (int.TryParse(playerId, out int id))
            {
                foreach (var player in PlayerManager.FindPlayer(playerName))
                {
                    PlayerManager.RemovePlayer(player);
                }
            }
        }

        /// <summary>
        /// Handle player chat entry
        /// </summary>
        /// <param name="root">JSON element</param>
        void HandlePlayerChat(JsonElement root)
        {
            string playerName = root.TryGetProperty("player_name", out JsonElement playerNameElement) ? playerNameElement.GetString() : "Unknown";
            string playerId = root.TryGetProperty("player_id", out JsonElement playerIdElement) ? playerIdElement.GetString() : "Unknown";
            string playerMessage = root.TryGetProperty("player_message", out JsonElement playerMessageElement) ? playerMessageElement.GetString() : "No message";
            string playerHasOp = root.TryGetProperty("player_has_op", out JsonElement playerHasOpElement) ? playerHasOpElement.GetString() : "unknown";
            bool isPlayerOpped = playerHasOp.Equals("true", StringComparison.OrdinalIgnoreCase);

            string playerFlying = root.TryGetProperty("player_flying", out JsonElement playerFlyingElement) ? playerFlyingElement.GetString() : "unknown";
            bool isPlayerFlying = playerFlying.Equals("true", StringComparison.OrdinalIgnoreCase);

            double coordsX = int.Parse(root.GetProperty("coords_x").ToString().Split('.')[0].Trim());
            double coordsY = int.Parse(root.GetProperty("coords_y").ToString().Split('.')[0].Trim());
            double coordsZ = int.Parse(root.GetProperty("coords_z").ToString().Split('.')[0].Trim());

            if (int.TryParse(playerId, out int id))
            {
                structures.chat.Player player = new structures.chat.Player(id, playerName, coordsX, coordsY, coordsZ, isPlayerFlying, isPlayerOpped);

                if (playerMessage.StartsWith("!"))
                {
                   Command command = new Command(playerMessage, playerMessage.Split(' '));

                    CommandResult commandResult = commandListener.OnPlayerCommand(player, command);

                    if (commandResult.HasFlag(CommandResult.Failed))
                    {
                        commandListener.SendMessageToPlayer(player, new Message(StringManager.GetString(12).Replace("%s", command.Name), StringManager.GetString(12).Replace("%s", command.Name).Length, structures.text.Color.White, structures.text.Style.None, true));
                    }
                    else if (commandResult.HasFlag(CommandResult.Success))
                    {
                        if (commandResult.HasFlag(CommandResult.NotAvailableFromGame))
                        {
                            commandListener.SendMessageToPlayer(player, new Message(StringManager.GetString(14).Replace("%s", "in-game chat"), StringManager.GetString(14).Replace("%s", "in-game chat").Length, structures.text.Color.White, structures.text.Style.None, true));
                        }
                    }
                }
                else
                {
                    commandListener.OnPlayerMessage(player, new Message(playerMessage, playerMessage.Length, structures.text.Color.White, structures.text.Style.None, false));
                }
            }
        }
    }
}
