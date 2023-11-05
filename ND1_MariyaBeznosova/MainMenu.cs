using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ND1_Mariya_Beznosova;

namespace ND1_MariyaBeznosova
{
    class MainMenu
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("              Welcome to Reversi!                 ");

            Console.WriteLine("==================================================");

            ReversiGame game = new ReversiGame();
            game.StartGame();
        }
        public static string WhoGoesFirst()
        {
            return (new Random().Next(2) == 0) ? "computer" : "player";
        }

        public static bool PlayAgain(bool defaultChoice = true)
        {
            Console.WriteLine("Do you want to play again? (yes or no)");
            string input = Console.ReadLine().Trim().ToLower();
            if (input.StartsWith("y") || (string.IsNullOrEmpty(input) && defaultChoice))
            {
                return true;
            }
            return false;
        }

        public static void EnterPlayerTile(out char playerTile, out char computerTile)
        {
            playerTile = ' ';
            computerTile = ' ';

            while (playerTile != 'X' && playerTile != 'O')
            {
                Console.WriteLine("Do you want to be X or O?");
                playerTile = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine();
            }

            computerTile = (playerTile == 'X') ? 'O' : 'X';
        }
    }
}
