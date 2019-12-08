using OFX_BattleShipsGame.StateTrackerAPI.Classes;
using OFX_BattleShipsGame.StateTrackerAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OFX_BattleShipsGame.StateTrackerAPI
{
    public class Board
    {
        public const int xCoordinator = 10;
        public const int yCoordinator = 10;
        private Dictionary<Coordinates, ShotHistory> ShotHistorys;
        private int _currentShipIndex;
        public Ship[] Ships { get; private set; }

        public Board()
        {
            ShotHistorys = new Dictionary<Coordinates, ShotHistory>();
            Ships = new Ship[5];
            _currentShipIndex = 0;
        }
        public ShipPlacements PlaceShip(ShipCoordinates request)
        {
            if (_currentShipIndex > 4)
                throw new Exception("You can not add another ship, 5 is the limit!");

            if (!IsValidCoordinate(request.Coordinate))
                return ShipPlacements.NotEnoughSpace;

            Ship newShip = CreateShip.CreateShips(request.ShipType);
            switch (request.Direction)
            {
                case ShipDirections.Down:
                    return PlaceShipDown(request.Coordinate, newShip);
                case ShipDirections.Up:
                    return PlaceShipUp(request.Coordinate, newShip);
                case ShipDirections.Left:
                    return PlaceShipLeft(request.Coordinate, newShip);
                default:
                    return PlaceShipRight(request.Coordinate, newShip);
            }
        }

        private bool IsValidCoordinate(Coordinates coordinate)
        {
            return coordinate.XCoordinate >= 1 && coordinate.XCoordinate <= xCoordinator &&
            coordinate.YCoordinate >= 1 && coordinate.YCoordinate <= yCoordinator;
        }

        private void AddShipToBoard(Ship newShip)
        {
            Ships[_currentShipIndex] = newShip;
            _currentShipIndex++;
        }

        //Directions -------------------------
        private ShipPlacements PlaceShipDown(Coordinates coordinate, Ship newShip)
        {
            // y coordinate gets bigger
            int positionIndex = 0;
            int maxX = coordinate.XCoordinate + newShip.BoardPositions.Length;

            for (int i = coordinate.XCoordinate; i < maxX; i++)
            {
                var currentCoordinate = new Coordinates(i, coordinate.YCoordinate);

                if (!IsValidCoordinate(currentCoordinate))
                    return ShipPlacements.NotEnoughSpace;

                if (OverlapsAnotherShip(currentCoordinate))
                    return ShipPlacements.Overlap;

                newShip.BoardPositions[positionIndex] = currentCoordinate;
                positionIndex++;
            }

            AddShipToBoard(newShip);
            return ShipPlacements.Ok;
        }

        private ShipPlacements PlaceShipUp(Coordinates coordinate, Ship newShip)
        {
            // x coordinate gets smaller
            int positionIndex = 0;
            int minX = coordinate.XCoordinate - newShip.BoardPositions.Length;

            for (int i = coordinate.XCoordinate; i > minX; i--)
            {
                var currentCoordinate = new Coordinates(i, coordinate.YCoordinate);

                if (!IsValidCoordinate(currentCoordinate))
                    return ShipPlacements.NotEnoughSpace;

                if (OverlapsAnotherShip(currentCoordinate))
                    return ShipPlacements.Overlap;

                newShip.BoardPositions[positionIndex] = currentCoordinate;
                positionIndex++;
            }

            AddShipToBoard(newShip);
            return ShipPlacements.Ok;
        }

        private ShipPlacements PlaceShipRight(Coordinates coordinate, Ship newShip)
        {
            // y coordinate gets bigger
            int positionIndex = 0;
            int maxY = coordinate.YCoordinate + newShip.BoardPositions.Length;

            for (int i = coordinate.YCoordinate; i < maxY; i++)
            {
                var currentCoordinate = new Coordinates(coordinate.XCoordinate, i);
                if (!IsValidCoordinate(currentCoordinate))
                    return ShipPlacements.NotEnoughSpace;

                if (OverlapsAnotherShip(currentCoordinate))
                    return ShipPlacements.Overlap;

                newShip.BoardPositions[positionIndex] = currentCoordinate;
                positionIndex++;
            }

            AddShipToBoard(newShip);
            return ShipPlacements.Ok;
        }

        private ShipPlacements PlaceShipLeft(Coordinates coordinate, Ship newShip)
        {
            // y coordinate gets smaller
            int positionIndex = 0;
            int minY = coordinate.YCoordinate - newShip.BoardPositions.Length;

            for (int i = coordinate.YCoordinate; i > minY; i--)
            {
                var currentCoordinate = new Coordinates(coordinate.XCoordinate, i);

                if (!IsValidCoordinate(currentCoordinate))
                    return ShipPlacements.NotEnoughSpace;

                if (OverlapsAnotherShip(currentCoordinate))
                    return ShipPlacements.Overlap;

                newShip.BoardPositions[positionIndex] = currentCoordinate;
                positionIndex++;
            }

            AddShipToBoard(newShip);
            return ShipPlacements.Ok;
        }

        private bool OverlapsAnotherShip(Coordinates coordinates)
        {
            foreach (var ship in Ships)
            {
                if (ship != null)
                {
                    if (ship.BoardPositions.Contains(coordinates))
                        return true;
                }
            }

            return false;
        }


        //Displaying
        public void DrawBoards(Player player)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.Write("  ");
            for (int y = 1; y <= 10; y++)
            {
                Console.Write(y);
                Console.Write(" ");
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            for (int x = 1; x <= 10; x++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(GetLetterFromNumber(x) + " ");
                Console.ForegroundColor = ConsoleColor.White;
                for (int y = 1; y <= 10; y++)
                {
                    //Console.Write("O");
                    {
                        ShotHistory history = player.PlayerBoard.CheckCoordinate(new Coordinates(x, y));
                        switch (history)
                        {
                            case ShotHistory.Hit:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("H");
                                Console.ForegroundColor = ConsoleColor.White;
                                break;
                            case ShotHistory.Miss:
                                Console.Write("M");
                                break;
                            case ShotHistory.Unknown:
                                Console.Write(" ");
                                break;
                        }
                    }
                    Console.Write("|");
                }
                Console.WriteLine();
            }
            Console.WriteLine("");
        }

        public static string GetLetterFromNumber(int number)
        {
            string result = "";
            switch (number)
            {
                case 1:
                    result = "A";
                    break;
                case 2:
                    result = "B";
                    break;
                case 3:
                    result = "C";
                    break;
                case 4:
                    result = "D";
                    break;
                case 5:
                    result = "E";
                    break;
                case 6:
                    result = "F";
                    break;
                case 7:
                    result = "G";
                    break;
                case 8:
                    result = "H";
                    break;
                case 9:
                    result = "I";
                    break;
                case 10:
                    result = "J";
                    break;
                default:
                    break;
            }
            return result;
        }

        public ShotHistory CheckCoordinate(Coordinates coordinate)
        {
            if (ShotHistorys.ContainsKey(coordinate))
            {
                return ShotHistorys[coordinate];
            }
            else
            {
                return ShotHistory.Unknown;
            }
        }

        public FireShotResponse FireShot(Coordinates coordinate)
        {
            var response = new FireShotResponse();
            // is this coordinate on the board?
            if (!IsValidCoordinate(coordinate))
            {
                response.ShotStatus = ShotStatus.Invalid;
                return response;
            }

            // did they already try this position?
            if (ShotHistorys.ContainsKey(coordinate))
            {
                response.ShotStatus = ShotStatus.Duplicate;
                return response;
            }

            CheckShipsForHit(coordinate, response);

            return response;
        }

        private void CheckShipsForHit(Coordinates coordinate, FireShotResponse response)
        {
            response.ShotStatus = ShotStatus.Miss;

            foreach (var ship in Ships)
            {
                if (ship.IsSunk)
                    continue;

                ShotStatus status = ship.FireAtShip(coordinate);

                switch (status)
                {
                    case ShotStatus.Hit:
                        response.ShotStatus = ShotStatus.Hit;
                        response.ShipImpacted = ship.ShipName;
                        ShotHistorys.Add(coordinate, ShotHistory.Hit);
                        break;
                }

                if (status != ShotStatus.Miss)
                    break;
            }

            if (response.ShotStatus == ShotStatus.Miss)
            {
                response.ShotStatus = ShotStatus.Miss;
                ShotHistorys.Add(coordinate,ShotHistory.Miss);
            }
        }
    }
}