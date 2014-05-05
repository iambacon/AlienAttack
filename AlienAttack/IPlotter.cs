using System.Collections.Generic;

namespace AlienAttack
{
    /// <summary>
    /// Interface for plotting 2D coordinates.
    /// </summary>
    public interface IPlotter
    {
        /// <summary>
        /// Gets the orientation.
        /// </summary>
        /// <value>
        /// The orientation.
        /// </value>
        int Orientation { get; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        Coordinate Position { get; set; }

        /// <summary>
        /// Calculates the orientation based on the move.
        /// </summary>
        /// <param name="move">The move.</param>
        void CalculateOrientation(string move);

        /// <summary>
        /// Increment position.
        /// </summary>
        void Forward();

        /// <summary>
        /// Plots the move.
        /// </summary>
        /// <param name="move">The move.</param>
        void PlotMove(string move);

        /// <summary>
        /// Plots the moves.
        /// </summary>
        /// <param name="moves">The moves.</param>
        List<Position> PlotMoves(List<string> moves);

        /// <summary>
        /// Turns left.
        /// </summary>
        void TurnLeft();

        /// <summary>
        /// Turns right.
        /// </summary>
        void TurnRight();
    }
}
