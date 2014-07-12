namespace Labyrinth.Memento
{
    using Labyrinth.Interfaces;

    public class Memento
    {
        public Memento(ILabyrinthFactory factory, IPlayfield playfield, IPlayer player, IGameDialog dialogs, IScoreboard scoreboard, int numberOfMoves)
        {
            this.Factory = factory;
            this.Playfield = playfield;
            this.Player = player;
            this.Dialogs = dialogs;
            this.Scoreboard = scoreboard;
            this.NumberOfMoves = numberOfMoves;
        }

        public ILabyrinthFactory Factory { get; set; }

        public IPlayfield Playfield { get; set; }

        public IPlayer Player { get; set; }

        public IGameDialog Dialogs { get; set; }

        public IScoreboard Scoreboard { get; set; }

        public int NumberOfMoves { get; set; }
    }
}
