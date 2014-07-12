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
            while ((input = Console.ReadLine()) != "exit")
            {
                switch (input)
                {
                    case "top":
                        this.scoreboard.ShowStatistics();
                        break;
                    case "restart":
                        StartNewGame();
                        break;
                    case "exit":
                    case "save":
                        SaveMemento();
                        break;
                    case "load":
                        LoadMemento(this.save);
                        break;
                    case "L":
                    case "l":
                        player.Move(Directions.Left);
                        if (playfield.IsValidMovePosition(player))
                        {
                            numberOfMoves++;
                            playfield.PrintPlayfield(player);
                        }
                        else
                        {
                            player.Move(Directions.Right);
                            this.dialogs.InvalidMoveMessage();
                        }
                        break;
                    case "U":
                    case "u":
                        player.Move(Directions.Up);
                        if (playfield.IsValidMovePosition(player))
                        {
                            numberOfMoves++;
                            playfield.PrintPlayfield(player);
                        }
                        else
                        {
                            player.Move(Directions.Down);
                            this.dialogs.InvalidMoveMessage();
                        }
                        break;
                    case "R":
                    case "r":
                        player.Move(Directions.Right);
                        if (playfield.IsValidMovePosition(player))
                        {
                            numberOfMoves++;
                            playfield.PrintPlayfield(player);
                        }
                        else
                        {
                            player.Move(Directions.Left);
                            this.dialogs.InvalidMoveMessage();
                        }
                        break;
                    case "D":
                    case "d":
                        player.Move(Directions.Down);
                        if (playfield.IsValidMovePosition(player))
                        {
                            numberOfMoves++;
                            playfield.PrintPlayfield(player);
                        }
                        else
                        {
                            player.Move(Directions.Up);
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
