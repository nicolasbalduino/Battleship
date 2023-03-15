using Battleship.Pieces;
using Microsoft.Win32.SafeHandles;

namespace Battleship.GameBoard
{
    internal class Board
    {
        // Constrói um tabuleiro de tipo Peca
        public Piece[,] board;
        public int Lines { get; private set; }
        public int Columns { get; private set; }
        public Board(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            board = new Piece[Lines, Columns];
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
                    // Verifica se não há nada na posição
                    if (board[line, column] == null)
                        Console.Write(" ");
                    else
                        Console.Write(board[line, column]);
                    Console.Write(" | ");
                }

                Console.Write("\n------------------------------------------------------------------------------------");
            }
            Console.WriteLine();
        }

        public void PrintShootBoard()
        {
            // Armazena as cores padrões de plano de fundo e principal
            ConsoleColor auxF = Console.ForegroundColor;
            ConsoleColor auxB = Console.BackgroundColor;

            Console.Write("   | A | B | C | D | E | F | G | H | I | J | K | L | M | N | O | P | Q | R | S | T |");
            Console.Write("\n------------------------------------------------------------------------------------");
            for (int line = 0; line < board.GetLength(0); line++)
            {
                Console.Write("\n" + (line + 1).ToString("D2") + " | ");
                for (int column = 0; column < board.GetLength(1); column++)
                {
                    // Oculta qualquer peça que não for tiro
                    if (!(board[line, column] is Shoot))
                        Console.Write(" ");

                    else
                    {
                        // Adiciona cores de acordo com o acerto ou erro do tiro
                        if (board[line, column] != null) Console.ForegroundColor = ConsoleColor.Red;
                        else Console.BackgroundColor = ConsoleColor.Blue;
                        Console.Write("X");

                        // Retorna às cores padrões do console
                        Console.ForegroundColor = auxF;
                        Console.BackgroundColor = auxB;
                    }
                    Console.Write(" | ");
                }

                Console.Write("\n------------------------------------------------------------------------------------");
            }
            Console.WriteLine();
        }

        public bool InsertPiece(Piece piece, Position pos, char direction) {

            // Verificação de posições inválidas
            if (pos.Line < 0 || pos.Line >= board.GetLength(0)) return false;
            if (pos.Column < 0 || pos.Column >= board.GetLength(1)) return false;
            if (direction != 'H' && direction != 'V') return false;

            // Verificação e posicionamento na horizontal
            if (direction == 'H')
            {
                for (int i = 0; i < piece.Size; i++)
                {
                    if (pos.Column + i >= board.GetLength(1))
                    {
                        if (board[pos.Line, pos.Column - piece.Size + i] != null) return false;
                    }
                    else
                    {
                        if (board[pos.Line, pos.Column + i] != null) return false;
                    }

                    //if (pos.Column + i >= board.GetLength(1)) return false;
                    //if (board[pos.Line, pos.Column + i] != null) return false;
                }

                for (int i = 0; i < piece.Size; i++)
                {
                    if (pos.Column + i >= board.GetLength(1))
                    {
                        board[pos.Line, pos.Column - piece.Size + i] = piece;
                    }
                    else
                    {
                        board[pos.Line, pos.Column + i] = piece;
                    }
                        
                }
                return true;
            }

            // Verificação e posicionamento na vertical
            for (int i = 0; i < piece.Size; i++)
            {
                if (pos.Line + i >= board.GetLength(0))
                {
                    if (board[pos.Line - piece.Size + i, pos.Column] != null) return false;
                }
                else
                {
                    if (board[pos.Line + i, pos.Column] != null) return false;
                }
                //if (pos.Line + i >= board.GetLength(0)) return false;
                //if (board[pos.Line + i, pos.Column] != null) return false;
            }

            // Insere o navio em cada posição da direção escolhida
            for (int i = 0; i < piece.Size; i++)
            {
                if (pos.Line + i >= board.GetLength(0))
                {
                    board[pos.Line - piece.Size + i, pos.Column] = piece;
                }
                else
                {
                    board[pos.Line + i, pos.Column] = piece;
                }
                
            }

            return true;
        }

        public bool InsertShoot(Piece piece, Position pos)
        {
            // Verificação de posições inválidas
            if (pos.Line < 0 || pos.Line >= board.GetLength(0)) return false;
            if (pos.Column < 0 || pos.Column >= board.GetLength(1)) return false;

            // Verifica se já não há algum tiro na posição
            if (board[pos.Line, pos.Column] is Shoot) return false;

            // Insere a peça tiro
            board[pos.Line, pos.Column] = piece;
            return true;
        }

        public void ClearBoard()
        {
            board = new Piece[Lines, Columns];
        }
    }
}
