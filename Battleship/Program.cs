using Battleship.GameBoard;
using Battleship.Pieces;

namespace Battleship
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // Declaração de variáveis
            Board allyBoard = new(20, 20);
            Board enemyBoard = new(20, 20);
            Piece[] allyPieces = { new Destroyer(), new Submarine(), new AircraftCarrier()};

            // Posicionamento de peças no tabuleiro
            for (int i = 0; i < allyPieces.Length; i++)
            {
                
                PlacePiece(allyPieces[i], allyBoard);
            }
            
            
        }

        public static void PlacePiece(Piece piece, Board board)
        {
            Console.Clear();
            board.PrintBoard();

            Console.WriteLine("Peça atual: {0} | Espaços de ocupação: {1}", piece, piece.Size);

            Console.Write("Informe a coluna de posicionamento: ");
            if (!char.TryParse(Console.ReadLine(), out char coordinateY))
            {
                PrintError("Coordenada inválida, tente novamente 1");
                PlacePiece(piece, board);
            }

            Console.Write("Informe a linha de posicionamento: ");
            if (!int.TryParse(Console.ReadLine(), out int coordinateX)) {

                PrintError("Coordenada inválida, tente novamente 2");
                PlacePiece(piece, board);
            }
            Position pos = new Position(coordinateX, coordinateY);
            if (!board.InsertPiece(piece, pos))
            {
                PrintError($"Coordenada inválida, tente novamente {pos.Line} {pos.Column}");
                PlacePiece(piece, board);
            }
        }

        public static void PrintError(string message)
        {
            Console.Clear();

            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(message);

            Console.ForegroundColor = aux;
            Console.WriteLine("ESPERE ");

            Thread.Sleep(2000);
            Console.Clear();
        }
    }
}
