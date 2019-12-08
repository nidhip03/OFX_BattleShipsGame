using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFX_BattleShipsGame.StateTrackerAPI.Classes
{
    public class GetRandom
    {
        public static System.Random r = new System.Random();

        public static bool WhoseFirst()
        {
            return r.Next(1, 10) <= 5;
        }

        public static string GetDirection()
        {
            switch (r.Next(1, 4))
            {
                case 1:
                    return "L";
                case 2:
                    return "R";
                case 3:
                    return "U";
                case 4:
                    return "D";
                default:
                    return "";
            }
        }

        public static int GetLocation()
        {
            return r.Next(1, 10);
        }

    }
}
