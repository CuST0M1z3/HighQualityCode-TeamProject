namespace Labyrinth.Interfaces
{
    using Labyrinth.Enumerations;

    public interface IPlayer
    {
        int XPosition { get; }
        int YPosition { get; }
        void Move(Directions direction);
    }
}
