using Battleship.Gameboard;

namespace Battleship.Entities
{
    internal class Submarine : Ship
    {        
        public Submarine()
        {
            SizeOf = 2;
        }

        public override string ToString()
        {
            return "S";
        }
    }
}