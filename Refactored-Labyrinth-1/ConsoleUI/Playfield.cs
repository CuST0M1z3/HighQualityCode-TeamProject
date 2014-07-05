namespace Labyrinth.ConsoleUI
{
    using System;
    using Labyrinth.Enumerations;
    using Labyrinth.Players;
    using Labyrinth.Interfaces;

    public class Playfield : IPlayfield
    {
        private const int PLAYFIELD_SIZE = 7;
        private int[,] Labyrinth { get; set; }        

        public Playfield()
        {
            this.Labyrinth = new int[PLAYFIELD_SIZE, PLAYFIELD_SIZE];
        }

        public bool IsPlayerIsInPlayfieldRange(Player player)
        {
            if (0 <= player.XPosition && player.XPosition < PLAYFIELD_SIZE && 0 <= player.YPosition && player.YPosition < PLAYFIELD_SIZE)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsPlayerWinning(Player player)
        {
            if (player.XPosition == 0 || player.XPosition == (PLAYFIELD_SIZE - 1) || player.YPosition == 0 || player.YPosition == (PLAYFIELD_SIZE - 1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //public bool move(Direction direction)
        //{
        //    if (isValidMove(player, direction))
        //        player.move(direction);
        //    else return false;
        //    return true;
        //}

        public bool IsValidMovePosition(Player player)
        {
            return ((Labyrinth[player.XPosition, player.YPosition] == 0) && (IsPlayerIsInPlayfieldRange(player)));
        }

        //bool isValidMove(Player position, Direction direction)
        //{
        //    if (position.isWinning()) return false;

        //    Player newPosition = new Player(position.x, position.y);

        //    newPosition.move(direction);

        //    return IsValidPosition(newPosition);
        //}

        public bool IsBlankMovePosition(Player player)
        {
            return Labyrinth[player.XPosition, player.YPosition] == -1;
        }

        //bool isBlankPosition(Player position)
        //{
        //    return Labyrinth[position.x, position.y] == -1;
        //}
        //bool isBlankMove(Player position, Direction direction)
        //{
        //    Player newPosition = new Player(position.x, position.y);




        //    newPosition.move(direction);

        //    return isBlankPosition(newPosition);
        //}

        public void PrintPlayfield(Player player)
        {
            for (int playfieldCol = 0; playfieldCol < PLAYFIELD_SIZE; playfieldCol++)
            {
                for (int playfieldRow = 0; playfieldRow < PLAYFIELD_SIZE; playfieldRow++)
                {
                    if (player.XPosition == playfieldRow && player.YPosition == playfieldCol)
                    {
                        Console.Write("*");
                    }
                    else
                    {
                        if (Labyrinth[playfieldRow, playfieldCol] == 0)
                        {
                            Console.Write("-");
                        }
                        else
                        {
                            Console.Write("X");
                        }
                    }
                }
                Console.WriteLine();
            }
        }

        public void ResetPlayfield()
        {
            for (int playfieldRow = 0; playfieldRow < PLAYFIELD_SIZE; playfieldRow++)
            {
                for (int playefieldCol = 0; playefieldCol < PLAYFIELD_SIZE; playefieldCol++)
                {
                    Labyrinth[playfieldRow, playefieldCol] = -1;
                }
            }

            Player player = new Player();
            Labyrinth[player.XPosition, player.YPosition] = 0;

            Directions direction = Directions.Blank;
            Random randomGenerator = new Random();

            //while (!tempPos2.isWinning())
            //{
            //    do
            //    {
            //        int randomNumber = random.Next() % 4;
            //        d = (Direction)(randomNumber);
            //    } while (!isBlankMove(tempPos2, d));

            //    tempPos2.move(d);

            //    Labyrinth[tempPos2.x, tempPos2.y] = 0;
            //}

            while (true)
            {
                if (IsPlayerWinning(player))
                {
                    break;
                }
                else
                {
                    do
                    {
                        int randomDirection = randomGenerator.Next() % 4;
                        direction = (Directions)(randomDirection);
                        player.Move(direction);
                    }
                    while (!IsBlankMovePosition(player));
                    Labyrinth[player.XPosition, player.YPosition] = 0;
                }
            }

            for (int playfieldRow = 0; playfieldRow < PLAYFIELD_SIZE; playfieldRow++)
            {
                for (int playefieldCol = 0; playefieldCol < PLAYFIELD_SIZE; playefieldCol++)
                {
                    if (Labyrinth[playfieldRow, playefieldCol] == -1)
                    {
                        int randomNumber = randomGenerator.Next();
                        if (randomNumber % 3 == 0)
                        {
                            Labyrinth[playfieldRow, playefieldCol] = 0;
                        }
                        else
                        {
                            Labyrinth[playfieldRow, playefieldCol] = 1;
                        } 
                    }
                }
            }

            //while (!tempPos2.isWinning())
            //{
            //    do
            //    {
            //        int randomNumber = random.Next() % 4;
            //        d = (Direction)(randomNumber);
            //    } while (!isBlankMove(tempPos2, d));

            //    tempPos2.move(d);

            //    Labyrinth[tempPos2.x, tempPos2.y] = 0;
            //}
            //for (int i = 0; i < 7; i++)
            //{
            //    for (int j = 0; j < 7; j++)
            //    {
            //        if (Labyrinth[i, j] == -1)
            //        {
            //            int randomNumber = random.Next();
            //            if (randomNumber % 3 == 0) Labyrinth[i, j] = 0;
            //            else Labyrinth[i, j] = 1;
            //        }
            //    }
            //}
        }

    }
}
