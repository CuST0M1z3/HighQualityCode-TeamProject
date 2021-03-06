﻿namespace Labyrinth.Engine
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
            var field = Playfield.Instance;
            return field;
        }

        public IGameDialog CreateDialogs()
        {
            return new GameDialog();
        }

        public IPlayer CreatePlayer()
        {
            return new Player();
        }

        public IPlayer CreatePlayer(int xPosition, int yPosition)
        {
            return new Player(xPosition, yPosition);
        }      

        public IScoreboard CreateScoreboard()
        {
            return new Scoreboard();
        }
    }
}
