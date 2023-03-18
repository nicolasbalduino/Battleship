using Battleship.GameBoard;
using Battleship.Pieces;
using System.Threading.Channels;

namespace Battleship
{
    internal class Program
    {
        public static void Main(string[] args)
        {

            GamePresents();
            // Declaração de variáveis
            Board allyBoard = new(20, 20);
            Board enemyBoard = new(20, 20);
            Ship[] allyPieces = { new Destroyer(), new Submarine(), new AircraftCarrier() };
            Ship[] enemyPieces = { new Destroyer(), new Submarine(), new AircraftCarrier() };
            bool reposicionar;

            Console.WriteLine("BATALHA NAVAL");
            // Escolha de nome de jogadores
            Console.Write("Jogador 1, Informe seu nome: ");
            string player1 = Console.ReadLine();

            Console.Write("Jogador 2, Informe seu nome: ");
            string player2 = Console.ReadLine();

            Console.Clear();

            // Posicionamento de peças no tabuleiro
            PrintAlert("Posicionamento de peças aliadas", 'G');
            Thread.Sleep(2000);

            // Loop até usuário confirmar posicionamentos
            do
            {
                // Loop até todas as peças forem posicionadas
                for (int i = 0; i < allyPieces.Length; i++)
                {
                    PlaceShip(allyPieces[i], allyBoard, player1);
                }

                Console.Clear();
                allyBoard.PrintBoard();

                Console.Write("Deseja reposicionar? S para reposicionar | Qualquer outra letra para continuar: ");
                string opc = Console.ReadLine();
                if (opc == "S" || opc == "s")
                {
                    reposicionar = true;
                    allyBoard.ClearBoard();
                }
                else reposicionar = false;
            } while (reposicionar);

            Console.Clear();

            PrintAlert("Posicionamento de peças do oponente", 'R');
            Thread.Sleep(2000);

            // Loop até usuário confirmar posicionamentos
            do
            {
                // Loop até todas as peças forem posicionadas
                for (int i = 0; i < enemyPieces.Length; i++)
                {
                    PlaceShip(enemyPieces[i], enemyBoard, player2);
                }

                Console.Clear();
                enemyBoard.PrintBoard();
                Console.Write("Deseja reposicionar? S para reposicionar | Qualquer outra letra para continuar: ");

                string opc = Console.ReadLine();
                if (opc == "S" || opc == "s")
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
                    hit = PlaceShoot(new Shoot(), enemyBoard, player1);

                    if (hit)
                    {
                        Console.Clear();
                        enemyBoard.PrintShootBoard();
                        PrintAlert("Acertou miserável! Aperte qualquer tecla para continuar", 'R');
                        Console.ReadKey();
                        allyShoot++;
                    }
                    else PrintError("Errou! Aperte qualquer tecla para trocar de jogador...");
                } while (hit && allyShoot < 9);

                // Verifica se todas as peças foram acertadas
                if (allyShoot == 9)
                {
                    Console.Clear();
                    PrintAlert($"{player1} Venceu!", 'R');
                    break;
                }

                // Jogador 2
                do
                {
                    hit = PlaceShoot(new Shoot(), allyBoard, player2);

                    if (hit)
                    {
                        Console.Clear();
                        allyBoard.PrintShootBoard();
                        PrintAlert("Acertou miserável! aperte qualquer tecla para continuar", 'R');
                        Console.ReadKey();
                        enemyShoot++;
                    }
                    else
                    {
                        PrintError("Errou! Aperte qualquer tecla para trocar de jogador...");
                        Console.ReadKey();
                    }
                } while (hit && enemyShoot < 9);

                // Verifica se todas as peças foram acertadas
                if (enemyShoot == 9)
                {
                    Console.Clear();
                    PrintAlert($"{player2} Venceu!", 'R');
                    break;
                }
            } while (true);
        }

        public static Position Coordenates()
        {
            Console.Write("\nDigite uma coordenada no formato Coluna Linha (Ex: A1): ");
            string coordinate = Console.ReadLine().ToUpper();

            if (coordinate.Length < 2 || coordinate.Length > 3)
            {
                PrintError("Coordenada inválida, aperte qualquer tecla para tentar novamente");
                return null;
            }

            char coordinateY = coordinate[0];

            bool isNumber = int.TryParse(coordinate.Replace(coordinateY, ' '), out int coordinateX);

            if (!char.IsLetter(coordinateY) || !isNumber)
            {
                PrintError("Coordenada inválida, aperte qualquer tecla para tentar novamente");
                return null;
            }

            if ((coordinateY - 'A' > 19) || (coordinateX < 1 || coordinateX > 20))
            {
                PrintError("Coordenada inválida, aperte qualquer tecla para tentar novamente");
                return null;
            }

            Position pos = new Position(coordinateX, coordinateY);
            return pos;
        }

        // Posiciona o navio no tabuleiro
        public static void PlaceShip(Ship piece, Board board, string playerName)
        {
            Position pos;

            do
            {
                Console.Clear();
                board.PrintBoard();
                Console.WriteLine("{0} | Peça atual: {1} | Espaços de ocupação: {2}", playerName, piece.PieceName, piece.Size);
                pos = Coordenates();
            } while (pos == null);

            Console.Write("Digite a direção de posicionamento (H - horizontal | V - vertical): ");
            // Tenta converter direção para caractere
            string directionString = Console.ReadLine();
            if (!char.TryParse(directionString, out char direction))
            {
                PrintError("Direção inválida, aperte qualquer tecla para tentar novamente");
                PlaceShip(piece, board, playerName);
            }
            Thread.Sleep(100);

            if (!board.InsertPiece(piece, pos, char.ToUpper(direction)))
            {
                PrintError("Coordenada inválida, aperte qualquer tecla para tentar novamente");
                PlaceShip(piece, board, playerName);
            }
        }

        // Posiciona o tiro no tabuleiro
        public static bool PlaceShoot(Piece piece, Board board, string playerName)
        {
            Position pos;
            do
            {
                Console.Clear();
                board.PrintShootBoard();

                // Imprime os navios afundados:
                Console.Write("Navios afundados: ");
                foreach (string ship in board.SunkenShips)
                {
                    Console.Write("{0} ", ship);
                }
                Console.WriteLine();

                Console.WriteLine("{0} | Insira as coordenadas do tiro", playerName);
                pos = Coordenates();
            } while (pos == null);

            if (!board.InsertShoot(piece, pos))
            {
                PrintError("Coordenada inválida, tente novamente");
                PlaceShoot(piece, board, playerName);
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

            Console.ReadKey();
            Console.Clear();
        }

        // Exibir avisos com pausa e cor
        public static void PrintAlert(string message, char color)
        {
            ConsoleColor aux = Console.ForegroundColor;

            switch (color)
            {
                case 'R':
                    Console.ForegroundColor = ConsoleColor.Red;
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

        public static void GamePresents()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("_____________________________________________________________________________________________");
            Console.WriteLine("|                                                                                            |");
            Console.WriteLine("|    BBBBB     AAA   TTTTTTTT  TTTTTTTT  LL     EEEEEEE  SSSSSS  HH   HH  IIII  PPPPPP       |");
            Console.WriteLine("|    BB   B   AAAAA     TT        TT     LL     EE      SS       HH   HH   II   PP   PP      |");
            Console.WriteLine("|    BBBBB   AA   AA    TT        TT     LL     EEEEE    SSSSS   HHHHHHH   II   PPPPPP       |");
            Console.WriteLine("|    BB   B AAAAAAAAA   TT        TT     LL     EE           SS  HH   HH   II   PP           |");
            Console.WriteLine("|    BBBBB   AA   AA    TT        TT     LLLLLL EEEEEEE  SSSSSS  HH   HH  IIII  PP           |");
            Console.WriteLine("|____________________________________________________________________________________________|");
            Console.WriteLine("|                                                                                            |");
            Console.WriteLine("|                                                                                            |");
            Console.WriteLine("|                                                                                            |");
            Console.WriteLine("|                                                                                            |");
            Console.WriteLine("|                                                                                            |");
            Console.WriteLine("|                                                                                            |");
            Console.WriteLine("|                                                                                            |");
            Console.WriteLine("|                                                                                            |");
            Console.WriteLine("|             ___|00000|===__                                     __===|00000|___            |");
            Console.WriteLine("|            \\__º__º__º__º__/                                    \\__º__º__º__º__/            |");
            Console.WriteLine("| ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~  |");
            Console.WriteLine("|                                                                                            |");
            Console.WriteLine("|                        ____|0|__                             __|0|____                     |");
            Console.WriteLine("|                     x=(_________)                           (_________)=x                  |");
            Console.WriteLine("|                                                                                            |");
            Console.WriteLine("|____________________________________________________________________________________________|");
            Console.WriteLine();
            Console.ForegroundColor= ConsoleColor.Red;
            Console.WriteLine("Aperte Enter para jogar: ");
            Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Green;
        }
    }
}
