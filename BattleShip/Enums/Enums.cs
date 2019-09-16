namespace BattleShip.Enums
{
    public enum ShipOrientation
    {
        Horizontal = 1,
        Vertical = 2
    }

    public enum AttackResult
    {
        Hit,
        Miss,
        AlreadyShot
    }

    public enum ShipType
    {
        Carrier,
        Battleship,
        Cruiser,
        Submarine,
        Destroyer
    }
}
