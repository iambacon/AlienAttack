using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AlienAttack.Tests
{
    [TestClass]
    public class PlotterTests
    {
        private IPlotter plotter;

        [TestInitialize]
        public void Init()
        {
            plotter = new Plotter { Position = new Coordinate { x = 0, y = 1 } };
        }

        [TestMethod]
        public void Forward_should_increament_the_x_or_y_position_by_one_unit()
        {
            // Arrange

            // Act
            plotter.Forward();

            // Assert
            Assert.AreEqual(2, plotter.Position.y);
        }

        [TestMethod]
        public void TurnLeft_should_decrease_the_angle_by_ninety_degrees()
        {
            // Arrange

            // Act
            plotter.TurnLeft();

            // Assert
            Assert.AreEqual(270, plotter.Orientation);
        }

        [TestMethod]
        public void TurnRight_should_increase_the_angle_by_ninety_degrees()
        {
            // Arrange

            // Act
            plotter.TurnRight();

            // Assert
            Assert.AreEqual(90, plotter.Orientation);
        }

        [TestMethod]
        public void PlotMoves_should_set_coordinates_x1_y2()
        {
            // Arrange
            var expectedCoordinate = new Coordinate { x = 1, y = 2 };

            var moves = new List<string>
            {
                "FORWARD",
                "RIGHT",
                "FORWARD"
            };

            // Act
            plotter.PlotMoves(moves);

            // Assert
            Assert.AreEqual(expectedCoordinate.x, plotter.Position.x);
            Assert.AreEqual(expectedCoordinate.y, plotter.Position.y);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "x position out of bounds")]
        public void PlotMoves_should_throw_exception_if_coordinates_out_of_bounds()
        {
            // Arrange
            var moves = new List<string>
            {
                "FORWARD",
                "LEFT",
                "FORWARD"
            };

            // Act
            plotter.PlotMoves(moves);

            // Assert
        }

        [TestMethod]
        public void CalculateOrientation_should_set_orientation_to_0_degrees()
        {
            // Arrange
            const int expectedValue = 0;

            // Act
            plotter.CalculateOrientation("FORWARD");

            // Assert
            Assert.AreEqual(expectedValue, plotter.Orientation);
        }

        [TestMethod]
        public void CalculateOrientation_should_set_orientation_to_90_degrees()
        {
            // Arrange
            const int expectedValue = 90;
            plotter.Position = new Coordinate { x = 1, y = 0 };

            // Act
            plotter.CalculateOrientation("FORWARD");

            // Assert
            Assert.AreEqual(expectedValue, plotter.Orientation);
        }

        [TestMethod]
        public void Should_set_coordinates_x1_y1()
        {
            // Arrange
            const string json = @"{'Directions':['FORWARD', 'FORWARD', 'RIGHT', 'FORWARD', 'FORWARD', 'FORWARD', " +
                "'FORWARD', 'RIGHT', 'FORWARD', 'FORWARD', 'LEFT', 'FORWARD', 'RIGHT', 'LEFT', 'LEFT', 'FORWARD', 'FORWARD', 'FORWARD', 'LEFT', 'RIGHT']}";

            var data = JObject.Parse(json)["Directions"];
            var directions = JsonConvert.DeserializeObject<List<string>>(data.ToString());

            plotter.Position = new Coordinate { x = 0, y = 4 };
            plotter.CalculateOrientation(directions.First());

            directions.RemoveAt(0);

            // Act
            plotter.PlotMoves(directions);

            // Assert
            Assert.AreEqual(5, plotter.Position.x);
            Assert.AreEqual(6, plotter.Position.y);
        }
    }
}
