using System;

namespace BattleShip
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("*** Welcome to battleship ***\n");
            Console.WriteLine("Coordinate must be in the following format : row,column");
            Console.WriteLine("Row/Column value between 1 and 10\n");

            try
            {
                GameManager gameManager = new GameManager();
                gameManager.Play();
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }

            Console.Read();
        }
    }           
}
