namespace Labyrinth.Engine
{
    using System;
    using Labyrinth.Interfaces;
    using Labyrinth.Dialogs;
    using Labyrinth.Statistics;
    using Labyrinth.ConsoleUI;
    using Labyrinth.Players;

    public class LabyrinthFactory : ILabyrinthFactory
    {
        public IPlayfield CreatePlayfield()
        {
            return new Playfield();
        }

        public IGameDialog CreateDialogs()
        {
            return new GameDialog();
        }

        public IPlayer CreatePlayer()
        {
            return new Player();
        }

        public IScoreboard CreateScoreboard()
        {
            return new Scoreboard();
        }
    }
}
