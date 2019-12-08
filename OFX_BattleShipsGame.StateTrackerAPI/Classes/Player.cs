using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFX_BattleShipsGame.StateTrackerAPI
{
    public class Player
    {
        public string Name { get; set; }
        public int Win { get; set; }
        public bool IsPC { get; set; }

        public Board PlayerBoard;
    }
}
