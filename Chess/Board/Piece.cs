namespace Board {
    abstract class Piece {

        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int TotalMoves { get; protected set; }
        public ChessBoard Board { get; protected set; }

        public Piece(ChessBoard board, Color color) {
            Position = null;
            Color = color;
            Board = board;
            TotalMoves = 0;
        }

        public void IncrementMoves() {
            TotalMoves++;
        }

        public void DecrementMoves() {
            TotalMoves--;
        }

        public bool ExistsPossibleMoves() {
            bool[,] matrix = PossibleMoves();
            for (int i = 0; i < Board.Rows; i++) {
                for (int j = 0; j < Board.Columns; j++) {
                    if (matrix[i, j] == true) {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CanMoveTo(Position pos) {
            return PossibleMoves()[pos.Row, pos.Column];
        }

        public abstract bool[,] PossibleMoves();
    }
}
