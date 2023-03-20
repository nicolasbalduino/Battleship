namespace Battleship.Pieces
{
    internal class AircraftCarrier : Ship
    {
        public AircraftCarrier()
        {
            PieceName = "Aircraft Carrier";
            Size = 4;
        }

        public override string ToString()
        {
            return "C";
        }
    }
}
