namespace Battleship.GameBoard
{
    internal class Board
    {
        public Piece[,] board;

        public Board(int lines, int columns)
        {
            board = new Piece[lines, columns];
        }

        public void PrintBoard ()
        {
            Console.Write("   | A | B | C | D | E | F | G | H | I | J | K | L | M | N | O | P | Q | R | S | T |");
            Console.Write("\n------------------------------------------------------------------------------------");
            for (int line = 0; line < board.GetLength(0); line++)
            {
                Console.Write("\n" + (line + 1).ToString("D2") + " | ");
                for (int column = 0; column < board.GetLength(1); column++)
                {
                    if (board[line, column] == null)
                        Console.Write("  | ");
                    else
                        Console.Write(board[line, column] + " | ");
                }

                Console.Write("\n------------------------------------------------------------------------------------");
            }
            Console.WriteLine();
        }

        public bool InsertPiece(Piece piece, Position pos) {
            if (pos.Line < 0 || pos.Line > board.GetLength(0)) return false;
            if (pos.Column < 0 || pos.Column > board.GetLength(1)) return false;

            if (board[pos.Line, pos.Column] == null) board[pos.Line, pos.Column] = piece;
            else return false;
            return true;
        }
    }
}
