namespace Battleship.Gameboard
{
    internal class Position
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public Position(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        public override string ToString()
        {
            return $"{Row}, {Column}";
        }
    }
}
