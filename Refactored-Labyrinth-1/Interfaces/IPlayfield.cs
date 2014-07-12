namespace Labyrinth.Interfaces
{
    using Labyrinth.Enumerations;

    public interface IPlayfield
    {
        bool IsPlayerIsInPlayfieldRange(IPlayer player);
        bool IsPlayerWinning(IPlayer player);
        bool IsValidMovePosition(IPlayer player, Directions direction);
        bool IsBlankMovePosition(IPlayer player);
        void PrintPlayfield(IPlayer player);
        void ResetPlayfield();
    }

}
