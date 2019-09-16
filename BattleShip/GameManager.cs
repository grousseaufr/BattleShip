using BattleShip.Boards;
using BattleShip.Players;
using System;
namespace BattleShip
{
    /// <summary>
    /// 
    /// </summary>
    public class GameManager
    {
        public Player Player;

        public GameManager()
        {
            Player = new Player("Greg");
            Player.PlaceShips();
        }

        public void Play()
        {
            while (!Player.HasLost)
            {
                PlayRound();
            }

            Console.WriteLine("You lost ! All ships are sunk !");
        }

        private void PlayRound()
        {
            //Wait for input and handle attack
            var attackCoordinates = GetPlayerAttack();
            var attackResult = Player.HandleAttack(attackCoordinates.Row, attackCoordinates.Column);

            Console.WriteLine($"{attackResult} ! \n");

            //Display status following attack
            var playerStatus = Player.GetStatusMessage();
            if (!string.IsNullOrEmpty(playerStatus))
            {
                Console.WriteLine(playerStatus + "\n");
            }
        }

        private Coordinate GetPlayerAttack()
        {
            int row = 0;
            int column = 0;
            bool IsCorrectInput = false;

            while (!IsCorrectInput)
            {
                Console.Write("Enter an attack : ");

                try
                {
                    var userInput = Console.ReadLine().Split(',');
                    row = int.Parse(userInput[0]);
                    column = int.Parse(userInput[1]);

                    if (row < 1 || row > 10 || column < 1 || column > 10)
                    {
                        throw new Exception();
                    }

                    IsCorrectInput = true;
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid coordinate. Format is \"row,column\". Values between 1 and 10.\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

            return new Coordinate(row, column);
        }
    }
}
