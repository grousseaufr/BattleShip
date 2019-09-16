using BattleShip.Enums;

namespace BattleShip.Ships
{
    public abstract class Ship
    {
        public string Name { get; set; }
        public int Length { get; set; }
        public int HitCount { get; set; }
        public ShipType Type { get; set; }

        public bool IsSunk
        {
            get
            {
                return HitCount == Length;
            }
        }
    }
}

/*
	Carrier	    5
	Battleship	4
	Cruiser	    3
	Submarine	3
	Destroyer	2
 * */
