using Board;

namespace Chess {
    class Queen : Piece {

        public Queen(ChessBoard board, Color color) : base(board, color) { }

        public override string ToString() {
            return "Q";
        }

        private bool CanMove(Position pos) {
            Piece piece = Board.Piece(pos);
            return piece == null || piece.Color != Color;
        }

        public override bool[,] PossibleMoves() {
            bool[,] matrix = new bool[Board.Rows, Board.Columns];
            Position pos = new Position(0, 0);

            //Up position (N)
            pos.SetValues(Position.Row - 1, Position.Column);
            while (Board.ValidPosition(pos) && CanMove(pos)) {
                matrix[pos.Row, pos.Column] = true;
                if (Board.Piece(pos) !=null && Board.Piece(pos).Color != Color) {
                    break;
                }
                pos.SetValues(pos.Row - 1, pos.Column);
            }

            //Down position (S)
            pos.SetValues(Position.Row + 1, Position.Column);
            while (Board.ValidPosition(pos) && CanMove(pos)) {
                matrix[pos.Row, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color) {
                    break;
                }
                pos.SetValues(pos.Row + 1, pos.Column);
            }

            //Right position (E)
            pos.SetValues(Position.Row , Position.Column + 1);
            while (Board.ValidPosition(pos) && CanMove(pos)) {
                matrix[pos.Row, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color) {
                    break;
                }
                pos.SetValues(pos.Row, pos.Column + 1);
            }

            //Left position
            pos.SetValues(Position.Row, Position.Column - 1);
            while (Board.ValidPosition(pos) && CanMove(pos)) {
                matrix[pos.Row, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color) {
                    break;
                }
                pos.SetValues(pos.Row, pos.Column - 1);
            }

            //Up-left position (NW)
            pos.SetValues(Position.Row - 1, Position.Column - 1);
            while (Board.ValidPosition(pos) && CanMove(pos)) {
                matrix[pos.Row, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color) {
                    break;
                }
                pos.SetValues(pos.Row - 1, pos.Column - 1);
            }

            //Up-right position (NE)
            pos.SetValues(Position.Row - 1, Position.Column + 1);
            while (Board.ValidPosition(pos) && CanMove(pos)) {
                matrix[pos.Row, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color) {
                    break;
                }
                pos.SetValues(pos.Row - 1, pos.Column + 1);
            }

            //Down-left position (SW)
            pos.SetValues(Position.Row + 1, Position.Column - 1);
            while (Board.ValidPosition(pos) && CanMove(pos)) {
                matrix[pos.Row, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color) {
                    break;
                }
                pos.SetValues(pos.Row + 1, pos.Column - 1);
            }

            //Down-right position (SE)
            pos.SetValues(Position.Row + 1, Position.Column + 1);
            while (Board.ValidPosition(pos) && CanMove(pos)) {
                matrix[pos.Row, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color) {
                    break;
                }
                pos.SetValues(pos.Row + 1, pos.Column + 1);
            }

            return matrix;
        }
    }
}