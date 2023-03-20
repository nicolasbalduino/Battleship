using Battleship.Pieces;

namespace Battleship.GameBoard
{
    internal class Board
    {
        // Constrói um tabuleiro de tipo Peca
        public Piece[,] board;
        public int Lines { get; private set; }
        public int Columns { get; private set; }
        public char[] Letters { get; private set; }
        public string[] SunkenShips { get; private set; }
        public Board(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            board = new Piece[Lines, Columns];
            SunkenShips = new string[3];
            Letters = new char[]{ 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        }

        public void PrintBoard ()
        {
            ConsoleColor aux = Console.ForegroundColor;

            // Adiciona as letras das colunas
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("   |");
            for (int i = 0; i < Columns; i++)
            {
                Console.Write(" {0} |", Letters[i]);
            }
            Console.ForegroundColor = aux;

            // Impressão do tabuleiro
            Console.Write("\n" + new string('-', 4 * (Lines + 1)));
            for (int line = 0; line < board.GetLength(0); line++)
            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\n" + (line + 1).ToString("D2"));
                Console.ForegroundColor = aux;
                Console.Write(" | ");

                for (int column = 0; column < board.GetLength(1); column++)
                {
                    // Troca cores de acordo com o tipo de peça
                    if (board[line, column] is ShipBorder) Console.ForegroundColor = ConsoleColor.DarkBlue;
                    if (board[line, column] is Ship) Console.ForegroundColor = ConsoleColor.White;

                    // Verifica se há algo na posição
                    if (board[line, column] == null) Console.Write(" ");
                    else Console.Write(board[line, column]);

                    Console.ForegroundColor = aux;
                    Console.Write(" | ");
                }

                Console.Write("\n" + new string('-', 4 * (Lines + 1)));
            }
            Console.WriteLine();
        }

        public void PrintShootBoard()
        {
            // Armazena as cores padrões de plano de fundo e principal
            ConsoleColor aux = Console.ForegroundColor;
            ConsoleColor auxB = Console.BackgroundColor;

            // Adiciona as letras das colunas
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("   |");
            for (int i = 0; i < Columns; i++)
            {
                Console.Write(" {0} |", Letters[i]);
            }
            Console.ForegroundColor = aux;

            // Impressão do tabuleiro
            Console.Write("\n" + new string('-', 4 * (Lines + 1)));
            for (int line = 0; line < board.GetLength(0); line++)
            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\n" + (line + 1).ToString("D2"));
                Console.ForegroundColor = aux;
                Console.Write(" |");

                for (int column = 0; column < board.GetLength(1); column++)
                {
                    Piece piece = board[line, column];

                    // Troca cores do console caso haja um tiro de acerto em um navio
                    if (piece is Shoot && piece.Overlap is Ship)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(" X ");
                        Console.ForegroundColor = aux;
                    }

                    // Troca cores do console caso haja um tiro sem acerto em um navio
                    if (piece is Shoot && piece.Overlap == null)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.Write("   ");
                        Console.BackgroundColor = auxB;
                    }

                    // Oculta navios
                    if (piece is not Shoot) Console.Write("   ");

                    Console.Write("|");
                }

                Console.Write("\n" + new string('-', 4 * (Lines + 1)));

            }
            Console.WriteLine();
        }

        public bool InsertPiece(Ship piece, Position pos, char direction) {

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
                }

                for (int i = 0; i < piece.Size; i++)
                {
                    if (pos.Column + i >= board.GetLength(1))
                    {
                        board[pos.Line, pos.Column - piece.Size + i] = piece;
                        PlaceBorder(pos.Line, pos.Column - piece.Size + i);
                    }
                    else
                    {
                        board[pos.Line, pos.Column + i] = piece;
                        PlaceBorder(pos.Line, pos.Column + i);
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
            }

            // Insere o navio em cada posição da direção escolhida
            for (int i = 0; i < piece.Size; i++)
            {
                if (pos.Line + i >= board.GetLength(0))
                {
                    board[pos.Line - piece.Size + i, pos.Column] = piece;
                    PlaceBorder(pos.Line - piece.Size + i, pos.Column);
                }
                else
                {
                    board[pos.Line + i, pos.Column] = piece;
                    PlaceBorder(pos.Line + i, pos.Column);
                }
            }

            return true;
        }

        // Insere bordas ao redor do navio
        public void PlaceBorder(int line, int column)
        {
            ShipBorder shipBorder = new ShipBorder();

            // Verifica e coloca em posições acima da peça
            if (line > 0)
            {
                if (column > 0 && board[line - 1, column - 1] is not Ship) 
                    board[line - 1, column - 1] = shipBorder;

                if (board[line - 1, column] is not Ship) 
                    board[line - 1, column] = shipBorder;

                if (column < board.GetLength(1) - 1 && board[line - 1, column + 1] is not Ship) 
                    board[line - 1, column + 1] = shipBorder;
            }

            // Verifica e coloca em posições à esquerda e direita da peça
            if (column > 0 && board[line, column - 1] is not Ship)
                board[line, column - 1] = shipBorder;

            if (column < board.GetLength(1) - 1 && board[line, column + 1] is not Ship) 
                board[line, column + 1] = shipBorder;

            // Verifica e coloca em posições abaixo da peça
            if (line < board.GetLength(1) - 1)
            {
                if (column > 0 && board[line + 1, column - 1] is not Ship) 
                    board[line + 1, column - 1] = shipBorder;

                if (board[line + 1, column] is not Ship) 
                    board[line + 1, column] = shipBorder;

                if (column < board.GetLength(1) - 1 && board[line + 1, column + 1] is not Ship) 
                    board[line + 1, column + 1] = shipBorder;
            }
        }

        public bool InsertShoot(Piece piece, Position pos)
        {
            // Verificação de posições inválidas
            if (pos.Line < 0 || pos.Line >= board.GetLength(0)) return false;
            if (pos.Column < 0 || pos.Column >= board.GetLength(1)) return false;

            // Verifica se já não há algum tiro na posição
            if (board[pos.Line, pos.Column] is Shoot) return false;

            // Armazena peça sobreposta, caso exista
            if (board[pos.Line, pos.Column] is not ShipBorder) piece.Overlap = board[pos.Line, pos.Column];

            // Diminui quantidade de peças restante do navio, caso exista
            if (piece.Overlap != null)
            {
                Ship shipShooted = ((Ship)piece.Overlap);
                shipShooted.Size--;
                if (shipShooted.Size == 0) ShipSunked(shipShooted);
            }

            // Insere a peça tiro
            board[pos.Line, pos.Column] = piece;
            return true;
        }

        // Verifica se a posição de tiro é valida
        public bool VerifyShootPosition(Position pos)
        {
            if (pos.Line < 0 || pos.Line >= board.GetLength(0)) return false;
            if (pos.Column < 0 || pos.Column >= board.GetLength(1)) return false;

            if (board[pos.Line, pos.Column] is Shoot && board[pos.Line, pos.Column].Overlap == null) return false;

            return true;
        }

        // Verifica se na posição já contém tiros
        public bool ContainsShoot(Position pos)
        {
            if (pos.Line < 0 || pos.Line >= board.GetLength(0)) return true;
            if (pos.Column < 0 || pos.Column >= board.GetLength(1)) return true;

            if (board[pos.Line, pos.Column] is Shoot) return true;
            return false;
        }

        // Armazena os navios afundados
        public void ShipSunked (Piece ShipSunk)
        {
            for (int i = 0; i < SunkenShips.Length; i++)
            {
                if (SunkenShips[i] == null)
                {
                    SunkenShips[i] = ShipSunk.PieceName;
                    return;
                }
            }
        }

        // Reseta o tabuleiro
        public void ClearBoard()
        {
            board = new Piece[Lines, Columns];
        }
    }
}
