namespace Labyrinth.Players
{
    using System;
    using Labyrinth.Enumerations;
    using Labyrinth.Engine;
    using Labyrinth.Interfaces;

    public class Player : IPlayer
    {
        private const int DEFAULT_X_POSITTION = 3;
        private const int DEFAULT_Y_POSITTION = 3;
        private int xPosition;
        private int yPosition;

        public Player()
        {
            this.XPosition = DEFAULT_X_POSITTION;
            this.YPosition = DEFAULT_Y_POSITTION;
        }
        public Player(int xPosition, int yPosition)
        {
            this.XPosition = xPosition;
            this.YPosition = yPosition;
        }

        public int XPosition
        {
            get
            {
                return this.xPosition;
            }
            private set
            {
                this.xPosition = value;
            }
        }

        public int YPosition
        {
            get
            {
                return this.yPosition;
            }
            private set
            {
                this.yPosition = value;
            }
        }

        public void Move(Directions direction)
        {
            switch (Enumeration.GetEnumDescription(direction))
            {
                case "Left":
                    this.XPosition -= 1;
                    break;
                case "Up":
                    this.YPosition -= 1;
                    break;
                case "Right":
                    this.XPosition += 1;
                    break;
                case "Down":
                    this.YPosition += 1;
                    break;
                default:
                    break;
            }
        }

        public void Update(IPlayer player, IPlayfield playfield, int numberOfMoves, IScoreboard scoreboard, IGameDialog dialogs)
        {
            if (playfield.IsPlayerWinning(player))
            {
                Console.Write(dialogs.WinnerMessage(numberOfMoves));
                string name = Console.ReadLine();
                try
                {
                    scoreboard.AddTopScoreToScoreboard(name, numberOfMoves);
                }
                finally
                {

                }
                Console.WriteLine();
                LabyrinthEngine.Instance.StartNewGame();
            }
        }
    }
}
