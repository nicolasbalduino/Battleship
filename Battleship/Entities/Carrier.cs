using Battleship.Gameboard;

namespace Battleship.Entities
{
    internal class Carrier : Ship
    {
        public Carrier()
        {
            SizeOf = 4;
        }

        public override string ToString()
        {
            return "C";
        }
    }
}
