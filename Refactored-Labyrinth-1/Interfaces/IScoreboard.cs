namespace Labyrinth.Interfaces
{
    public interface IScoreboard
    {
        void ShowStatistics();
        void AddTopScoreToScoreboard(string playerName, int playerScore);
    }
}
