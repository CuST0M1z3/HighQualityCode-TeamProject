﻿namespace Labyrinth.Engine
{
    using System;
    using Labyrinth.Interfaces;
    using Labyrinth.Memento;
    using Labyrinth.Players;
    using Labyrinth.Enumerations;

    public sealed class LabyrinthEngine : ILabyrinthEngine
    {
        private static readonly LabyrinthEngine singleInstance = new LabyrinthEngine();

        private ILabyrinthFactory factory;
        private IPlayfield playfield;
        private IPlayer player;
        private IGameDialog dialogs;
        private IScoreboard scoreboard;
        private int numberOfMoves = 0;
        private Memento save;
     

        private LabyrinthEngine()
        {
            this.factory = new LabyrinthFactory();
            this.playfield = this.factory.CreatePlayfield();
            this.player = this.factory.CreatePlayer();
            this.dialogs = this.factory.CreateDialogs();
            this.scoreboard = this.factory.CreateScoreboard();
            this.save = new Memento(this.factory, this.playfield, this.player, this.dialogs, this.scoreboard);
        }

        public static LabyrinthEngine Instance
        {
            get
            {
                return singleInstance;
            }
        }

        public void StartNewGame()
        {
            this.player = this.factory.CreatePlayer();
            this.dialogs.IntroMessage();
            this.playfield.ResetPlayfield();
            Console.WriteLine();
            this.playfield.PrintPlayfield(player); 
            numberOfMoves = 0;
            ReadCommands();
        }

        private void ReadCommands()
        {
            String input = "";
            this.dialogs.EnterYourMoveMessage();
            while ((input = Console.ReadLine().ToUpper()) != "exit")
            {
                switch (input)
                {
                    case "TOP":
                        this.scoreboard.ShowStatistics();
                        break;
                    case "RESTART":
                        StartNewGame();
                        break;
                    case "EXIT":
                        break;
                    case "SAVE":
                        SaveMemento();
                        break;
                    case "LOAD":
                        LoadMemento(this.save);
                        break;
                    case "L":
                        if (playfield.IsValidMovePosition(player, Directions.Left))
                        {
                            player.Move(Directions.Left);
                            numberOfMoves++;
                            playfield.PrintPlayfield(player);
                        }
                        else
                        {
                            this.dialogs.InvalidMoveMessage();
                        }
                        break;
                    case "U":
                        if (playfield.IsValidMovePosition(player, Directions.Up))
                        {
                            player.Move(Directions.Up);
                            numberOfMoves++;
                            playfield.PrintPlayfield(player);
                        }
                        else
                        {
                            this.dialogs.InvalidMoveMessage();
                        }
                        break;
                    case "R":
                        if (playfield.IsValidMovePosition(player, Directions.Right))
                        {
                            player.Move(Directions.Right);
                            numberOfMoves++;
                            playfield.PrintPlayfield(player);
                        }
                        else
                        {
                            this.dialogs.InvalidMoveMessage();
                        }
                        break;
                    case "D":
                        if (playfield.IsValidMovePosition(player, Directions.Down))
                        {
                            player.Move(Directions.Down);
                            numberOfMoves++;
                            playfield.PrintPlayfield(player);
                        }
                        else
                        {
                            this.dialogs.InvalidMoveMessage();
                        }
                        break;
                    default:
                        this.dialogs.InvalidCommandMessage();
                        break;
                }

                if (playfield.IsPlayerWinning(player))  //-> Observer Pattern (Behavioral 2)
                {
                    this.dialogs.WinnerMessage(numberOfMoves);
                    string name = Console.ReadLine();
                    try
                    {
                        this.scoreboard.AddTopScoreToScoreboard(name, numberOfMoves);
                    }
                    finally
                    {

                    }
                    Console.WriteLine();
                    StartNewGame();
                }
                this.dialogs.EnterYourMoveMessage();
            }
            Console.Write("Good Bye!");
            Environment.Exit(0);
        }

        public Memento SaveMemento()
        {
            Console.WriteLine("\nSaving state --\n");
            this.playfield.PrintPlayfield(player);
            return new Memento(this.factory, this.playfield, this.player, this.dialogs, this.scoreboard);
        }

        public void LoadMemento(Memento save)
        {
            Console.WriteLine("\nRestoring state --\n");
            this.factory = save.Factory;
            this.playfield = save.Playfield;
            this.player = save.Player;
            this.dialogs = save.Dialogs;
            this.scoreboard = save.Scoreboard;
            this.playfield.PrintPlayfield(player);
        }
    }
}
