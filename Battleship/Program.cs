using System.Diagnostics;
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
            Ship[] allyPieces = { new Destroyer(), new Submarine(), new AircraftCarrier()};
            Ship[] enemyPieces = { new Destroyer(), new Submarine(), new AircraftCarrier()};
            bool reposicionar;

            // Posicionamento de peças no tabuleiro
            PrintAlert("Posicionamento de peças aliadas",'G');
            Thread.Sleep(2000);

            // Loop até usuário confirmar posicionamentos
            do
            {
                // Loop até todas as peças forem posicionadas
                for (int i = 0; i < allyPieces.Length; i++)
                {
                    PlaceShip(allyPieces[i], allyBoard);
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
                    PlaceShip(enemyPieces[i], enemyBoard);
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
            int allyShoot = 0;
            int enemyShoot = 0;
            do
            {
                bool hit = false;

                // Jogador 1
                do
                {
                    hit = PlaceShoot(new Shoot(), enemyBoard, 1);

                    if (hit)
                    {
                        PrintAlert("Acertou miserável!", 'R');
                        allyShoot++;
                    }
                    else PrintError("Errou! Trocando de jogador...");
                    Thread.Sleep(1000);
                } while (hit && allyShoot < 9);

                if (allyShoot == 9)
                {
                    Console.Clear();
                    PrintAlert("Vitória do jogador 1", 'R');
                    break;
                }

                // Jogador 2
                do
                {
                    hit = PlaceShoot(new Shoot(), allyBoard, 2);

                    if (hit)
                    {
                        PrintAlert("Acertou miserável!", 'R');
                        enemyShoot++;
                    }
                    else PrintError("Errou! Trocando de jogador...");
                    Thread.Sleep(1000);
                } while (hit && enemyShoot < 9);

                if (enemyShoot == 9)
                {
                    Console.Clear();
                    PrintAlert("vitória do jogador 2", 'R');
                    break;
                }
            } while (true);

            Credits();
        }

        public static Position Coordenates()
        {
            Console.Write("Informe a coluna de posicionamento: ");
            // Tenta converter coordenada da coluna para caractere
            string columnString = Console.ReadLine();
            if (!char.TryParse(columnString, out char coordinateY))
            {
                PrintError("Coordenada inválida, tente novamente");
                return null;
            }
            Thread.Sleep(100);

            Console.Write("Informe a linha de posicionamento: ");
            // Tenta converter coordenada da linha para inteiro
            string lineString = Console.ReadLine();
            if (!int.TryParse(lineString, out int coordinateX))
            {
                PrintError("Coordenada inválida, tente novamente");
                return null;
            }
            Thread.Sleep(100);

            // Cria uma nova posição, com X e Y convertidos corretamente
            Position pos = new Position(coordinateX, char.ToUpper(coordinateY));
            return pos;
        }

        // Posiciona o navio no tabuleiro
        public static void PlaceShip (Ship piece, Board board)
        {
            Position pos;

            do
            {
                Console.Clear();
                board.PrintBoard();

                Console.WriteLine("Peça atual: {0} | Espaços de ocupação: {1}", piece.PieceName, piece.Size);
                pos = Coordenates();
            } while (pos == null);

            Console.Write("Digite a direção de posicionamento (H - horizontal | V - vertical): ");
            // Tenta converter direção para caractere
            string directionString = Console.ReadLine();
            if (!char.TryParse(directionString, out char direction))
            {
                PrintError("Direção inválida, tente novamente");
                PlaceShip(piece, board);
            }
            Thread.Sleep(100);

            if (!board.InsertPiece(piece, pos, char.ToUpper(direction)))
            {
                PrintError("Coordenada inválida, tente novamente");
                PlaceShip(piece, board);
            }
        } 

        // Posiciona o tiro no tabuleiro
        public static bool PlaceShoot(Piece piece, Board board, int player)
        {
            Position pos;
            do
            {
                Console.Clear();
                board.PrintShootBoard();

                Console.WriteLine("JOGADOR {0}, Insira as coordenadas do tiro", player);
                pos = Coordenates();
            } while (pos == null);

            if (!board.InsertShoot(piece, pos))
            {
                PrintError("Coordenada inválida, tente novamente");
                PlaceShoot(piece, board, player);
            }

            if (piece.Overlap != null) return true;
            return false;
        }

        // Exibir erros com pausa e cor
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

        public static void Credits()
        {
            // Frescures
            Console.CursorVisible = false;
            string[] credits = {
            "Desenvolvido por: Seu nome aqui",
            "Agradecimentos especiais: Fulano, Beltrano",
            "Copyright © 2023"
        };
            int y = Console.WindowHeight;
            int x = Console.WindowWidth / 2;
            while (y >= 0)
            {
                Console.Clear();
                for (int i = 0; i < credits.Length; i++)
                {
                    Console.SetCursorPosition(x - credits[i].Length / 2, y + i);
                    Console.Write(credits[i]);
                }
                Thread.Sleep(50);
                y--;
            }
            Console.Clear();
        }
    }
}
