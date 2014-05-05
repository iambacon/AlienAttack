using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlienAttack.Web.Models
{
    /// <summary>
    /// Move class.
    /// </summary>
    public class Move
    {
        /// <summary>
        /// The position.
        /// </summary>
        public Coordinate Position;

        /// <summary>
        /// The next move.
        /// </summary>
        public string NextMove;
    }
}