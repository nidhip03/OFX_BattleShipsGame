﻿using System;

namespace OFX_BattleShipsGame.StateTrackerAPI.Classes
{
    public class Coordinates
    {
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }

        public Coordinates(int x, int y)
        {
            XCoordinate = x;
            YCoordinate = y;
        }
        public override bool Equals(object obj)
        {
            Coordinates otherCoordinate = obj as Coordinates;

            if (otherCoordinate == null)
                return false;

            return otherCoordinate.XCoordinate == this.XCoordinate &&
                   otherCoordinate.YCoordinate == this.YCoordinate;
        }
        public override int GetHashCode()
        {
            string uniqueHash = this.XCoordinate.ToString() + this.YCoordinate.ToString() + "00";
            return (Convert.ToInt32(uniqueHash));
        }
    }
}
