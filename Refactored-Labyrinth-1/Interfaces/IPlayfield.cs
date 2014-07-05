namespace Labyrinth.Interfaces
{
    using Labyrinth.Players;

    public interface IPlayfield
    {
        bool IsPlayerIsInPlayfieldRange(Player player);
        bool IsPlayerWinning(Player player);
        bool IsValidMovePosition(Player player);
        bool IsBlankMovePosition(Player player);
        void PrintPlayfield(Player player);
        void ResetPlayfield();
    }
}
