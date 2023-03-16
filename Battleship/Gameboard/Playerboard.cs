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

        public Ship Ship(Position pos)
        {
            return Ships[pos.Row, pos.Column];
        }

        public bool VerifyExistentShip(Position pos)
        {
            VeifyIfPositionIsValid(pos);
            return Ship(pos) != null;
        }

        public bool VeifyIfPositionIsValid(Position pos)
        {
            if(pos.Row < 0 || pos.Row >= Rows ||
                pos.Column < 0 || pos.Column >= Columns)
            {
                return false;
            }
            return true;
        }

        public void ConfirmPosition(Position pos)
        {
            if (!VeifyIfPositionIsValid(pos))
            {
                Console.WriteLine("Invalid position\n" +
                                  "Please try another...");
            }
        }
    }
}
