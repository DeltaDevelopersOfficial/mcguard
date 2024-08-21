using System;
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

        public IPC(Process process, string pipeName)
        {
            this.pipeName = pipeName;
            CommandListener commandListener = new CommandListener(process);
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
                        Console.WriteLine("Client connected to stream!");

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

        void HandlePlayerChat(JsonElement root)
        {
            string playerName = root.TryGetProperty("player_name", out JsonElement playerNameElement) ? playerNameElement.GetString() : "Unknown";
            string playerId = root.TryGetProperty("player_name", out JsonElement playerIdElement) ? playerIdElement.GetString() : "Unknown";
            string playerMessage = root.TryGetProperty("player_message", out JsonElement playerMessageElement) ? playerMessageElement.GetString() : "No message";
            string playerHasOp = root.TryGetProperty("player_has_op", out JsonElement playerHasOpElement) ? playerHasOpElement.GetString() : "unknown";
            bool isPlayerOpped = playerHasOp.Equals("true", StringComparison.OrdinalIgnoreCase);

            string playerFlying = root.TryGetProperty("player_flying", out JsonElement playerFlyingElement) ? playerFlyingElement.GetString() : "unknown";
            bool isPlayerFlying = playerFlying.Equals("true", StringComparison.OrdinalIgnoreCase);

            double coordsX = double.TryParse(root.TryGetProperty("coords_x", out JsonElement coordsXElement) ? coordsXElement.GetString() : "0", out double tempX) ? tempX : 0;
            double coordsY = double.TryParse(root.TryGetProperty("coords_y", out JsonElement coordsYElement) ? coordsYElement.GetString() : "0", out double tempY) ? tempY : 0;
            double coordsZ = double.TryParse(root.TryGetProperty("coords_z", out JsonElement coordsZElement) ? coordsZElement.GetString() : "0", out double tempZ) ? tempZ : 0;

            if (int.TryParse(playerId, out int id))
            {
                structures.chat.Player player = new structures.chat.Player(id, playerName, coordsX, coordsY, coordsZ, isPlayerFlying, isPlayerOpped);

                if (playerMessage.StartsWith("!"))
                {
                    Command command = new Command(playerMessage, playerMessage.Split(' '));
                    commandListener.OnPlayerCommand(player, command);
                }
            }
        }
    }
}
