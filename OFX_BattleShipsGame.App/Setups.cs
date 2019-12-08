using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OFX_BattleShipsGame.StateTrackerAPI;
using OFX_BattleShipsGame.StateTrackerAPI.Classes;
using OFX_BattleShipsGame.StateTrackerAPI.Enums;

namespace OFX_BattleShipsGame.App
{
    public class Setups
    {
        PlayerProfile _playerProfile;

        public Setups(PlayerProfile playerProfile)
        {
            _playerProfile = playerProfile;
        }

        public void Setup()
        {
            Console.ForegroundColor = ConsoleColor.White;

            string player1 = Inputs.GetNameFromUser();

            _playerProfile.Player1.Name = player1;
            _playerProfile.Player1.IsPC = false;
            _playerProfile.Player1.Win = 0;

            //vs Computer
            {
                _playerProfile.Player2.Name = "Computer";
                _playerProfile.Player2.IsPC = true;
            }
        }

        public void SetBoard()
        {
            _playerProfile.Player1.PlayerBoard = new Board();
            PlaceShipOnBoard(_playerProfile.Player1);

            _playerProfile.Player2.PlayerBoard = new Board();
            PlaceShipOnBoard(_playerProfile.Player2);

            Console.WriteLine("All ship were placed successfull! Press any key to continue...");
            Console.ReadKey();
        }

        public void PlaceShipOnBoard(Player player)
        {
            bool IfManuallyPlaceTheShips = false;
            if (player.IsPC != true)
            {
                IfManuallyPlaceTheShips = Inputs.IfManuallyPlaceTheShips();
                if (!IfManuallyPlaceTheShips)
                    Console.WriteLine("Input the location and direction(L, R, U, D) of the ships. Ex:) A6, L:");
            }
            for (ShipType s = ShipType.Destroyer; s <= ShipType.Carrier; s++)
            {
                ShipCoordinates ShipToPlace = new ShipCoordinates();
                ShipPlacements result;
                do
                {
                    if (!player.IsPC && !IfManuallyPlaceTheShips)
                    {
                        ShipToPlace = Inputs.GetCoordinatesForPlayer1(s.ToString());
                        ShipToPlace.ShipType = s;
                        result = player.PlayerBoard.PlaceShip(ShipToPlace);
                        if (result == ShipPlacements.NotEnoughSpace)
                            Console.WriteLine("Not Enough Space!");
                        else if (result == ShipPlacements.Overlap)
                            Console.WriteLine("Overlap placement!");
                    }
                    else
                    {
                        ShipToPlace = Inputs.GetLocationFromComputer();
                        ShipToPlace.ShipType = s;
                        result = player.PlayerBoard.PlaceShip(ShipToPlace);
                    }

                } while (result != ShipPlacements.Ok);
            }
        }
    }
}
