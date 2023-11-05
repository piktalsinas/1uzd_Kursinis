using ND1_Mariya_Beznosova;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND1_MariyaBeznosova
{
    internal class Hints
    {
        public static void ShowHints(char[][] board, List<int[]> validMoves)
        {
            char[][] boardWithHints = ReversiGame.GetBoardCopy(board);

            foreach (var move in validMoves)
            {
                boardWithHints[move[0]][move[1]] = '*';
            }

            ReversiGame.DrawBoard(boardWithHints);
        }

        public static void ShowPoints(char[][] board, char playerTile, char computerTile)
        {
            Dictionary<char, int> scores = ReversiGame.GetScoreOfBoard(board);
            Console.WriteLine("You have " + scores[playerTile] + " points. The computer has " + scores[computerTile] + " points.");
        }

    }
}
