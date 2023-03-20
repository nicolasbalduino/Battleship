namespace Battleship.Pieces
{
    internal class Submarine : Ship
    {
        public Submarine() {
            PieceName = "Submarine";
            Size = 2;
        }

        public override string ToString()
        {
            return "S";
        }
    }
}
