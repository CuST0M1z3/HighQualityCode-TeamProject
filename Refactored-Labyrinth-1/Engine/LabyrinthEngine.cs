namespace Labyrinth.Engine
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
            this.dialogs.IntroMessage();
            this.playfield.ResetPlayfield();
            Console.WriteLine();
            this.playfield.PrintPlayfield(player);
            this.save = new SaveSystem();
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
                        this.save.Memento = SaveMemento();
                        break;
                    case "LOAD":
                        LoadMemento(this.save.Memento);
                        this.playfield.PrintPlayfield(player);
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
            this.playfield.PrintPlayfield(this.player);
            return new Memento(this.factory, this.playfield, this.player, this.dialogs, this.scoreboard, this.numberOfMoves);
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
