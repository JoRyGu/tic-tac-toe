using System;

namespace ConsoleApp2
{
    public class TicTacToeException : Exception
    {
        public TicTacToeException(string message)
            : base(message)
        {
        }
    }

    public class SpaceOccupiedException : TicTacToeException
    {
        public SpaceOccupiedException()
            : base("This space has already been played.")
        {
        }
    }
}