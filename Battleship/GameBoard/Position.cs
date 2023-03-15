using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.GameBoard
{
    internal class Position
    {
        public int Line;
        public int Column;

        public Position (int line, char column)
        {
            Line = line - 1;
            Column = (int)char.ToUpperInvariant(column) - 'A';
        }
    }
}
