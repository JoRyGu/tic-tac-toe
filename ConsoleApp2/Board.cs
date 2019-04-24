using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class Board
    {
        private Piece[,] _board;
        private Dictionary<int, int[]> _positions;
        private int[] _lastMove;    

        public Board()
        {
            this._board = new Piece[3, 3];
            this._lastMove = new int[] {100, 100};
            this._positions = new Dictionary<int, int[]>();

            _positions.Add(1, new int[] {0, 0});
            _positions.Add(2, new int[] {0, 1});
            _positions.Add(3, new int[] {0, 2});
            _positions.Add(4, new int[] {1, 0});
            _positions.Add(5, new int[] {1, 1});
            _positions.Add(6, new int[] {1, 2});
            _positions.Add(7, new int[] {2, 0});
            _positions.Add(8, new int[] {2, 1});
            _positions.Add(9, new int[] {2, 2});
        }

        public void ResetBoard()
        {
            this._board = new Piece[3, 3];
        }

        public void SetPiece(Piece piece, int position)
        {
            if (position < 1 || position > 9)
            {
                throw new IndexOutOfRangeException("Invalid space. Try again.");
            } 
            
            var boardPosition = this._positions[position];
            
            if (this._board[boardPosition[0], boardPosition[1]] == Piece.None)
            {
                this._board[boardPosition[0], boardPosition[1]] = piece;
                this._lastMove = boardPosition;
            }
            else
            {
                throw new SpaceOccupiedException();
            }
        }
        
        private bool CheckWinCol()
        {
            var column = this._lastMove[1];
            bool isWinner;
            foreach (KeyValuePair<int, int[]> position in _positions)
            {
                if (position.Value[1] == column)
                {
                    isWinner = _board[position.Value[0], position.Value[1]] == _board[_lastMove[0], _lastMove[1]];
                    if (!isWinner) return false;
                }
            }

            return true;
        }

        private bool CheckWinRow()
        {
            var row = this._lastMove[0];
            bool isWinner;
            foreach (KeyValuePair<int, int[]> position in _positions)
            {
                if (position.Value[0] == row)
                {
                    isWinner = _board[position.Value[0], position.Value[1]] == _board[_lastMove[0], _lastMove[1]];
                    if (!isWinner) return false;
                }
            }

            return true;
        }
        
        private bool CheckWinDiag()
        {
            if ((_board[0, 0] == _board[1, 1] && _board[0, 0] == _board[2, 2] && _board[0,0] != Piece.None) ||
                (_board[0, 2] == _board[1, 1] && _board[0, 2] == _board[2, 0] && _board[0,2] != Piece.None))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public bool CheckWin()
        {
            Console.WriteLine(CheckWinCol());
            Console.WriteLine(CheckWinRow());
            Console.WriteLine(CheckWinDiag());
            return CheckWinCol() || CheckWinRow() || CheckWinDiag();
        } 
        
        public void PrintBoard()
        {
            Console.Clear();
            var spaces = new string[9];

            for (var i = 1; i < 10; i++)
            {
                var yPos = _positions[i][0];
                var xPos = _positions[i][1];

                if (_board[yPos, xPos] == Piece.None)
                {
                    spaces[i - 1] = string.Format("{0}", i);
                }
                else if (_board[yPos, xPos] == Piece.Player1)
                {
                    spaces[i - 1] = "X";
                }
                else
                {
                    spaces[i - 1] = "Y";
                }
            }

            Console.WriteLine(" {0} | {1} | {2} \n------------\n {3} | {4} | {5} \n------------\n {6} | {7} | {8} ",
                spaces);
        }
    }
}