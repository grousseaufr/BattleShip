using System;
using System.Collections.Generic;
using System.Linq;
using BattleShip.Enums;
using BattleShip.Ships;

namespace BattleShip.Boards
{
    /// <summary>
    /// Game board (composed of squares) where the player will store his ships.
    /// </summary>
    public class GameBoard
    {
        private const int COLUMN_COUNT = 10;
        private const int ROW_COUNT = 10;

        public List<Square> Squares { get; set; }

        public GameBoard()
        {
            Squares = new List<Square>(COLUMN_COUNT * ROW_COUNT);
            InitGrid();
        }

        public Square GetSquare(int row, int column)
        {
            return Squares.FirstOrDefault(f => f.Coordinate.Row == row && f.Coordinate.Column == column);
        }

        public List<Square> GetSquares(int startRow, int startColumn, int endRow, int endColumn)
        {
            return Squares.Where(w => w.Coordinate.Row >= startRow &&
                                      w.Coordinate.Column >= startColumn &&
                                      w.Coordinate.Row <= endRow &&
                                      w.Coordinate.Column <= endColumn).ToList();
        }

        internal void ValidateShipLocation(int startRow, int startColumn, int endColumn, int endRow, Ship ship, ShipOrientation shipOrientation)
        {
            if (!IsOnBoard(endRow, endColumn, shipOrientation))
            {
                throw new Exception($"Error : {ship.Name} is out of board.");
            }

            if (IsOccupied(startRow, startColumn, endRow, endColumn, shipOrientation))
            {
                throw new Exception($"Error :  {ship.Name} is on non-empty square.");
            }
        }

        private void InitGrid()
        {
            for (int row = 1; row <= ROW_COUNT; row++)
            {
                for (int column = 1; column <= COLUMN_COUNT; column++)
                {
                    Squares.Add(new Square
                    {
                        Coordinate = new Coordinate(row, column)
                    });
                }
            }
        }

        private bool IsOnBoard(int endRow, int endColumn, ShipOrientation shipOrientation)
        {
            switch (shipOrientation)
            {
                case ShipOrientation.Horizontal:
                    return endColumn <= COLUMN_COUNT;
                case ShipOrientation.Vertical:
                    return endRow <= ROW_COUNT;
                default:
                    throw new ArgumentException("Unknown shipOrientation");
            }
        }

        private bool IsOccupied(int startRow, int startColumn, int endRow, int endColumn, ShipOrientation shipOrientation)
        {
            var squares = GetSquares(startRow, startColumn, endRow, endColumn);
            return squares.Any(a => a.IsOccupied);
        }
    }
}
