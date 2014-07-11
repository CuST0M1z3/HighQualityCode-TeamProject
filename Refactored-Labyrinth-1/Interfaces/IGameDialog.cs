namespace Labyrinth.Interfaces
{
    public interface IGameDialog
    {
        void IntroMessage();
        void InvalidMoveMessage();
        void InvalidCommandMessage();
        void EnterYourMoveMessage();
        void WinnerMessage(int numberOfMoves);
    }
}
