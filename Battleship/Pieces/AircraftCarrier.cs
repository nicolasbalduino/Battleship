using Battleship.GameBoard;

namespace Battleship.Pieces
{
    internal class AircraftCarrier : Piece
    {
        public AircraftCarrier()
        {
            PieceName = "Aircraft Carrier";
            Size = 5;
        }

        public override string ToString()
        {
            return "C";
        }
    }
}
