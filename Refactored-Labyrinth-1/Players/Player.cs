namespace Labyrinth.Players
{
    using System;
    using Labyrinth.Enumerations;
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
            switch (direction)
            {
                case Directions.Left:
                    this.XPosition -= 1;
                    break;
                case Directions.Up:
                    this.YPosition -= 1;
                    break;
                case Directions.Right:
                    this.XPosition += 1;
                    break;
                case Directions.Down:
                    this.YPosition += 1;
                    break;
                default:
                    break;
            }
        }
    }
}
