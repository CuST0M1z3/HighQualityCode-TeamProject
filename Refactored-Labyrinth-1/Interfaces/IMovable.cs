using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Labyrinth.Enumerations;
using Labyrinth.ConsoleUI;

namespace Labyrinth.Interfaces
{
    public interface IMovable
    {
        void Move(Directions direction);
    }
}
