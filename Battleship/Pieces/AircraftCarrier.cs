using Battleship.GameBoard;

namespace Battleship.Pieces
{
    internal class AircraftCarrier : Piece
    {
        public AircraftCarrier()
        {
            Size = 5;
        }

        public override string ToString()
        {
            return "AC";
        }
    }
}
