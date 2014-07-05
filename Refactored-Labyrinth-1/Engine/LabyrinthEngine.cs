using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Labyrinth.Engine
{
    using System;
    using System.Linq;
    using Labyrinth.ConsoleUI;
    using Labyrinth.Dialogs;
    using Labyrinth.Enumerations;
    using Labyrinth.Statistics;
    using Labyrinth.Players;

    public class LabyrinthEngine
    {
        static Playfield playfield = new Playfield(); //-> Remake into Singleton Desing Pattern (Creational 1)
        static GameDialog dialog = new GameDialog();
        static Scoreboard scores;
        static Player player = new Player();
        static int numberOfMoves = 0;

        //Remake to a Builder Design Pattern (Creational 2)
        static void StartNewGame()
        {
            player = new Player();
            dialog.IntroMessage();
            playfield.ResetPlayfield();
            Console.WriteLine();
            playfield.PrintPlayfield(player);
            numberOfMoves = 0;
        }

        static void Main(string[] args)
        {

            StartNewGame();
            scores = new Scoreboard();
            String input = "";
            dialog.EnterYourMoveMessage();
            //Remake using Command Desing Pattern (Behavioral 1)
            while ((input = Console.ReadLine()) != "exit")
            {
                switch (input)
                {
                    case "top":
                        scores.ShowStatistics();
                        break;
                    case "restart":
                        StartNewGame();
                        break;
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
                            dialog.InvalidMoveMessage();
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
                            dialog.InvalidMoveMessage();
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
                            dialog.InvalidMoveMessage();
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
                            dialog.InvalidMoveMessage();
                        }
                        break;
                    default:
                        dialog.InvalidMoveMessage();
                        break;
                }

                if (playfield.IsPlayerWinning(player))  //-> Observer Pattern (Behavioral 2)
                {
                    dialog.WinnerMessage(numberOfMoves);
                    string name = Console.ReadLine();
                    try
                    {
                        scores.AddTopScoreToScoreboard(name, numberOfMoves);
                    }
                    finally
                    {

                    }
                    Console.WriteLine();
                    StartNewGame();
                }
                dialog.EnterYourMoveMessage();
            }
            Console.Write("Good Bye!");
            Console.ReadKey();
        }
    }
}
