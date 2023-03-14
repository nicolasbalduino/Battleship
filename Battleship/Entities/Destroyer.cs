using Battleship.Gameboard;

namespace Battleship.Entities
{
    internal class Destroyer : Ship
    {
        public Destroyer(string typeOf, int sizeOf) : base(typeOf, sizeOf)
        {
            sizeOf = 3;
        }

        public override string ToString()
        {
            return "D";
        }
    }
}
