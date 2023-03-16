using Battleship.Gameboard;
using Gameboard;
namespace Battleship.Entities
{
    internal class Player
    {
        public string Name { get; set; }
        public Playerboard Playerboard { get; protected set; }
        
        public Player()
        {
            Name = string.Empty;            
        }

        public void PlaceShipInBoard(Ship sh, Position pos )
        {
            if (Playerboard.VerifyExistentShip(pos))
            {
                Console.WriteLine("Já existe uma embarcação ali\n" +
                                  "Insira novas coordenadas...");
            }
            Playerboard.Ships[pos.Row, pos.Column] = sh;
            sh.Position = pos;
        }

        string ToShot()
        {
            return "X";            
        }
    }
}
