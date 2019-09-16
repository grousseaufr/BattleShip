using BattleShip.Boards;
using BattleShip.Enums;

namespace BattleShip.Players
{
    /// <summary>
    /// Description of a ship placement on the board
    /// </summary>
    internal class ShipPlacementSetup
    {
        public Coordinate StartCoordinate { get; set; }
        public ShipOrientation ShipOrientation { get; set; }
    }
}