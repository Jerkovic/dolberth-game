using System;

namespace Dolberth.Maze.Models
{
    public class Cell
    {
        public int x { get; set; }

        public int z { get; set; }

        public CellType cellType { get; set; }

        public Cell from { get; set; }

        public Cell(int x, int z)
        {
            this.x = x;
            this.z = z;
            this.cellType = CellType.WALL;
        }

        public override string ToString()
        {
            return "Cell: " + x + ", " + z + " " + cellType;
        }
    }
}
