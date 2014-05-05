using System;
using System.Collections.Generic;

namespace AlienAttack
{
    /// <summary>
    /// Plotter class. 
    /// Plots and tracks 2D coordinates and direction.
    /// </summary>
    public class Plotter : IPlotter
    {
        /// <summary>
        /// Gets the orientation.
        /// </summary>
        /// <value>
        /// The orientation.
        /// </value>
        public int Orientation { get; private set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public Coordinate Position { get; set; }

        private const int MaxUnitsX = 10;
        private const int MaxUnitsY = 10;
        private const int MinUnitsX = 0;
        private const int MinUnitsY = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Plotter"/> class.
        /// </summary>
        public Plotter()
        {
            this.Position = new Coordinate();
        }

        #region Public Methods
        /// <summary>
        /// Increment position by one unit.
        /// </summary>
        public void Forward()
        {
            this.Move(1);
        }

        /// <summary>
        /// Plots the move.
        /// </summary>
        /// <param name="move">The move.</param>
        public void PlotMove(string move)
        {
            switch (move.ToUpper())
            {
                case "FORWARD":
                    this.Forward();
                    break;
                case "LEFT":
                    this.TurnLeft();
                    break;
                case "RIGHT":
                    this.TurnRight();
                    break;
            }

            if (this.Position.x < MinUnitsX || this.Position.x > MaxUnitsX)
            {
                throw new Exception("x position out of bounds");
            }

            if (this.Position.y < MinUnitsY || this.Position.y > MaxUnitsY)
            {
                throw new Exception("y position out of bounds");
            }
        }

        /// <summary>
        /// Plots the moves.
        /// </summary>
        /// <param name="moves">The moves.</param>
        public List<Position> PlotMoves(List<string> moves)
        {
            var positions = new List<Position>();

            foreach (var move in moves)
            {
                this.PlotMove(move);
                positions.Add(new Position
                {
                    Orientation = this.Orientation,
                    Coordinate = new Coordinate
                    {
                        x = this.Position.x,
                        y = this.Position.y
                    }
                });
            }

            return positions;
        }

        /// <summary>
        /// Sets orientation left by 90 degrees.
        /// </summary>
        public void TurnLeft()
        {
            this.Rotate(-90);
        }

        /// <summary>
        /// Sets orientation right by 90 degrees.
        /// </summary>
        public void TurnRight()
        {
            this.Rotate(90);
        }

        /// <summary>
        /// Calculates the orientation based on the first move.
        /// </summary>
        /// <param name="move">The move.</param>
        public void CalculateOrientation(string move)
        {
            if (move == "FORWARD")
            {
                if (this.Position.x > 0)
                {
                    this.Orientation = 90;
                    return;
                }

                if (this.Position.y > 0)
                {
                    this.Orientation = 0;
                }
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Sets orientation value by the specified degrees.
        /// </summary>
        /// <param name="degrees">The degrees.</param>
        private void Rotate(int degrees)
        {
            this.Orientation += degrees;

            if (this.Orientation < 0)
            {
                this.Orientation += 360;
            }

            if (this.Orientation >= 360)
            {
                this.Orientation -= 360;
            }
        }

        /// <summary>
        /// Moves the x or y position by the specified unit.
        /// </summary>
        /// <param name="unit">The unit.</param>
        private void Move(int unit)
        {
            switch (this.Orientation)
            {
                case 0:
                    this.Position.y += unit;
                    break;
                case 90:
                    this.Position.x += unit;
                    break;
                case 180:
                    this.Position.y -= unit;
                    break;
                case 270:
                    this.Position.x -= unit;
                    break;
            }
        }
        #endregion
    }
}
