using OFX_BattleShipsGame.StateTrackerAPI;
using OFX_BattleShipsGame.StateTrackerAPI.Classes;
using OFX_BattleShipsGame.StateTrackerAPI.Enums;
using System;
using System.Collections.Generic;

namespace OFX_BattleShipsGame.App
{
    public class Inputs
    {
        public static string GetNameFromUser()
        {
            string Name;
            Console.WriteLine("You are Playing with Computer.  ");
            Console.Write("Please Enter your Name -- ");
            Name = Console.ReadLine();
            _ = Name.Trim().ToUpper();

            return Name;
        }
        public static bool IfManuallyPlaceTheShips()
        {
            string strResult = "";
            do
            {
                Console.WriteLine("Let the Game randomly place the ship on the board?Y/N : ");
                strResult = Console.ReadLine(); strResult = strResult.Trim().ToUpper();
            } while (strResult != "Y" && strResult != "N");
            return strResult == "Y";
        }

        public static ShipCoordinates GetCoordinatesForPlayer1(string ShipType)
        {
            ShipCoordinates result = null;
            do
            {
                Console.Write("- " + ShipType + ": ");
                result = GetLocation(Console.ReadLine());

                if (result is null) {} else return result;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please Enter correct Directions?. Ex:) B3, R");
                Console.ForegroundColor = ConsoleColor.White;
            } while (result is null);
            return result;
        }

        public static ShipCoordinates GetLocation(string location)
        {
            string strX, strY, strDirection; int x, y;

            if (location.Split(',').Length == 2)
            {
                if (location.Split(',')[0].Trim().Length > 1)
                {
                    strX = location.Split(',')[0].Trim().Substring(0, 1);
                    strY = location.Split(',')[0].Trim().Substring(1);
                    strDirection = location.Split(',')[1].ToUpper().Trim();

                    x = GetNumberFromLetter(strX);
                    if (x > 0 && x < 11 && int.TryParse(strY, out y) && y > 0 && y < 11
                        && (strDirection == "L"
                        || strDirection == "R"
                        || strDirection == "U"
                        || strDirection == "D"))
                    {
                        ShipCoordinates ShipToPlace = new ShipCoordinates();
                        ShipToPlace.Direction = getDirection(strDirection);
                        ShipToPlace.Coordinate = new Coordinates(x, y);
                        return ShipToPlace;
                    }
                }
            }
            return null;
        }

        static int GetNumberFromLetter(string letter)
        {
            int result = -1;
            switch (letter.ToUpper())
            {
                case "A":
                    result = 1;
                    break;
                case "B":
                    result = 2;
                    break;
                case "C":
                    result = 3;
                    break;
                case "D":
                    result = 4;
                    break;
                case "E":
                    result = 5;
                    break;
                case "F":
                    result = 6;
                    break;
                case "G":
                    result = 7;
                    break;
                case "H":
                    result = 8;
                    break;
                case "I":
                    result = 9;
                    break;
                case "J":
                    result = 10;
                    break;
                default:
                    break;
            }
            return result;
        }

        public static ShipDirections getDirection(string direction)
        {
            switch (direction.ToUpper())
            {
                case "L":
                    return ShipDirections.Left;
                  
                case "R":
                    return ShipDirections.Right;
                 
                case "U":
                    return ShipDirections.Up;
                   
                default:
                    return ShipDirections.Down;
                  
            }

        }

        public static ShipCoordinates GetLocationFromComputer()
        {
            ShipCoordinates ShipToPlace = new ShipCoordinates();
            ShipToPlace.Direction = getDirection(GetRandom.GetDirection());
            ShipToPlace.Coordinate = new Coordinates(GetRandom.GetLocation(), GetRandom.GetLocation());
            return ShipToPlace;
        }

        public static Coordinates GetShotLocationFromUser()
        {
            string result = ""; int x, y;
            while (true)
            {
                Console.Write("Enter the Coordinates to Hit? ");
                result = Console.ReadLine();
                if (result.Trim().Length > 1)
                {
                    x = GetNumberFromLetter(result.Substring(0, 1));
                    if (x > 0 && int.TryParse(result.Substring(1), out y))
                    {
                        return new Coordinates(x, y);
                    }
                }
            }
        }
        public static Coordinates GetShotLocationFromComputer(Board victimboard)
        {
            _ = GetRandom.r.Next(1, 100) <= 30;
            return GetRightLocationToShot(victimboard);
        }
        static Coordinates GetRightLocationToShot(Board victimboard)
        {
            List<Coordinates> tmpList = new List<Coordinates> { };
            for (int i = 0; i < victimboard.Ships.Length; i++)
            {
                Ship tmpShip = victimboard.Ships[i];
                for (int j = 0; j < tmpShip.BoardPositions.Length; j++)
                {
                    if (victimboard.CheckCoordinate(tmpShip.BoardPositions[j]) == ShotHistory.Unknown)
                        tmpList.Add(tmpShip.BoardPositions[j]);
                }
            }

            return tmpList[GetRandom.r.Next(0, tmpList.Count - 1)];

        }

    }
}