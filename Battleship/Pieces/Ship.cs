using Battleship.GameBoard;

namespace Battleship.Pieces
{
    internal abstract class Ship : Piece
    {
        // Navios possuem propriedade adicional de tamanho
        public int Size { get; set; }
    }
}
