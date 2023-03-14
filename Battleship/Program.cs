internal class Program
{
    private static void Main(string[] args)
    {
        char[,] board = new char[20,20];
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
    }
}