namespace Battleship.Pieces
{
    internal class Destroyer : Ship
    {
        public Destroyer() {
            PieceName = "Destroyer";
            Size = 3;
        }

        public override string ToString()
        {
            return "D";
        }
    }
}
