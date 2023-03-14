using Gameboard;
namespace Battleship.Entities
{
    internal class Player
    {
        public string Name { get; set; }
        public Playerboard Playerboard { get; protected set; }
        


        string ToShot()
        {
            return "X";            
        }
    }
}
