internal class Program
{
    private static void Main(string[] args)
    {
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