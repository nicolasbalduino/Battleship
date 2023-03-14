using Battleship.Gameboard;

namespace Gameboard
{
    internal class Playerboard
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        private Ship[,] Ships;
        
        public Playerboard(int rows, int columns)
        {
            this.Rows = rows;
            this.Columns = columns;
            Ships = new Ship[rows, columns];
        }

        public Ship Ship (int row, int column)
        {
            return Ships[row, column]; 
        }
    }
}
