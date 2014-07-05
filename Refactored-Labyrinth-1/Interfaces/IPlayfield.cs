namespace Labyrinth.Interfaces
{

    public interface IPlayfield
    {
        bool IsPlayerIsInPlayfieldRange(IPlayer player);
        bool IsPlayerWinning(IPlayer player);
        bool IsValidMovePosition(IPlayer player);
        bool IsBlankMovePosition(IPlayer player);
        void PrintPlayfield(IPlayer player);
        void ResetPlayfield();
    }

}
