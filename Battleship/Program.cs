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
            Piece[] enemyPieces = { new Destroyer(), new Submarine(), new AircraftCarrier()};
            bool reposicionar = false;

            // Posicionamento de peças no tabuleiro
            PrintAlert("Posicionamento de peças aliadas",'G');
            Thread.Sleep(2000);
            do
            {
                for (int i = 0; i < allyPieces.Length; i++)
                {
                    PlacePiece(allyPieces[i], allyBoard);
                }
                Console.Clear();
                allyBoard.PrintBoard();
                Console.Write("Deseja reposicionar? S para reposicionar | Qualquer outra letra para continuar: ");
                if (Console.ReadLine() == "S")
                {
                    reposicionar = true;
                    allyBoard.ClearBoard();
                }
                else reposicionar = false;
            } while (reposicionar);

            Console.Clear();

            PrintAlert("Posicionamento de peças do oponente", 'G');
            Thread.Sleep(2000);
            do
            {
                for (int i = 0; i < enemyPieces.Length; i++)
                {
                    PlacePiece(enemyPieces[i], enemyBoard);
                }
                Console.Clear();
                enemyBoard.PrintBoard();
                Console.Write("Deseja reposicionar? S para reposicionar | Qualquer outra letra para continuar: ");
                if (Console.ReadLine().ToUpper() == "S")
                {
                    reposicionar = true;
                    enemyBoard.ClearBoard();
                }
                else reposicionar = false;
            } while (reposicionar);
        }

        public static void PlacePiece(Piece piece, Board board)
        {
            Console.Clear();
            board.PrintBoard();

            Console.WriteLine("Peça atual: {0} | Espaços de ocupação: {1}", piece, piece.Size);

            Console.Write("Informe a coluna de posicionamento: ");
            if (!char.TryParse(Console.ReadLine(), out char coordinateY))
            {
                PrintError("Coordenada inválida, tente novamente");
                PlacePiece(piece, board);
            }
            Thread.Sleep(100);

            Console.Write("Informe a linha de posicionamento: ");
            if (!int.TryParse(Console.ReadLine(), out int coordinateX)) {

                PrintError("Coordenada inválida, tente novamente");
                PlacePiece(piece, board);
            }
            Thread.Sleep(100);

            Console.Write("Digite a direção de posicionamento (H - horizontal | V - vertical): ");
            if (!char.TryParse(Console.ReadLine(), out char direction))
            {
                PrintError("Direção inválida, tente novamente");
                PlacePiece(piece, board);
            }
            Thread.Sleep(100);

            if (!board.InsertPiece(piece, new Position(coordinateX, char.ToUpper(coordinateY)), char.ToUpper(direction)))
            {
                PrintError("Coordenada inválida, tente novamente");
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

            Thread.Sleep(1500);
            Console.Clear();
        }

        public static void PrintAlert(string message, char color)
        {
            ConsoleColor aux = Console.ForegroundColor;

            switch (color)
            {
                case 'R': Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case 'G':
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
            }
            Console.WriteLine(message);
            Console.ForegroundColor = aux;
        }
    }
}
