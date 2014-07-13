namespace Labyrinth.Interfaces
{
    using Labyrinth.Enumerations;

    public interface IPlayfield
    {
        bool IsPlayerIsInPlayfieldRange(IPlayer player);
        bool IsPlayerWinning(IPlayer player);
        bool IsValidMovePosition(IPlayer player, Directions direction);
        bool IsBlankMovePosition(IPlayer player);
        string PrintPlayfield(IPlayer player);
        void ResetPlayfield();
        int[,] GetLabyrinthArray();
        void SetLabyrinthArray(int[,] playfieldArray);
    }

}
