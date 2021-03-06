﻿namespace Labyrinth.ConsoleUI
{
    using System;
    using Labyrinth.Enumerations;
    using Labyrinth.Players;
    using Labyrinth.Interfaces;
    using System.Text;

    public sealed class Playfield : IPlayfield
    {
        private const int PLAYFIELD_SIZE = 7;
        private int[,] Labyrinth { get; set; }

        private static readonly Playfield field = new Playfield();

        public static Playfield Instance
        {
            get
            {
                return field;
            }
        }

        private Playfield()
        {
            this.Labyrinth = new int[PLAYFIELD_SIZE, PLAYFIELD_SIZE];
        }

        public int[,] GetLabyrinthArray()
        {
            return this.Labyrinth;
        }

        public void SetLabyrinthArray(int[,] playfieldArray)
        {
            this.Labyrinth = playfieldArray;
        }

        public bool IsPlayerIsInPlayfieldRange(IPlayer player)
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

        public bool IsPlayerWinning(IPlayer player)
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

        public bool IsValidMovePosition(IPlayer player, Directions direction)
        {
            IPlayer newPlayer = new Player(player.XPosition, player.YPosition);
            newPlayer.Move(direction);
            return ((Labyrinth[newPlayer.XPosition, newPlayer.YPosition] == 0) && (IsPlayerIsInPlayfieldRange(newPlayer)));
        }

        public bool IsBlankMovePosition(IPlayer player)
        {
            return Labyrinth[player.XPosition, player.YPosition] == -1;
        }

        public string PrintPlayfield(IPlayer player)
        {
            StringBuilder result = new StringBuilder();
            for (int playfieldCol = 0; playfieldCol < PLAYFIELD_SIZE; playfieldCol++)
            {
                for (int playfieldRow = 0; playfieldRow < PLAYFIELD_SIZE; playfieldRow++)
                {
                    if (player.XPosition == playfieldRow && player.YPosition == playfieldCol)
                    {
                        result.Append("*");
                    }
                    else
                    {
                        if (Labyrinth[playfieldRow, playfieldCol] == 0)
                        {
                            result.Append("-");
                        }
                        else
                        {
                            result.Append("X");
                        }
                    }
                }
                result.Append("\n");
            }

            return result.ToString();
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

            IPlayer player = new Player();
            Labyrinth[player.XPosition, player.YPosition] = 0;

            Directions direction = Directions.B;
            Random randomGenerator = new Random();
            
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

        }

    }
}
