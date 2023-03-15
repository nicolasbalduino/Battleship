using Battleship.GameBoard;

namespace Battleship.Pieces
{
    internal class Submarine : Ship
    {
        public Submarine() {
            PieceName = "Submarine";
            Size = 3;
        }

        public override string ToString()
        {
            return "S";
        }
    }
}
