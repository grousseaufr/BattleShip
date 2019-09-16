using BattleShip.Enums;

namespace BattleShip.Boards
{
    /// <summary>
    /// Square is what a game grid is composed of.
    /// </summary>
    public class Square
    {
        public Coordinate Coordinate { get; set; }
        public bool HasBeenShot { get; set; }
        public ShipType? ShipType { get; set; }

        public bool IsOccupied
        {
            get
            {
                return ShipType != null;
            }
        }

        public bool IsHit
        {
            get
            {
                return HasBeenShot && IsOccupied;
            }
        }

        public bool IsMiss
        {
            get
            {
                return HasBeenShot && !IsOccupied;
            }
        }
    }
}
