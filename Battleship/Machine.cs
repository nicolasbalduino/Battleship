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
            bool valid = false;
            do
            {

                // Adiciona peças em coordenadas aleatórias do tabuleiro
                Position pos = Coordinates();

                // Escolhe aleatóriamente uma direção
                char direction;
                if (rdn.Next(2) == 1) direction = 'H';
                else direction = 'V';

                // Verifica se posicionamento está correto e insere as peças
                valid = Board.InsertPiece(piece, pos, direction);
            } while (valid);
        }

        public List<Position> Proximity (Position machineShoot)
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

            if (VerifyProximityShoot.Count == 0) return null;

            return VerifyProximityShoot;
        }

        public bool PlaceShoot(out Position machineShoot)
        {
            Position pos;
            bool valid = false;
            Shoot shoot = new Shoot();

            do
            {
                // Verifica cada posição do tabuleiro a procura de tiros anteriores
                pos = VerifyBoard();

                // Se não houver posições possiveís, escolhe uma aleatóra
                if (pos == null) pos = Coordinates();


                // Tenta enviar o tiro no tabuleiro
                valid = EnemyBoard.InsertShoot(shoot, pos);
            } while (!valid);
            
            machineShoot = pos;

            // Caso haja acerto, armazena em uma lista
            if (shoot.Overlap != null)
            {
                ShootedPositions.Add(pos);
                return true;
            }

            return false;
        }

        public Position SequentialShoot(Position machineShoot)
        {
            // Verifica se existe possibilidades de tiro ao redor
            List<Position> proximityShoots = Proximity(machineShoot);

            Position pos = null;

            // Verifica cada posição de tiro
            for (int i = 0; i < proximityShoots.Count; i++)
            {
                int line = proximityShoots[i].Line;
                int column = proximityShoots[i].Column;

                // Se houver outro tiro ao redor, armazenar posição
                if (EnemyBoard.board[line, column] is Shoot && EnemyBoard.board[line, column].Overlap != null)
                {
                    pos = new Position(line, column);
                    break;
                }
            }

            // Se não houver tiros certeiros ao redor, atirar em qualquer posição ao redor
            if (pos == null)
            {
                int position = rdn.Next(proximityShoots.Count);
                return proximityShoots[position];
            }

            // Armazenar diferença entre posição atual e a do tiro ao redor
            int shootLine = machineShoot.Line - pos.Line;
            int shootColumn = machineShoot.Column - pos.Column;

            // Posicionamento de tiro sequencial
            Position shootPosition = new Position(machineShoot.Line + shootLine, machineShoot.Column + shootColumn);

            // Retornar posição caso seja válida, caso contrário, retorna null
            if (!EnemyBoard.VerifyShootPosition(pos)) return null;
            if (EnemyBoard.ContainsShoot(shootPosition)) return null;
            return shootPosition;

        }

        public Position Coordinates()
        {
            // Retorna uma coordenada aleatória
            Position pos;
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
                pos = SequentialShoot(ShootedPositions[i]);
                if (pos != null) return pos;
            }

            return null;
        }
    }
}
