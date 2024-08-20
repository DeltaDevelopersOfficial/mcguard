﻿using System;
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
        public int Id;

        /// <summary>
        /// Player name
        /// </summary>
        public string Name;

        /// <summary>
        /// Player IP address
        /// </summary>
        public string IpAddress;

        /// <summary>
        /// Coord X
        /// </summary>
        public double PositionX;

        /// <summary>
        /// Coord Y
        /// </summary>
        public double PositionY;

        /// <summary>
        /// Coord Z
        /// </summary>
        public double PositionZ;
    }
}
