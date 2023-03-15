using Battleship.GameBoard;

namespace Battleship.Pieces
{
    internal class Destroyer : Piece
    {
        public Destroyer() {
            Size = 2;
        }

        public override string ToString()
        {
            return "D";
        }
    }
}
