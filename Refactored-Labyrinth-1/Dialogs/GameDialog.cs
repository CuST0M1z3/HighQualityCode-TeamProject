namespace Labyrinth.Dialogs
{
    using System;
    using Labyrinth.Interfaces;

    public class GameDialog : IGameDialog
    {
        public void InvalidMoveMessage()
        {
            Console.WriteLine("Invalid move!");
        }

        public void InvalidCommandMessage()
        {
            Console.WriteLine("Invalid command!");
        }

        public void EnterYourMoveMessage()
        {
            Console.Write("Enter your move (L=left, R=right, U=up, D=down): ");
        }

        public void IntroMessage()
        {
            Console.WriteLine("Welcome to \"Labyrinth\" game. Please try to escape. Use 'top' to view the top scoreboard, 'restart' to start a new game and 'exit' to quit the game.");
        }

        public void WinnerMessage(int numberOfMoves)
        {
            Console.Write("Congratulations! You escaped in {0} moves.\nPlease enter your name for the top scoreboard: ", numberOfMoves);
        }
    }
}
