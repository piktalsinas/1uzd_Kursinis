using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;
using static System.Formats.Asn1.AsnWriter;
using ND1_MariyaBeznosova;

namespace ND1_Mariya_Beznosova
{
    internal class ReversiGame : BoardUtils
    {
        private int playerScore;
        private int computerScore;

        public void StartGame()
        {
            Console.WriteLine("Press Enter to start");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }

            while (true)
            {
                char[][] mainBoard = BoardUtils.GetNewBoard();
                BoardUtils.ResetBoard(mainBoard);
                char playerTile, computerTile;
                MainMenu.EnterPlayerTile(out playerTile, out computerTile);

                bool showHints = false;
                string turn = MainMenu.WhoGoesFirst();

                Console.WriteLine($"The {turn} will go first.");

                int[] playerMove = new int[2];
                int[] computerMove = new int[2];

                while (true)
                {
                    switch (turn)
                    {
                        case "player":
                            DrawBoard(mainBoard);
                            Console.WriteLine("Press Enter to make your move.");
                            Console.ReadLine();
                            if (showHints)
                            {
                                Hints.ShowHints(mainBoard, Moves.GetValidMoves(mainBoard, playerTile));
                            }
                            break;
                        case "computer":
                            DrawBoard(mainBoard);
                            Console.WriteLine("Computer is thinking...");
                            Thread.Sleep(1000);
                            break;
                        default:
                            playerMove = new int[] { -1, -1 };
                            computerMove = new int[] { -1, -1 };
                            break;
                    }

                    Hints.ShowPoints(mainBoard, playerTile, computerTile);

                    int[] move;

                    if (turn == "player")
                    {
                        move = Moves.GetPlayerMove(mainBoard, playerTile);
                    }
                    else if (turn == "computer")
                    {
                        move = Moves.GetComputerMove(mainBoard, computerTile);
                    }
                    else
                    {
                        move = new int[] { -1, -1 };
                    }

                    if (move[0] == -1)
                    {
                        Console.WriteLine("Thanks for playing!");
                        return;
                    }
                    else if (move[0] == -2)
                    {
                        showHints = !showHints;
                        continue;
                    }
                    else
                    {
                        Moves.MakeMove(mainBoard, turn switch
                        {
                            "player" => playerTile,
                            "computer" => computerTile,
                            _ => ' ' // Default case
                        }, move[0], move[1]);
                    }

                    List<int[]> playerValidMoves = Moves.GetValidMoves(mainBoard, playerTile);
                    List<int[]> computerValidMoves = Moves.GetValidMoves(mainBoard, computerTile);

                   if (playerValidMoves.Count == 0 && computerValidMoves.Count == 0)
                    {
                        break;
                    }
                    else
                    {
                        turn = turn switch
                        {
                            "player" => "computer",
                            "computer" => "player",
                            _ => "player" // Default case
                        };
                    }
                }


                char[][] playerBoard = GetBoardCopy(mainBoard);
                DrawBoard(playerBoard);
                Thread.Sleep(1000);

                char[][] newBoard = GetNewBoard();
                mainBoard = GetBoardCopy(newBoard);
                DrawBoard(mainBoard);
                Thread.Sleep(1000);

                Dictionary<char, int> scores = GetScoreOfBoard(mainBoard);
                playerScore = scores[playerTile];
                computerScore = scores[computerTile];

                Console.WriteLine("X scored " + playerScore + " points. O scored " + computerScore + " points.");

                if (playerScore > computerScore)
                {
                    Console.WriteLine("You beat the computer by " + (playerScore - computerScore) + " points! Congratulations!");
                }
                else if (playerScore < computerScore)
                {
                    Console.WriteLine("You lost. The computer beat you by " + (computerScore - playerScore) + " points.");
                }
                else
                {
                    Console.WriteLine("The game was a tie!");
                }

                if (!MainMenu.PlayAgain())
                {
                    return;
                }
            }
        }

       
        public Dictionary<char, int> GetScores(params int[] scores)
        {
            return new Dictionary<char, int>
            {
                { 'X', scores[0] },
                { 'O', scores[1] }
            };
        }

        public int CompareTo(ReversiGame other)
        {
            if (other is ReversiGame reversiGame)
            {
                var otherScores = reversiGame.GetScores();
                var thisScores = GetScores(playerScore, computerScore);

                if (thisScores['X'] > otherScores['X'] || (thisScores['X'] == otherScores['X'] && thisScores['O'] > otherScores['O']))
                {
                    return 1;
                }
                else if (thisScores['X'] == otherScores['X'] && thisScores['O'] == otherScores['O'])
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                throw new ArgumentException("Error");
            }
        }

        public static char[][] GetBoardWithValidMoves(char[][] board, char tile)
        {
            char[][] dupeBoard = GetBoardCopy(board);

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (IsValidMove(dupeBoard, tile, x, y))
                    {
                        dupeBoard[x][y] = '.';
                    }
                }
            }

            return dupeBoard;
        }

    }
}
