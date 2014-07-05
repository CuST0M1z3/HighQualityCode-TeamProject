namespace Labyrinth.Interfaces
{
    public interface ILabyrinthFactory
    {
        IPlayfield CreatePlayfield();
        IGameDialog CreateDialogs();
        IPlayer CreatePlayer();
        IScoreboard CreateScoreboard();
    }
}
