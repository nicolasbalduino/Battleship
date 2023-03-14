using Battleship.Gameboard;

namespace Battleship.Entities
{
    internal class Submarine : Ship
    {
        public Submarine(string typeOf, int sizeOf) : base(typeOf, sizeOf)
        {            
        }

        public override string ToString()
        {
            return "S";
        }
    }
}