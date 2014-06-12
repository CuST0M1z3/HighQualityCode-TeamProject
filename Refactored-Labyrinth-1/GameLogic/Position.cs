using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Labyrinth.Enumerations;

namespace Labyrinth.GameLogic
{
    class Position
    {
        public int x;
        public int y;
        public Position()
        {
            this.x = 3;
            this.y = 3;
        }
        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public bool move(Direction direction)
        {
            if (isWinning()) return false;
            switch (direction)
            {
                case Direction.Left:
                    this.x -= 1;
                    break;
                case Direction.Up:
                    this.y -= 1;
                    break;
                case Direction.Right:
                    this.x += 1;
                    break;
                case Direction.Down:
                    this.y += 1;
                    break;
                default:
                    return false;
            }
            return true;
        }
        public bool isWinning()
        {
            bool resault;
            resault = false;
            if (x == 0 || x == 6 || y == 0 || y == 6)
                resault = true;
            return resault;
        }



        public bool isValidPosition()
        {



            if (x <= 6 && x >= 0 && y >= 0 && y <= 6) return true;
            else return false;
        }

        public void makeStarting()
        {
            this.x = 3;
            this.y = 3;
        }
    }
}
