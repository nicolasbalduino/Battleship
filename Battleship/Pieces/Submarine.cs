using Battleship.GameBoard;

namespace Battleship.Pieces
{
    internal class Submarine : Piece
    {
        public Submarine() {
            Size = 3;
        }

        public override string ToString()
        {
            return "S";
        }
    }
}
