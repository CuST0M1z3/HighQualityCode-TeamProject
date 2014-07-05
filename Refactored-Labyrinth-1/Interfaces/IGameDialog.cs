namespace Labyrinth.Interfaces
{
    public interface IGameDialog
    {
        void IntroMessage();
        void InvalidMoveMessage();
        void EnterYourMoveMessage();
        void WinnerMessage(int numberOfMoves);
    }
}
