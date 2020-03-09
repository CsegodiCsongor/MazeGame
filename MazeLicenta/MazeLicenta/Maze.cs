using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLicenta
{
    public class Maze
    {
        public MyPoint StartingPoint { get; set; }
        public MyPoint EndingPoint { get; set; }

        public Tile[,] maze { get; set; }
    }
}
