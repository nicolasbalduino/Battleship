using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.Board;

namespace Battleship.GameBoard
{
    internal class Board
    {
        public Piece[,] board;

        public Board(int lines, int columns)
        {
            board = new Piece[lines, columns];
        }

        public void PrintBoard()
        {
            char[] letters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            Console.Write("   | ");
            for (int i = 0; i < board.GetLength(1); i++)
            {
                Console.Write(letters[i] + " | ");
            }
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

        public void InsertBoard(Piece piece, Position pos)
        {
            board[pos.Line, pos.Column] = piece;
        }
    }
}

