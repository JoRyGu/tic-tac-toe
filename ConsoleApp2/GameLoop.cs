using System;

namespace ConsoleApp2
{
    public class GameLoop
    {
        private readonly Board _board;
        private int _turn;
        private bool _isXTurn;

        public GameLoop()
        {
            _board = new Board();
            _turn = 0;
            _isXTurn = true;
        }

        public void Run()
        {
            var isRunning = true;

            while (isRunning)
            {
                if (CheckTie())
                {
                    Console.WriteLine("Game is a tie!");
                    Console.WriteLine("Would you like to play again (Y/N)?");
                    var playAgain = Console.ReadLine();

                    if (playAgain.ToLower() == "y")
                    {
                        ResetGame();
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                }
                _board.PrintBoard();
                
                var piece = _isXTurn ? Piece.Player1 : Piece.Player2;
                var playerName = _isXTurn ? "Player 1" : "Player 2";

                Console.WriteLine("{0}, which space will you choose?", playerName);


                try
                {
                    var space = int.Parse(Console.ReadLine());
                    _board.SetPiece(piece, space);

                    var hasWinner = _board.CheckWin();
                    

                    if (hasWinner)
                    {
                        _board.PrintBoard();
                        var winner = _isXTurn ? "Player 1" : "Player 2";
                        Console.WriteLine("{0} wins the game!", winner);
                        Console.WriteLine("Play another game?");

                        var playAgain = Console.ReadLine();
                        if (playAgain.ToLower() == "y")
                        {
                            ResetGame();
                        }
                        else
                        {
                            Environment.Exit(0);
                        }
                    }
                    else
                    {
                        _turn++;
                        _isXTurn = !_isXTurn;
                    }
                    
                }
                catch (SpaceOccupiedException)
                {
                    Console.WriteLine("That space is already occupied, try again.");
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Invalid space entered, try again.");
                }
            }
        }

        private bool CheckTie()
        {
            return _turn > 8;
        }

        private void ResetGame()
        {
            _board.ResetBoard();
            _turn = 0;
            _isXTurn = true;
        }

        
    }
}