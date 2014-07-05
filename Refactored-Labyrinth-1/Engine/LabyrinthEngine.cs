namespace Labyrinth.Engine
{
    using System;
    using Labyrinth.Interfaces;
    using Labyrinth.Players;
    using Labyrinth.Enumerations;

    public sealed class LabyrinthEngine : ILabyrinthEngine
    {
        private static readonly LabyrinthEngine singleInstance = new LabyrinthEngine();

        private ILabyrinthFactory factory;
        private IPlayfield playfield; //-> Remake into Singleton Desing Pattern (Creational 1)
        private IPlayer player;
        private IGameDialog dialogs;
        private IScoreboard scoreboard;
        private int numberOfMoves = 0;

        private LabyrinthEngine()
        {
            this.factory = new LabyrinthFactory();
            this.playfield = this.factory.CreatePlayfield();
            this.player = this.factory.CreatePlayer();
            this.dialogs = this.factory.CreateDialogs();
            this.scoreboard = this.factory.CreateScoreboard();
        }

        public static LabyrinthEngine Instance
        {
            get
            {
                return singleInstance;
            }
        }

        //Remake to a Builder Design Pattern (Creational 2)
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
            //Remake using Command Desing Pattern (Behavioral 1)
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

                    case "L":
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
                        this.dialogs.InvalidMoveMessage();
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
    }
}
