using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Labyrinth.Dialogs
{
    public class GameDialog
    {
        public void Invalid()
        {
            Console.WriteLine("* Invalid move!");
        }
        public void move()
        {
            Console.Write("Enter your move (L=left, R=right, U=up, D=down): ");
        }

        public void intro()
        {
            Console.WriteLine("Welcome to \"Labyrinth\" game. Please try to escape. Use 'top' to view the top scoreboard, 'restart' to start a new game and 'exit' to quit the game.");
        }
        public void nl()
        {



            Console.WriteLine();
        }
        public void win(int moves)
        {
            Console.Write("Congratulations! You escaped in {0} moves.\nPlease enter your name for the top scoreboard: ", moves);



        }
        public void playing()
        {
            Console.WriteLine("You are playing \"Labyrinth\" game. Please try to escape. Use 'top' to view the top scoreboard, 'restart' to start a new game and 'exit' to quit the game.");
        }
    }
}
