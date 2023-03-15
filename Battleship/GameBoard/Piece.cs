using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.GameBoard
{
    internal class Piece
    {
        public string PieceName { get; protected set; }
        public int[] OriginalPosition { get; set; }

        public Piece()
        {
            OriginalPosition = new int[2];
            PieceName = "Piece";
        }
        public int Size { get; set; }
    }
}
