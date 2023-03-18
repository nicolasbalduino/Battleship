using Battleship.GameBoard;
using Battleship.Pieces;

namespace Battleship
{
    internal class Machine
    {
        public Random rdn { get; private set; }
        public Board Board { get; private set; }
        public Board EnemyBoard { get; private set; }
        List<Position> ShootedPositions { get; set; }

        public Machine(Board board, Board enemyBoard)
        {
            rdn = new Random();
            Board = board;
            EnemyBoard = enemyBoard;
            ShootedPositions = new List<Position>();
        }

        public void PlaceShip(Ship piece)
        {
            // Adiciona peças em coordenadas aleatórias do tabuleiro
            Position pos = Coordinates();

            // Escolhe aleatóriamente uma direção
            char direction;
            if (rdn.Next(2) == 1) direction = 'H';
            else direction = 'V';

            // Verifica se posicionamento está correto e insere as peças
            if (!Board.InsertPiece(piece, pos, direction)) PlaceShip(piece);
        }

        public bool PlaceRandomShoot(out Position machineShoot)
        {
            // Insere um tiro em uma coordenada
            Shoot shoot = new Shoot();
            Position pos = Coordinates();
            if (!EnemyBoard.InsertShoot(shoot, pos)) PlaceRandomShoot(out machineShoot);

            // Armazena o local do tiro
            ShootedPositions.Add(pos);
            machineShoot = pos;

            // Verifica se acertou em alguma peça
            if (shoot.Overlap != null) return true;
            return false;
        }

        public bool PlaceProximityShoot(ref Position machineShoot)
        {
            // Utiliza o resultado do random para escolha de posicionamento do próximo tiro
            Position pos = ProximityShoot(machineShoot);

            // Insere posição aleatória caso já não haja uma
            if (pos == null) pos = Coordinates();

            // Insere um tiro em uma coordenada calculada
            Shoot shoot = new Shoot();
            if (!EnemyBoard.InsertShoot(shoot, pos)) PlaceProximityShoot(ref machineShoot);

            // Armazena o local do tiro
            ShootedPositions.Add(pos);
            machineShoot = pos;

            // Verifica se acertou em alguma peça
            if (shoot.Overlap != null) return true;
            return false;
        }

        public bool PlaceSequentialShoot(ref Position machineShoot)
        {
            Position pos = SequentialShoot(machineShoot);

            // Insere posição aleatória caso já não haja uma
            if (pos == null) pos = Coordinates();

            // Insere um tiro em uma coordenada sequencial
            Shoot shoot = new Shoot();
            if (!EnemyBoard.InsertShoot(shoot, pos)) PlaceProximityShoot(ref machineShoot);

            // Armazena o local do tiro
            ShootedPositions.Add(pos);
            machineShoot = pos;

            // Verifica se acertou em alguma peça
            if (shoot.Overlap != null) return true;
            return false;
        }

        public Position ProximityShoot(Position machineShoot)
        {
            List<Position> VerifyProximityShoot = new List<Position>();
            Position pos;

            // Verifica as posições disponíveis e as armazena na lista
            pos = new Position(machineShoot.Line - 1, machineShoot.Column);
            if (EnemyBoard.VerifyShootPosition(pos)) VerifyProximityShoot.Add(pos);

            pos = new Position(machineShoot.Line, machineShoot.Column - 1);
            if (EnemyBoard.VerifyShootPosition(pos)) VerifyProximityShoot.Add(pos);

            pos = new Position(machineShoot.Line, machineShoot.Column + 1);
            if (EnemyBoard.VerifyShootPosition(pos)) VerifyProximityShoot.Add(pos);

            pos = new Position(machineShoot.Line + 1, machineShoot.Column);
            if (EnemyBoard.VerifyShootPosition(pos)) VerifyProximityShoot.Add(pos);

            // Caso lista esteja vazia, retorna uma posição aleatória do tabuleiro
            if (VerifyProximityShoot.Count == 0) return null;

            // Escolhe uma posição aleatória da lista
            int position = rdn.Next(VerifyProximityShoot.Count);

            return VerifyProximityShoot[position];
        }

        public Position SequentialShoot(Position machineShoot)
        {
            int line, column;
            Position pos;

            // Procura a próxima linha e coluna da sequência
            line = (ShootedPositions.Last().Line - ShootedPositions[ShootedPositions.Count - 2].Line);
            column = (ShootedPositions.Last().Column - ShootedPositions[ShootedPositions.Count - 2].Column);

            // Calcula a posição
            pos = new Position(machineShoot.Line + line, machineShoot.Column + column);

            // Envia posição caso válida
            if (EnemyBoard.VerifyShootPosition(pos)) return pos;

            // Caso inválida, faz processo inverso de procura
            line = (ShootedPositions[ShootedPositions.Count - 2].Line - ShootedPositions.Last().Line);
            column = (ShootedPositions[ShootedPositions.Count - 2].Column - ShootedPositions.Last().Column);

            // Calcula a posição
            pos = new Position(ShootedPositions[ShootedPositions.Count - 2].Line + line, ShootedPositions[ShootedPositions.Count - 2].Column + column);

            // Envia posição caso válida
            if (EnemyBoard.VerifyShootPosition(pos)) return pos;

            // Caso inválida, retorna null
            return null;
        }

        public Position Coordinates()
        {
            Position pos;
            // Verifica se há tiros no tabuleiro
            pos = VerifyBoard();
            if (pos != null) return pos;

            // Caso não haja tiros, realiza o tiro em uma posição aleatória
            int coordinateX = rdn.Next(Board.Lines);
            int coordinateY = rdn.Next(Board.Columns);
            pos = new Position(coordinateX, coordinateY);
            return pos;
        }

        public Position VerifyBoard()
        {
            Position pos;
            // Verifica se há tiros no tabuleiro
            for (int i = 0; i < ShootedPositions.Count(); i++)
            {
                // Caso contenha tiros acertados, verifica se há posições ao redor e as envia
                if (EnemyBoard.board[ShootedPositions[i].Line, ShootedPositions[i].Column].Overlap != null) return ProximityShoot(ShootedPositions[i]);
            }

            return null;
        }

        public int AllShootedPositions()
        {
            int count = 0;
            for (int i = 0; i < ShootedPositions.Count(); i++) {
                if (EnemyBoard.board[ShootedPositions[i].Line, ShootedPositions[i].Column].Overlap != null) count++;
            }
            return count;
        }
    }
}
