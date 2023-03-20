namespace Battleship.GameBoard
{
    internal abstract class Piece
    {
        public string PieceName { get; protected set; }
        public Piece Overlap { get; set; }

        public Piece()
        {
            PieceName = "Piece";
        }
    }
}
