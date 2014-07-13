namespace Labyrinth.Interfaces
{
    public interface IGameDialog
    {
        string IntroMessage();
        string InvalidMoveMessage();
        string InvalidCommandMessage();
        string EnterYourMoveMessage();
        string WinnerMessage(int numberOfMoves);
    }
}
