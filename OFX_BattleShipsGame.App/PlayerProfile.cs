using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OFX_BattleShipsGame.StateTrackerAPI;

namespace OFX_BattleShipsGame.App
{
    public class PlayerProfile
    {
        public bool IsPlayer1 { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
    }
}
