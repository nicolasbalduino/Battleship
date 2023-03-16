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
                Console.WriteLine("There is already a ship there...\n" +
                                  "Pass new cordinates...");
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
