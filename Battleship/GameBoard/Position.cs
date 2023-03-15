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
            // Sincroniza linhas com o index do vetor
            Line = line - 1;

            // Transforma a letra em número, o sincronizando com o index do vetor
            Column = (int)char.ToUpperInvariant(column) - 'A';
        }
    }
}
