using System;
using System.Collections.Generic;
using System.Text;

namespace Life
{
    internal class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsAlive { get; set; }

        public Cell(int x, int y, bool isAlive)
        {
            this.X = x;
            this.Y = y;
            this.IsAlive = isAlive;
        }
    }
}
