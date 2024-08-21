using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McGuard.src.structures.chat
{
    internal struct Player
    {
        /// <summary>
        /// Identificator
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Player name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Coord X
        /// </summary>
        public double PositionX { get; private set; }

        /// <summary>
        /// Coord Y
        /// </summary>
        public double PositionY { get; private set; }

        /// <summary>
        /// Coord Z
        /// </summary>
        public double PositionZ { get; private set; }

        /// <summary>
        /// Is opped (has permissions)
        /// </summary>
        public bool IsOpped;

        /// <summary>
        /// Player are currently flying
        /// </summary>
        public bool IsFlying;

        public Player(int id, string name, double xPosition, double yPosition, double zPosition, bool isFlying, bool isOpped)
        {
            Id = id;
            Name = name;
            PositionX = xPosition;
            PositionY = yPosition;
            PositionZ = zPosition;
            IsFlying = isFlying;
            IsOpped = isOpped;
        }
    }
}
