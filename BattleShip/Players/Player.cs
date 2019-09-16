using BattleShip.Boards;
using BattleShip.Enums;
using BattleShip.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleShip.Players
{
    public class Player
    {
        public string Name { get; set; }
        public GameBoard GameBoard { get; set; }
        public List<Ship> Ships { get; set; }
        
        public bool HasLost
        {
            get
            {
                return Ships.All(a => a.IsSunk);
            }
        }

        public Player(string name)
        {
            Name = name;
            GameBoard = new GameBoard();
            Ships = new List<Ship>()
            {
                new Carrier(),
                new Battleship()
                //new Cruiser(),
                //new Submarine(),
                //new Destroyer()
            };
        }

        internal string GetStatusMessage()
        {
            var message = new StringBuilder();

            if(Ships.Any(a => a.HitCount > 0))
            {
                message.AppendLine($"{Name} current status:");

                var hitShips = Ships.Where(w => w.HitCount > 0 && !w.IsSunk).ToList();
                foreach (var item in hitShips)
                {
                    var plural = item.HitCount > 1 ? "s" : string.Empty;
                    message.AppendLine($"{item.Name} : {item.HitCount} hit{plural}");
                }

                var sunkShips = Ships.Where(w => w.IsSunk).ToList();
                foreach (var item in sunkShips)
                {
                    message.AppendLine($"{item.Name} : Sunk");
                }

                message.AppendLine($"Received {GameBoard.Squares.Count(c => c.IsHit)} hit shot");
                message.AppendLine($"Received {GameBoard.Squares.Count(c => c.IsMiss)} missed shot");

            }

            return message.ToString();
        }

        public void PlaceShips()
        {
            foreach (var ship in Ships)
            {
                //Get ship placement from setup class
                var shipPlacementSetup = ShipPlacementSetup[ship.Type];
                var startRow = shipPlacementSetup.StartCoordinate.Row;
                var startColumn = shipPlacementSetup.StartCoordinate.Column;

                PlaceShip(startRow, startColumn, ship, shipPlacementSetup.ShipOrientation);
            }
        }

        public void PlaceShip(int startRow, int startColumn, Ship ship, ShipOrientation shipOrientation)
        {
            var endColumn = shipOrientation == ShipOrientation.Horizontal ? startColumn + (ship.Length - 1) : startColumn;
            var endRow = shipOrientation == ShipOrientation.Vertical ? startRow + (ship.Length - 1) : startRow;

            GameBoard.ValidateShipLocation(startRow, startColumn, endColumn, endRow, ship, shipOrientation);

            //Set ship on game board
            var squares = GameBoard.GetSquares(startRow, startColumn, endRow, endColumn);
            foreach (var square in squares)
            {
                square.ShipType = ship.Type;
            }
        }

        public AttackResult HandleAttack(int row, int column)
        {
            var aimedSquare = GameBoard.GetSquare(row, column);

            if (aimedSquare.HasBeenShot)
            {
                return AttackResult.AlreadyShot;
            }

            aimedSquare.HasBeenShot = true;

            if (aimedSquare.IsOccupied)
            {
                var shipType = aimedSquare.ShipType;
                Ships.First(w => w.Type == shipType).HitCount++;
                
                return AttackResult.Hit;
            }

            return AttackResult.Miss;
        }

        private readonly Dictionary<ShipType, ShipPlacementSetup> ShipPlacementSetup = new Dictionary<ShipType, ShipPlacementSetup>
        {
            { ShipType.Carrier, new ShipPlacementSetup { ShipOrientation = ShipOrientation.Vertical, StartCoordinate = new Coordinate(1, 1)} },
            { ShipType.Battleship, new ShipPlacementSetup { ShipOrientation = ShipOrientation.Horizontal, StartCoordinate = new Coordinate(2, 2)} },
            { ShipType.Cruiser, new ShipPlacementSetup { ShipOrientation = ShipOrientation.Vertical, StartCoordinate = new Coordinate(5, 4)} },
            { ShipType.Submarine, new ShipPlacementSetup { ShipOrientation = ShipOrientation.Horizontal, StartCoordinate = new Coordinate(7, 7)} },
            { ShipType.Destroyer, new ShipPlacementSetup { ShipOrientation = ShipOrientation.Vertical, StartCoordinate = new Coordinate(8, 10)} },
        };
    }
}
