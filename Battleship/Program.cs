using Battleship.GameBoard;
using Battleship.Pieces;

namespace Battleship
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // Introdução do jogo
            GamePresents();

            // Declaração de variáveis
            Board allyBoard = new(20, 20);
            Board enemyBoard = new(20, 20);
            Ship[] allyPieces = { new Submarine(), new Destroyer(), new AircraftCarrier() };
            Ship[] enemyPieces = { new Submarine(), new Destroyer(), new AircraftCarrier() };
            Machine machine = new(enemyBoard, allyBoard);
            bool reposicionar;

            // Escolha de modo de jogo
            int gameMode = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("1 - Modo 2 jogadores");
                Console.WriteLine("2 - Modo Contra a máquina");
                Console.Write("Escolha o modo de jogo: ");
                int.TryParse(Console.ReadLine(), out gameMode);
            } while (gameMode != 1 && gameMode != 2);

            // Escolha de nome de jogadores
            string player1, player2;

            Console.Write("Jogador 1, Informe seu nome: ");
            player1 = Console.ReadLine();

            if (player1.Trim() == "") player1 = "Jogador 1";

            if (gameMode == 1)
            {
                Console.Write("Jogador 2, Informe seu nome: ");
                player2 = Console.ReadLine();
                if (player2.Trim() == "") player2 = "Jogador 2";
            }
            else player2 = "Maquina";

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
                string opc = Console.ReadLine().Trim();
                if (opc == "S" || opc == "s")
                {
                    reposicionar = true;
                    allyBoard.ClearBoard();
                }
                else reposicionar = false;
            } while (reposicionar);

            Console.Clear();

            if (gameMode == 1)
            {
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

                    string opc = Console.ReadLine().Trim();
                    if (opc == "S" || opc == "s")
                    {
                        reposicionar = true;
                        enemyBoard.ClearBoard();
                    }
                    else reposicionar = false;
                } while (reposicionar);
            }

            else
            {
                PrintAlert("Posicionamento das peças da máquina", 'R');
                Thread.Sleep(2000);

                // Loop até todas as peças forem posicionadas
                for (int i = 0; i < enemyPieces.Length; i++)
                {
                    machine.PlaceShip(enemyPieces[i]);
                }
            }

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
                    if (hit) allyShoot++;
                } while (hit && allyShoot < 9);

                // Verifica se todas as peças foram acertadas
                if (allyShoot == 9)
                {
                    Console.Clear();
                    PrintAlert($"{player1} Venceu!", 'R');
                    Console.WriteLine("\nPressione uma tecla para finalizar...");
                    Console.ReadKey();
                    break;
                }

                // Jogador 2
                do
                {
                    if (gameMode == 1) hit = PlaceShoot(new Shoot(), allyBoard, player2);
                    else
                    {
                        Position machineShoot;
                        hit = machine.PlaceShoot(out machineShoot);
                        Console.Clear();
                        MachineShootAlert(allyBoard, machineShoot, hit);
                    }

                    if (hit) enemyShoot++;
                } while (hit && enemyShoot < 9);

                // Verifica se todas as peças foram acertadas
                if (enemyShoot == 9)
                {
                    Console.Clear();
                    PrintAlert($"{player2} Venceu!", 'R');
                    Console.WriteLine("\nPressione uma tecla para finalizar...");
                    Console.ReadKey();
                    break;
                }
            } while (true);
            Credits();
        }

        public static Position Coordinates()
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

            // Cria uma nova posição, com X e Y convertidos corretamente
            Position pos = new Position(coordinateX - 1, ((int)char.ToUpperInvariant(coordinateY) - 'A'));
            return pos;
        }

        // Posiciona o navio no tabuleiro
        public static void PlaceShip(Ship piece, Board board, string playerName)
        {
            Position pos;
            bool valid = false;

            do
            {
                Console.Clear();
                board.PrintBoard();
                Console.WriteLine("{0} | Peça atual: {1} | Tamanho da embarcação: {2}", playerName, piece.PieceName, piece.Size);
                pos = Coordinates();
                if (pos != null)
                {
                    Console.Write("Digite a direção de posicionamento (H - horizontal | V - vertical): ");
                    // Tenta converter direção para caractere
                    string directionString = Console.ReadLine();
                    if (char.TryParse(directionString, out char direction))
                    {
                        valid = board.InsertPiece(piece, pos, char.ToUpper(direction));
                        if (!valid)
                        {
                            PrintError("Coordenada inválida, aperte qualquer tecla para tentar novamente");
                        }
                    }
                    else
                    {
                        PrintError("Direção inválida, aperte qualquer tecla para tentar novamente");
                    }
                }
            } while (!valid);
        }

        // Posiciona o tiro no tabuleiro
        public static bool PlaceShoot(Piece piece, Board board, string playerName)
        {
            Position pos;
            bool valid = false;
            do
            {

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
                    pos = Coordinates();
                } while (pos == null);

                valid = board.InsertShoot(piece, pos);
                if (!valid) PrintError("Coordenada inválida, tente novamente");
            } while (!valid);


            bool hit = piece.Overlap != null;

            PlayerShootAlert(board, pos, hit, playerName);

            return hit;
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

        public static void GamePresents()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("_______________________________________________________________________________________________________");
            Console.WriteLine("|                                                                                                     |");
            Console.WriteLine("|    BBBBB       AAA      TTTTTTTT  TTTTTTTT  LL      EEEEEEE  SSSSSSS  HH   HH  IIIIII  PPPPPPP      |");
            Console.WriteLine("|    BB  BB     AAAAA        TT        TT     LL      EE       SS       HH   HH    II    PP   PP      |");
            Console.WriteLine("|    BBBBB     AA   AA       TT        TT     LL      EEEEEEE  SSSSSSS  HHHHHHH    II    PPPPPPP      |");
            Console.WriteLine("|    BB  BB   AAAAAAAAA      TT        TT     LL      EE            SS  HH   HH    II    PP           |");
            Console.WriteLine("|    BBBBB   AAA     AAA     TT        TT     LLLLLL  EEEEEEE  SSSSSSS  HH   HH  IIIIII  PP           |");
            Console.WriteLine("|_____________________________________________________________________________________________________|");
            Console.WriteLine("|                                                                                                     |");
            Console.WriteLine("|                                                                                                     |");
            Console.WriteLine("|                                                                                                     |");
            Console.WriteLine("|                                                                                                     |");
            Console.WriteLine("|                                                                                                     |");
            Console.WriteLine("|                                                                                                     |");
            Console.WriteLine("|                                                                                                     |");
            Console.WriteLine("|                                                                                                     |");
            Console.WriteLine("|            ____|00000|===___                                   ____===|00000|___                    |");
            Console.WriteLine("|            \\__º__º__º__º__//                                   \\__º__º__º__º__//                    |");
            Console.WriteLine("|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|");
            Console.WriteLine("|                                                                                                     |");
            Console.WriteLine("|                        ____|0|__                             __|0|____                              |");
            Console.WriteLine("|                     x=(_________)                           (_________)=x                           |");
            Console.WriteLine("|                                                                                                     |");
            Console.WriteLine("|_____________________________________________________________________________________________________|");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Aperte Enter para jogar: ");
            Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Green;
        }

        public static void Credits()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.CursorVisible = false;
            string[] credits = {
                "Obrigado por jogar!",
                " ",
                "Desenvolvedores:",
                " ",
                "Daniel Visicatto",
                "Luis Guilherme Silva",
                "Nicolas Antonio Balduino",
                "Vinicius Picolo",
                " ",
                "Battleship IterAção 5by5 © 2023"
            };
            int y = Console.WindowHeight - credits.Length;
            int x = Console.WindowWidth / 2;
            while (y >= 0)
            {
                Console.Clear();
                for (int i = 0; i < credits.Length; i++)
                {
                    Console.SetCursorPosition(x - credits[i].Length / 2, y + i);
                    Console.Write(credits[i]);
                }
                Thread.Sleep(500);
                y--;
            }
            Console.Clear();
        }

        public static void PlayerShootAlert(Board board, Position playerShoot, bool hit, string player)
        {
            Console.Clear();
            board.PrintShootBoard();
            Console.WriteLine("{0} atirou na posição: ['{1}':{2}]", player, ConvertPosition(playerShoot.Column), playerShoot.Line + 1);
            if (hit) PrintAlert("Acertou o navio!", 'R');
            else PrintAlert("Acertou na água!", 'R');

            Console.WriteLine("Aperte ENTER para continuar");
            Console.ReadLine();
        }

        public static void MachineShootAlert(Board board, Position machineShoot, bool hit)
        {
            Console.Clear();
            board.PrintShootBoard();
            Console.WriteLine("Máquina atirou na posição: ['{0}':{1}]", ConvertPosition(machineShoot.Column), machineShoot.Line + 1);
            if (hit) PrintAlert("Acertou o navio!", 'R');
            else PrintAlert("Acertou na água!", 'R');

            Console.WriteLine("Aperte ENTER para continuar");
            Console.ReadLine();
        }

        public static char ConvertPosition(int pos)
        {
            return (char)((int)'A' + pos);
        }
    }
}