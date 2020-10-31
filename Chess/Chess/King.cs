using Board;

namespace Chess {
    class King : Piece {

        private ChessMatch match;

        public King(ChessBoard board, Color color, ChessMatch match) : base(board, color) {
            this.match = match;        
        }

        public override string ToString() {
            return "K";
        }

        private bool CanMove(Position pos) {
            Piece piece = Board.Piece(pos);
            return piece == null || piece.Color != Color;
        }

        private bool RoqueRook(Position pos) {
            Piece p = Board.Piece(pos);
            return p != null && p is Rook && p.Color == Color && p.TotalMoves == 0;
        }

        public override bool[,] PossibleMoves() {
            bool[,] matrix = new bool[Board.Rows, Board.Columns];
            Position pos = new Position(0, 0);

            //Up position (N)
            pos.SetValues(Position.Row - 1, Position.Column);
            if (Board.ValidPosition(pos) && CanMove(pos)) {
                matrix[pos.Row, pos.Column] = true;
            }

            //Up-right position (NE)
            pos.SetValues(Position.Row - 1, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos)) {
                matrix[pos.Row, pos.Column] = true;
            }

            //Right position (E)
            pos.SetValues(Position.Row, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos)) {
                matrix[pos.Row, pos.Column] = true;
            }

            //Down-right position (SE)
            pos.SetValues(Position.Row + 1, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos)) {
                matrix[pos.Row, pos.Column] = true;
            }

            //Down position (S)
            pos.SetValues(Position.Row + 1, Position.Column);
            if (Board.ValidPosition(pos) && CanMove(pos)) {
                matrix[pos.Row, pos.Column] = true;
            }

            //Down-left position (SW)
            pos.SetValues(Position.Row + 1, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos)) {
                matrix[pos.Row, pos.Column] = true;
            }

            //Left position
            pos.SetValues(Position.Row, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos)) {
                matrix[pos.Row, pos.Column] = true;
            }

            //Up-left position (NW)
            pos.SetValues(Position.Row - 1, Position.Column -1);
            if (Board.ValidPosition(pos) && CanMove(pos)) {
                matrix[pos.Row, pos.Column] = true;
            }

            if (TotalMoves == 0 && !match.Check) {
                //Little Roque
                Position posRook1 = new Position(Position.Row, Position.Column + 3);
                if (RoqueRook(posRook1)) {
                    Position p1 = new Position(Position.Row, Position.Column + 1);
                    Position p2 = new Position(Position.Row, Position.Column + 2);
                    if (Board.Piece(p1) == null && Board.Piece(p2) == null) {
                        matrix[Position.Row, Position.Column + 2] = true;
                    }
                }
                //Big Roque
                Position posRook2 = new Position(Position.Row, Position.Column - 4);
                if (RoqueRook(posRook2)) {
                    Position p1 = new Position(Position.Row, Position.Column - 1);
                    Position p2 = new Position(Position.Row, Position.Column - 2);
                    Position p3 = new Position(Position.Row, Position.Column - 3);
                    if (Board.Piece(p1) == null && Board.Piece(p2) == null && Board.Piece(p3) == null) {
                        matrix[Position.Row, Position.Column - 2] = true;
                    }
                }
            }

            return matrix;
        }
    }
}