using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    internal class Program
    {
        public static void Main(string[] args)
        {
                char[,] board = new char[20, 20];
                board[0, 0] = 'X';
                board[9, 9] = 'O';

                // impressao tabuleiro
                Console.Write("   | A | B | C | D | E | F | G | H | I | J | K | L | M | N | O | P | Q | R | S | T |");
                Console.Write("\n------------------------------------------------------------------------------------");
                for (int line = 0; line < 20; line++)
                {
                    Console.Write("\n" + (line + 1).ToString("D2") + " | ");
                    for (int column = 0; column < 20; column++)
                    {
                        if (board[line, column] == '\0')
                            Console.Write("  | ");
                        else
                            Console.Write(board[line, column] + " | ");
                    }

                    Console.Write("\n------------------------------------------------------------------------------------");
                }

                // Buscar coordenada
                string coordenada;
                do
                {
                    Console.Write("Digite uma coordenada (ex: A1): ");
                    coordenada = Console.ReadLine();

                    if (char.IsLetter(coordenada[0]) && char.IsNumber(coordenada[1]))
                    {
                        Console.WriteLine("Coluna " + coordenada[0].ToString().ToUpper());
                        Console.WriteLine("Linha " + coordenada[1]);
                    }
                    else if (char.IsLetter(coordenada[1]) && char.IsNumber(coordenada[0]))
                    {
                        Console.WriteLine("Coluna " + coordenada[1].ToString().ToUpper());
                        Console.WriteLine("Coluna " + (char.ToUpperInvariant(coordenada[1]) - 'A'));
                        Console.WriteLine("Linha " + coordenada[0]);
                    }
                    else
                    {
                        Console.WriteLine("Informe outra coordenada");
                        coordenada = "\0";
                    }
                } while (coordenada.Length != 2);
        }
    }
}
