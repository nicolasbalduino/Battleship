using Battleship.Gameboard;

namespace Battleship.Entities
{
    internal class Carrier : Ship
    {
        public Carrier(string typeOf, int sizeOf) : base(typeOf, sizeOf)
        {
            sizeOf = 4;
        }

        public override string ToString()
        {
            return "C";
        }
    }
}
