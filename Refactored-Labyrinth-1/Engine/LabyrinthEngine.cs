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
        private SaveSystem save;

        
        private LabyrinthEngine()
        {
            this.factory = new LabyrinthFactory();
            this.playfield = this.factory.CreatePlayfield();
            this.player = this.factory.CreatePlayer();
            this.dialogs = this.factory.CreateDialogs();
            this.scoreboard = this.factory.CreateScoreboard();
            this.save = new SaveSystem();
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
            Console.WriteLine(this.dialogs.IntroMessage());
            this.playfield.ResetPlayfield();
            Console.WriteLine();
            Console.WriteLine(this.playfield.PrintPlayfield(player));
            numberOfMoves = 0;
            ReadCommands();
        }

        private void ReadCommands()
        {
            String input = "";
            Console.Write(this.dialogs.EnterYourMoveMessage());
            while ((input = Console.ReadLine().ToUpper()) != "EXIT")
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
                        this.save.Memento = SaveMemento();
                        break;
                    case "LOAD":
                        LoadMemento(this.save.Memento);
                        Console.WriteLine(this.playfield.PrintPlayfield(player));
                        break;
                    default:
                        try
                        {
                            Directions direction = (Directions)Enum.Parse(typeof(Directions), input);

                            if (playfield.IsValidMovePosition(player, direction))
                            {
                                player.Move(direction);
                                numberOfMoves++;
                                Console.WriteLine(this.playfield.PrintPlayfield(player));
                                player.Update(this.player, this.playfield, this.numberOfMoves, this.scoreboard, this.dialogs);
                            }
                            else
                            {
                                Console.WriteLine(this.dialogs.InvalidMoveMessage());
                            }
                            break;
                        }
                        catch
                        {
                            Console.WriteLine(this.dialogs.InvalidCommandMessage());
                            break;
                        }
                }

               
                Console.Write(this.dialogs.EnterYourMoveMessage());
            }
            Console.Write("Good Bye!");
            Environment.Exit(0);
        }

        public Memento SaveMemento()
        {
            Console.WriteLine("\nSaving state --\n");
            Console.WriteLine(this.playfield.PrintPlayfield(this.player));
            return new Memento(this.factory, this.playfield, new Player(this.player.XPosition, this.player.YPosition), this.dialogs, this.scoreboard, this.numberOfMoves);
        }

        public void LoadMemento(Memento restore)
        {
            Console.WriteLine("\nRestoring state --\n");
            this.factory = restore.Factory;
            this.playfield = restore.Playfield;
            this.player = restore.Player;
            this.dialogs = restore.Dialogs;
            this.scoreboard = restore.Scoreboard;
            this.numberOfMoves = restore.NumberOfMoves;
        }

    }
}
