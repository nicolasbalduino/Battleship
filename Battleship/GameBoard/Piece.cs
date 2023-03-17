using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.GameBoard
{
    internal abstract class Piece
    {
        public string PieceName { get; protected set; }
        public Piece Overlap { get; set; }

        public Piece()
        {
            PieceName = "Piece";
        }
    }
}
