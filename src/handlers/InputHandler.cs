using McGuard.src.structures;
using McGuard.src.structures.text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McGuard.src.handlers
{
    internal class InputHandler
    {
        /// <summary>
        /// Server process instance
        /// </summary>
        private readonly Process process;

        public InputHandler(Process process)
        {
            this.process = process;
        }

        /// <summary>
        /// Sends input to an process.
        /// </summary>
        /// <param name="input"></param>
        public void SendInput(string input)
        {
            process.StandardInput.WriteLine(input);
        }

        /// <summary>
        /// Sends a message to a player
        /// </summary>
        /// <param name="player">Player to send to</param>
        /// <param name="message">Message instance</param>
        public void SendMessageToPlayer(Player player, Message message)
        {
            var styleMap = new Dictionary<Style, string>
            {
                { Style.Bold, "\"bold\":true" },
                { Style.Italic, "\"italic\":true" },
                { Style.Underlined, "\"underlined\":true" },
                { Style.Strikethrough, "\"strikethrough\":true" },
                { Style.Obfuscated, "\"obfuscated\":true" }
            };

            string styleJson = string.Join(",", styleMap.Where(kvp => message.Style.HasFlag(kvp.Key) && message.Style != Style.None).Select(kvp => kvp.Value));
            string escapedContent = message.Content.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\b", "\\b").Replace("\f", "\\f").Replace("\n", "\\n").Replace("\r", "\\r").Replace("\t", "\\t");
            string color = message.Color.ToString().ToLower();
            string json = $"{{\"text\":\"{(message.IsServerMessage ? "[Server] " : "")}{escapedContent}\"," + $"\"color\":\"{color}\"" + $"{(string.IsNullOrEmpty(styleJson) ? "" : $",{styleJson}")}}}";

            process.StandardInput.WriteLine($"tellraw {player.Name} {json}");
        }
    }
}
