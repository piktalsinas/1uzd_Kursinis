using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ND1_Mariya_Beznosova;

namespace ND1_MariyaBeznosova
{
    internal class BoardUtils 
    {
        public static void DrawBoard(char[][] board)
        {
            string HLINE = "  +---+---+---+---+---+---+---+---+";
            string VLINE = "  |   |   |   |   |   |   |   |   |";

            Console.WriteLine("    1   2   3   4   5   6   7   8");
            Console.WriteLine(HLINE);

            for (int y = 0; y < 8; y++)
            {
                Console.WriteLine(VLINE);
                Console.Write((y + 1) + " ");
                for (int x = 0; x < 8; x++)
                {
                    Console.Write("| " + board[x][y] + " ");
                }
                Console.WriteLine("|");
                Console.WriteLine(VLINE);
                Console.WriteLine(HLINE);
            }
        }
        public static char[][] GetNewBoard()
        {
            char[][] board = new char[8][];
            for (int i = 0; i < 8; i++)
            {
                board[i] = new char[8];
                for (int j = 0; j < 8; j++)
                {
                    board[i][j] = ' ';
                }
            }
            return board;
        }
        public static void ResetBoard(char[][] board)
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    board[x][y] = ' ';
                }
            }

            board[3][3] = 'X';
            board[3][4] = 'O';
            board[4][3] = 'O';
            board[4][4] = 'X';
        }

        public static bool IsValidMove(char[][] board, char tile, int xstart, int ystart)
        {
            if (board[xstart][ystart] != ' ' || !IsOnBoard(xstart, ystart))
            {
                return false;
            }

            char otherTile = (tile == 'X') ? 'O' : 'X';

            int[][] directions = new int[][] {
            new int[] { 0, 1 },
            new int[] { 1, 1 },
            new int[] { 1, 0 },
            new int[] { 1, -1 },
            new int[] { 0, -1 },
            new int[] { -1, -1 },
            new int[] { -1, 0 },
            new int[] { -1, 1 }
            };

            bool isValid = false;

            foreach (int[] direction in directions)
            {
                int x = xstart + direction[0];
                int y = ystart + direction[1];

                bool foundOpponentTile = false;

                while (IsOnBoard(x, y))
                {
                    if (board[x][y] == otherTile)
                    {
                        foundOpponentTile = true;
                    }
                    else if (board[x][y] == tile && foundOpponentTile)
                    {
                        isValid = true;
                        break;
                    }
                    else
                    {
                        break;
                    }

                    x += direction[0];
                    y += direction[1];
                }
            }

            return isValid;
        }


        public static char[][] GetBoardCopy(char[][] board)
        {
            char[][] dupeBoard = new char[8][];

            for (int x = 0; x < 8; x++)
            {
                dupeBoard[x] = new char[8];
                for (int y = 0; y < 8; y++)
                {
                    dupeBoard[x][y] = board[x][y];
                }
            }

            return dupeBoard;
        }

        public static bool IsOnCorner(int x, int y)
        {
            return (x == 0 && y == 0) || (x == 7 && y == 0) || (x == 0 && y == 7) || (x == 7 && y == 7);
        }

        public static bool IsOnBoard(int x, int y)
        {
            return x >= 0 && x < 8 && y >= 0 && y < 8;
        }
      
        public static Dictionary<char, int> GetScoreOfBoard(char[][] board)
        {
            int xscore = 0;
            int oscore = 0;

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (board[x][y] == 'X')
                    {
                        xscore++;
                    }
                    else if (board[x][y] == 'O')
                    {
                        oscore++;
                    }
                }
            }

            return new Dictionary<char, int>
            {
                { 'X', xscore },
                { 'O', oscore }
            };
        }

    }
}
