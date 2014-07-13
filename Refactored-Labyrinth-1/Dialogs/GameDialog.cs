namespace Labyrinth.Dialogs
{
    using System;
    using Labyrinth.Interfaces;

    public class GameDialog : IGameDialog
    {
        private const string INVALID_MOVE_MESSAGE = "Invalid move!";
        private const string INVALID_COMMAND_MESSAGE = "Invalid command!";
        private const string ENTER_MOVE_MESSAGE = "Enter your move (L=left, R=right, U=up, D=down): ";
        private const string INTRO_MESSAGE = "Welcome to \"Labyrinth\" game. Please try to escape. Use 'top' to view the top scoreboard, 'restart' to start a new game and 'exit' to quit the game.";
        private const string WINNER_MESSAGE = "Congratulations! You escaped in {0} moves.\nPlease enter your name for the top scoreboard: ";

        public string InvalidMoveMessage()
        {
            return INVALID_MOVE_MESSAGE;
        }

        public string InvalidCommandMessage()
        {
            return INVALID_COMMAND_MESSAGE;
        }

        public string EnterYourMoveMessage()
        {
            return ENTER_MOVE_MESSAGE;
        }

        public string IntroMessage()
        {
            return INTRO_MESSAGE;
        }

        public string WinnerMessage(int numberOfMoves)
        {
            string winnerMsg = String.Format(WINNER_MESSAGE, numberOfMoves);
            return winnerMsg;
        }
    }
}
