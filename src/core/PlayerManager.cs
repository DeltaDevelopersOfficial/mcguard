using McGuard.src.structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McGuard.src.core
{
    internal class PlayerManager
    {
        /// <summary>
        /// List of players
        /// </summary>
        private static List<Player> playerList = new List<Player>();

        /// <summary>
        /// Adds player to list
        /// </summary>
        /// <param name="player">Player instance</param>
        public static void AddPlayer(Player player)
        {
            if (!playerList.Contains(player))
            {
                playerList.Add(player);
            }
        }

        /// <summary>
        /// Removes player from list
        /// </summary>
        /// <param name="player"></param>
        public static void RemovePlayer(Player player)
        {
            if (playerList.Contains(player))
            {
                playerList.Remove(player);
            }
        }

        /// <summary>
        /// Tries to find a player in list
        /// </summary>
        /// <param name="playerName">Player instance</param>
        public static List<Player> FindPlayer(string playerName)
        {
            return playerList.Where(p => p.Name == playerName).Select(p => p).ToList();
        }
    }
}
