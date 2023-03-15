using Battleship.GameBoard;

namespace Battleship.Pieces
{
    internal class Shoot : Piece
    {
        public Shoot()
        {
            Size = 1;
        }

        public override string ToString()
        {
            return "X";
        }
    }
}
