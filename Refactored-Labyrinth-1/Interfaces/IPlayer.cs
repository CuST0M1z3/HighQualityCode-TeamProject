namespace Labyrinth.Interfaces
{
    using Labyrinth.Enumerations;

    public interface IPlayer
    {
        int XPosition { get; }
        int YPosition { get; }
        void Move(Directions direction);
        void Update(IPlayer player, IPlayfield playfield, int numberOfMoves, IScoreboard scoreboard, IGameDialog dialogs);
    }
}