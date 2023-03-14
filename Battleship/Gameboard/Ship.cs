namespace Battleship.Gameboard
{
    abstract class Ship
    {
        public string TypeOf { get; set; }        
        public int SizeOf { get; protected set; }

        protected Ship(string typeOf,  int sizeOf)
        {
            TypeOf = typeOf;            
            SizeOf = sizeOf;
        }

        public Ship() { }
    }
}
