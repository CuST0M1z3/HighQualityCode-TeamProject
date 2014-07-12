namespace Labyrinth.Memento
{
    using Labyrinth.Interfaces;

    public class Memento
    {
        public Memento(ILabyrinthFactory factory, IPlayfield playfield, IPlayer player, IGameDialog dialogs, IScoreboard scoreboard)
        {
            this.Factory = factory;
            this.Playfield = playfield;
            this.Player = player;
            this.Dialogs = dialogs;
            this.Scoreboard = scoreboard;
        }

        public ILabyrinthFactory Factory { get; set; }

        public IPlayfield Playfield { get; set; }

        public IPlayer Player { get; set; }

        public IGameDialog Dialogs { get; set; }

        public IScoreboard Scoreboard { get; set; }
    }
}
