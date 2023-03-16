using Battleship.Gameboard;

namespace Battleship.Entities
{
    internal class Destroyer : Ship
    {
        public Destroyer()
        {
            SizeOf = 2;
        }

        public override string ToString()
        {
            return "D";
        }
    }
}
