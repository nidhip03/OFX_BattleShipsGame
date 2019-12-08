using OFX_BattleShipsGame.StateTrackerAPI.Enums;

namespace OFX_BattleShipsGame.StateTrackerAPI.Classes
{
    public class ShipCoordinates
    {
        public Coordinates Coordinate { get; set; }
        public ShipDirections Direction { get; set; }
        public ShipType ShipType { get; set; }
    }
}
