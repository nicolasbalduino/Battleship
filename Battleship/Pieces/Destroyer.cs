﻿using Battleship.GameBoard;

namespace Battleship.Pieces
{
    internal class Destroyer : Ship
    {
        public Destroyer() {
            PieceName = "Destroyer";
            Size = 2;
        }

        public override string ToString()
        {
            return "D";
        }
    }
}
