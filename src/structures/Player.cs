using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McGuard.src.structures
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
        /// Player IP address
        /// </summary>
        public string IpAddress { get; private set; }

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

        public Player(int id, string name, string ipAddress, double xPosition, double yPosition, double zPosition)
        {
            Id = id;
            Name = name;
            IpAddress = ipAddress;
            PositionX = xPosition;
            PositionY = yPosition;
            PositionZ = zPosition;
        }
    }
}
