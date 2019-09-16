namespace BattleShip.Ships
{
    public class Carrier : Ship
    {
        public Carrier()
        {
            Name = "Carrier";
            Type = Enums.ShipType.Carrier;
            Length = 5;
        }
    }
}
