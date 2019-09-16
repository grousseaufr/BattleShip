namespace BattleShip.Ships
{
    public class Battleship : Ship
    {
        public Battleship()
        {
            Name = "Battleship";
            Type = Enums.ShipType.Battleship;
            Length = 4;
        }
    }
}
