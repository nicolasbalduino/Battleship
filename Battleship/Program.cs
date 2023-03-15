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

            // Loop até usuário confirmar posicionamentos
            do
            {
                // Loop até todas as peças forem posicionadas
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

            // Loop até usuário confirmar posicionamentos
            do
            {
                // Loop até todas as peças forem posicionadas
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

            // Início do jogo
            do
            {
                // Envia um novo tiro para o tabuleiro do inimigo
                PlacePiece(new Shoot(), enemyBoard);
            } while (true);
        }

        public static void PlacePiece(Piece piece, Board board)
        {
            Console.Clear();

            // Verifica se é uma Peca do tipo Shoot
            if ((piece is Shoot))
            {
                board.PrintShootBoard();
                Console.WriteLine("Posicione seu tiro");
            }

            // Caso não for do tipo Shoot, imprime o tabuleiro sem nada oculto
            else {
                board.PrintBoard();
                Console.WriteLine("Peça atual: {0} | Espaços de ocupação: {1}", piece.PieceName, piece.Size);
            }

            Console.Write("Informe a coluna de posicionamento: ");
            // Tenta converter coordenada da coluna para caracter
            string columnString = Console.ReadLine();
            if (!char.TryParse(columnString, out char coordinateY))
            {
                PrintError("Coordenada inválida, tente novamente");
                PlacePiece(piece, board);
            }
            Thread.Sleep(100);

            Console.Write("Informe a linha de posicionamento: ");
            // Tenta converter coordenada da linha para inteiro
            string lineString = Console.ReadLine();
            if (!int.TryParse(lineString, out int coordinateX)) {

                PrintError("Coordenada inválida, tente novamente");
                PlacePiece(piece, board);
            }
            Thread.Sleep(100);

            // Cria uma nova posição, com X e Y convertidos corretamente
            Position pos = new Position(coordinateX, char.ToUpper(coordinateY));

            // Verifica se a peça NÃO é do tipo Shoot
            if (!(piece is Shoot))
            {
                Console.Write("Digite a direção de posicionamento (H - horizontal | V - vertical): ");
                // Tenta converter direção para caracter
                string directionString = Console.ReadLine();
                if (!char.TryParse(directionString, out char direction))
                {
                    PrintError("Direção inválida, tente novamente");
                    PlacePiece(piece, board);
                }
                Thread.Sleep(100);

                if (!board.InsertPiece(piece, pos, char.ToUpper(direction)))
                {
                    PrintError("Coordenada inválida, tente novamente");
                    PlacePiece(piece, board);
                }
                return;
            }

            // Caso for do tipo Shoot, a função chegará até aqui, inserindo o tiro na posição
            if (!board.InsertShoot(piece, pos))
            {
                PrintError("Coordenada inválida, tente novamente");
                PlacePiece(piece, board);
            }
        }

        // Exibir erros com cor e pausa
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

        // Exibir avisos com pausa e cor
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
