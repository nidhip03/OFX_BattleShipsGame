using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OFX_BattleShipsGame.StateTrackerAPI.Enums;

namespace OFX_BattleShipsGame.StateTrackerAPI.Classes
{
    public class FireShotResponse
    {
        public ShotStatus ShotStatus { get; set; }
        public string ShipImpacted { get; set; }
    }
}
