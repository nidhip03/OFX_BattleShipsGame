using NUnit.Framework;
using OFX_BattleShipsGame.StateTrackerAPI.Classes;
using OFX_BattleShipsGame.StateTrackerAPI.Enums;
using OFX_BattleShipsGame.App;
using OFX_BattleShipsGame.StateTrackerAPI;

namespace OFX_BattleShipsGame.Tests
{
    [TestFixture]
    public class Tests
    {
        #region User Tests
        //Checking If Coordinates received are correct ---
        [TestCase("A8, R", 1, 8)]
        [TestCase("A3, D", 1, 3)]
        [TestCase("C1, U", 3, 1)]
        [TestCase("F3, L", 6, 3)]
        [TestCase("G9, R", 7, 9)]

        [Test]
        public void IsCoordinates_Correct(string inputs, int x, int y)
        {
            Assert.AreEqual(new Coordinates(x, y), Inputs.GetLocation(inputs).Coordinate);
        }

        //Checking If Coordinates received are not correct locations---
        [TestCase("Z8, R")]
        [TestCase("A8, O")]
        [TestCase("U2, P")]
        [TestCase("S0, K")]

        [Test]
        public void IsCoordinates_Incorrect(string inputs)
        {
            Assert.IsNull(Inputs.GetLocation(inputs));
        }

        //Testing Directions Input ----
        [TestCase("R", ShipDirections.Right)]
        [TestCase("L", ShipDirections.Left)]
        [TestCase("D", ShipDirections.Down)]
        [TestCase("U", ShipDirections.Up)]
        public void IsDirections_Correct(string input, ShipDirections expected)
        {
            Assert.AreEqual(expected, Inputs.getDirection(input));
        }
        #endregion

        #region Placement Tests

        // Placing Ship outside the index of Board
        [Test]
        public void CheckIfShipOutofIndex()
        {
            Board b = new Board();

            ShipCoordinates placingTheShip = new ShipCoordinates()
            {
                Coordinate = new Coordinates(23, 12),
                Direction = ShipDirections.Up,
                ShipType = ShipType.Destroyer
            };

            var response = b.PlaceShip(placingTheShip);
            Assert.AreEqual(ShipPlacements.NotEnoughSpace, response);
        }

        [Test]
        public void CheckIf50_50ShipOutofIndex()
        {
            Board b = new Board();

            ShipCoordinates placingTheShip = new ShipCoordinates()
            {
                Coordinate = new Coordinates(10, 10),
                Direction = ShipDirections.Right,
                ShipType = ShipType.Carrier
            };

            var response = b.PlaceShip(placingTheShip);
            Assert.AreEqual(ShipPlacements.NotEnoughSpace, response);
        }

        [Test]
        public void IfOverlapInShips()
        {
            Board board = new Board();

            // let's put a carrier at (6,10), (5,10), (4,10), (3,10), (2,10)
            var carrierRequest = new ShipCoordinates()
            {
                Coordinate = new Coordinates(6, 10),
                Direction = ShipDirections.Left,
                ShipType = ShipType.Carrier
            };

            var carrierResponse = board.PlaceShip(carrierRequest);
            Assert.AreEqual(ShipPlacements.Ok, carrierResponse);

            // Destroyer at (4,10)
            var destroyerRequest = new ShipCoordinates()
            {
                Coordinate = new Coordinates(4, 10),
                Direction = ShipDirections.Down,
                ShipType = ShipType.Battleship
            };

            var destroyerResponse = board.PlaceShip(destroyerRequest);
            Assert.AreEqual(ShipPlacements.Overlap, destroyerResponse);
        }

        #endregion

        #region Hit OR Miss Test
        /* Placing Ships at
         * Cruiser: (1,1) (1,2) (1,3)
         * Submarine: (4,5) (5,5) (6,5)
         * Battleship: (10,6) (10,7) (10,8) (10, 9)
         * Carrier: (2,4) (3,4) (4,4) (5,4) (6,4)
         * Destroyer: (1,8) (2,8)
         */

        private Board SetupBoard()
        {
            Board board = new Board();

            Destroyer(board);
            Cruiser(board);
            Submarine(board);
            Battleship(board);
            Carrier(board);

            return board;
        }
        private void Carrier(Board board)
        {
            var placingTheShip = new ShipCoordinates()
            {
                Coordinate = new Coordinates(2, 4),
                Direction = ShipDirections.Right,
                ShipType = ShipType.Carrier
            };
            board.PlaceShip(placingTheShip);
        }

        private void Battleship(Board board)
        {
            var placingTheShip = new ShipCoordinates()
            {
                Coordinate = new Coordinates(10, 6),
                Direction = ShipDirections.Right,
                ShipType = ShipType.Battleship
            };

            board.PlaceShip(placingTheShip);
        }

        private void Submarine(Board board)
        {
            var placingTheShip = new ShipCoordinates()
            {
                Coordinate = new Coordinates(4, 5),
                Direction = ShipDirections.Right,
                ShipType = ShipType.Submarine
            };

            board.PlaceShip(placingTheShip);
        }

        private void Cruiser(Board board)
        {
            var placingTheShip = new ShipCoordinates()
            {
                Coordinate = new Coordinates(1, 1),
                Direction = ShipDirections.Right,
                ShipType = ShipType.Cruiser
            };

            board.PlaceShip(placingTheShip);
        }

        private void Destroyer(Board board)
        {
            var placingTheShip = new ShipCoordinates()
            {
                Coordinate = new Coordinates(1, 8),
                Direction = ShipDirections.Down,
                ShipType = ShipType.Destroyer
            };

            board.PlaceShip(placingTheShip);
        }

        [Test]
        public void FireShots_OutofIndex()
        {
            var board = SetupBoard();

            var coordinate = new Coordinates(25, 10);

            var response = board.FireShot(coordinate);

            Assert.AreEqual(ShotStatus.Invalid, response.ShotStatus);
        }

        [Test]
        public void HitTheShip()
        {
            var board = SetupBoard();

            var coordinate = new Coordinates(10, 8);
            var response = board.FireShot(coordinate);

            Assert.AreEqual(ShotStatus.Hit, response.ShotStatus);
            Assert.AreEqual("Battleship", response.ShipImpacted);
        }

        #endregion
    }
}