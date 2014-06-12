using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Labyrinth.Enumerations;

namespace Labyrinth.GameUI
{
    class Playfield
    {
        int[,] labyrinth = new int[7, 7];
        Position player;




        public bool isWinning()
        {
            return player.isWinning();
        }
        public bool move(Direction direction)
        {
            if (isValidMove(player, direction))
                player.move(direction);
            else return false;
            return true;
        }

        bool isValidPosition(Position position)
        {



            return labyrinth[position.x, position.y] == 0 && position.isValidPosition();
        }

        bool isValidMove(Position position, Direction direction)
        {
            if (position.isWinning()) return false;

            Position newPosition = new Position(position.x, position.y);

            newPosition.move(direction);

            return isValidPosition(newPosition);
        }

        bool isBlankPosition(Position position)
        {
            return labyrinth[position.x, position.y] == -1;
        }
        bool isBlankMove(Position position, Direction direction)
        {
            Position newPosition = new Position(position.x, position.y);




            newPosition.move(direction);

            return isBlankPosition(newPosition);
        }


        public void print()
        {
            for (int temp2 = 0; temp2 < 7; temp2++)
            {

                for (int temp1 = 0; temp1 < 7; temp1++)
                {
                    if (player.x == temp1 && player.y == temp2) Console.Write("*");
                    else
                    {
                        if (labyrinth[temp1, temp2] == 0) Console.Write("-");
                        else
                        {
                            if (labyrinth[temp1, temp2] == 1) Console.Write("X");
                            else
                            {


                                Console.Write("+");
                            }
                        }
                    }
                }
                Console.WriteLine();
            }
        }
        public void reset()
        {
            player = new Position();

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    labyrinth[i, j] = -1;
                }
            }

            labyrinth[3, 3] = 0;

            Direction d = Direction.Blank;
            Random random = new Random();
            Position tempPos2 = new Position();
            while (!tempPos2.isWinning())
            {
                do
                {
                    int randomNumber = random.Next() % 4;
                    d = (Direction)(randomNumber);
                } while (!isBlankMove(tempPos2, d));

                tempPos2.move(d);

                labyrinth[tempPos2.x, tempPos2.y] = 0;
            }
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (labyrinth[i, j] == -1)
                    {
                        int randomNumber = random.Next();
                        if (randomNumber % 3 == 0) labyrinth[i, j] = 0;
                        else labyrinth[i, j] = 1;
                    }
                }
            }
        }
    }
}
