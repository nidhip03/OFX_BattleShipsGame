﻿using OFX_BattleShipsGame.StateTrackerAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFX_BattleShipsGame.StateTrackerAPI.Classes
{
    public class CreateShip
    {
        public static Ship CreateShips(ShipType type)
        {
            switch (type)
            {
                case ShipType.Destroyer:
                    return new Ship(ShipType.Destroyer, 2);
                case ShipType.Cruiser:
                    return new Ship(ShipType.Cruiser, 3);
                case ShipType.Submarine:
                    return new Ship(ShipType.Submarine, 3);
                case ShipType.Battleship:
                    return new Ship(ShipType.Battleship, 4);
                default:
                    return new Ship(ShipType.Carrier, 5);
            }
        }
    }
}
