using Board;

namespace Chess {
    class Pawn : Piece {

        private ChessMatch match;

        public Pawn(ChessBoard board, Color color, ChessMatch match) : base(board, color) {
            this.match = match;
        }

        public override string ToString() {
            return "P";
        }

        private bool isThereEnemy(Position pos) {
            Piece p = Board.Piece(pos);
            return p != null && p.Color != Color;
        }

        private bool Free(Position pos) {
            return Board.Piece(pos) == null;
        }

        public override bool[,] PossibleMoves() {
            bool[,] matrix = new bool[Board.Rows, Board.Columns];
            Position pos = new Position(0, 0);

            if (Color == Color.White) {
                pos.SetValues(Position.Row - 1, Position.Column);
                if (Board.ValidPosition(pos) && Free(pos)) {
                    matrix[pos.Row, pos.Column] = true;
                }
                pos.SetValues(Position.Row - 2, Position.Column);
                Position pos2 = new Position(Position.Row - 1, Position.Column);
                if (Board.ValidPosition(pos2) && Free(pos2) && Board.ValidPosition(pos) && Free(pos) && TotalMoves == 0) {
                    matrix[pos.Row, pos.Column] = true;
                }
                pos.SetValues(Position.Row - 1, Position.Column - 1);
                if (Board.ValidPosition(pos) && isThereEnemy(pos)) {
                    matrix[pos.Row, pos.Column] = true;
                }
                pos.SetValues(Position.Row - 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && isThereEnemy(pos)) {
                    matrix[pos.Row, pos.Column] = true;
                }
                //En Passant
                if (Position.Row == 3) {
                    Position left = new Position(Position.Row, Position.Column - 1);
                    if (Board.ValidPosition(left) && isThereEnemy(left) && Board.Piece(left) == match.vulnerableEnPassant) {
                        matrix[left.Row - 1, left.Column] = true;
                    }
                    Position right = new Position(Position.Row, Position.Column + 1);
                    if (Board.ValidPosition(right) && isThereEnemy(right) && Board.Piece(right) == match.vulnerableEnPassant) {
                        matrix[right.Row - 1, right.Column] = true;
                    }
                }
            } else {
                pos.SetValues(Position.Row + 1, Position.Column);
                if (Board.ValidPosition(pos) && Free(pos)) {
                    matrix[pos.Row, pos.Column] = true;
                }
                pos.SetValues(Position.Row + 2, Position.Column);
                Position pos2 = new Position(Position.Row + 1, Position.Column);
                if (Board.ValidPosition(pos2) && Free(pos2) && Board.ValidPosition(pos) && Free(pos) && TotalMoves == 0) {
                    matrix[pos.Row, pos.Column] = true;
                }
                pos.SetValues(Position.Row + 1, Position.Column - 1);
                if (Board.ValidPosition(pos) && isThereEnemy(pos)) {
                    matrix[pos.Row, pos.Column] = true;
                }
                pos.SetValues(Position.Row + 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && isThereEnemy(pos)) {
                    matrix[pos.Row, pos.Column] = true;
                }
                //En Passant
                if (Position.Row == 4) {
                    Position left = new Position(Position.Row, Position.Column - 1);
                    if (Board.ValidPosition(left) && isThereEnemy(left) && Board.Piece(left) == match.vulnerableEnPassant) {
                        matrix[left.Row + 1, left.Column] = true;
                    }
                    Position right = new Position(Position.Row, Position.Column + 1);
                    if (Board.ValidPosition(right) && isThereEnemy(right) && Board.Piece(right) == match.vulnerableEnPassant) {
                        matrix[right.Row + 1, right.Column] = true;
                    }
                }
            }

            return matrix;
        }
    }
}