using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND1_MariyaBeznosova
{
    internal class Moves
    {
        private const int NoMove = -1;
        private const int ToggleHints = -2;
        public static List<int[]> GetValidMoves(char[][] board, char tile)
        {
            List<int[]> validMoves = new List<int[]>();

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (BoardUtils.IsValidMove(board, tile, x, y))
                    {
                        validMoves.Add(new int[] { x, y });
                    }
                }
            }

            return validMoves;
        }

        public static void MakeMove(char[][] board, char tile, int xstart, int ystart)
        {
            List<int[]> tilesToFlip = new List<int[]>();

            if (BoardUtils.IsValidMove(board, tile, xstart, ystart))
            {
                board[xstart][ystart] = tile;

                char otherTile = (tile == 'X') ? 'O' : 'X';

                int[][] directions = new int[][] {
                    new int[] {0, 1},
                    new int[] {1, 1},
                    new int[] {1, 0},
                    new int[] {1, -1},
                    new int[] {0, -1},
                    new int[] {-1, -1},
                    new int[] {-1, 0},
                    new int[] {-1, 1}
                };

                foreach (int[] direction in directions)
                {
                    int x = xstart + direction[0];
                    int y = ystart + direction[1];

                    if (BoardUtils.IsOnBoard(x, y) && board[x][y] == otherTile)
                    {
                        x += direction[0];
                        y += direction[1];

                        if (!BoardUtils.IsOnBoard(x, y))
                        {
                            continue;
                        }

                        while (board[x][y] == otherTile)
                        {
                            x += direction[0];
                            y += direction[1];

                            if (!BoardUtils.IsOnBoard(x, y))
                            {
                                break;
                            }
                        }

                        if (!BoardUtils.IsOnBoard(x, y))
                        {
                            continue;
                        }

                        if (board[x][y] == tile)
                        {
                            while (true)
                            {
                                x -= direction[0];
                                y -= direction[1];

                                if (x == xstart && y == ystart)
                                {
                                    break;
                                }

                                tilesToFlip.Add(new int[] { x, y });
                            }
                        }
                    }
                }

                foreach (int[] tileToFlip in tilesToFlip)
                {
                    board[tileToFlip[0]][tileToFlip[1]] = tile;
                }
            }
        }
        public static int[] GetComputerMove(char[][] board, char computerTile)
        {
            List<int[]> possibleMoves = Moves.GetValidMoves(board, computerTile);

            // Randomize the order of the possible moves
            Random rng = new Random();
            int n = possibleMoves.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                int[] value = possibleMoves[k];
                possibleMoves[k] = possibleMoves[n];
                possibleMoves[n] = value;
            }

            // Always go for a corner if available
            foreach (int[] possibleMove in possibleMoves)
            {
                if (BoardUtils.IsOnCorner(possibleMove[0], possibleMove[1]))
                {
                    return possibleMove;
                }
            }

            // Go through all the possible moves and remember the best scoring move
            int bestScore = -1;
            int[] bestMove = new int[] { NoMove, NoMove };

            foreach (int[] possibleMove in possibleMoves)
            {
                char[][] dupeBoard = BoardUtils.GetBoardCopy(board);
                Moves.MakeMove(dupeBoard, computerTile, possibleMove[0], possibleMove[1]);
                int score = BoardUtils.GetScoreOfBoard(dupeBoard)[computerTile];

                if (score > bestScore)
                {
                    bestMove = possibleMove;
                    bestScore = score;
                }
            }

            return bestMove;
        }
        public static int[] GetPlayerMove(char[][] board, char playerTile)
        {
            int[] move = new int[] { NoMove, NoMove };
            List<int[]> validMoves = GetValidMoves(board, playerTile);

            if (validMoves.Count == 0)
            {
                return new int[] {NoMove, NoMove }; 
            }

            string moveStr = "";

            Console.WriteLine("Enter your move /// 'quit' to end the game /// 'hints' to turn off/on hints.");

            while (true)
            {
                moveStr = Console.ReadLine().ToLower();

                if (moveStr == "quit")
                {
                    return new int[] {NoMove, NoMove }; 
                }
                else if (moveStr == "hints")
                {
                    return new int[] { ToggleHints, ToggleHints };
                }

                if (moveStr.Length == 2 && char.IsDigit(moveStr[0]) && char.IsDigit(moveStr[1]))
                {
                    move[1] = int.Parse(moveStr[0].ToString()) - 1;
                    move[0] = int.Parse(moveStr[1].ToString()) - 1;

                    if (BoardUtils.IsValidMove(board, playerTile, move[0], move[1]))
                    {
                        break; // Valid move
                    }
                    else
                    {
                        Console.WriteLine("That is not a valid move. Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Type the y digit (1-8), then the x digit (1-8).");
                }
            }

            return move;
        }
    }
}
