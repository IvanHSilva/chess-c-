using Board;
using System;

namespace Chess {
    class Rook : Piece {

        public Rook(ChessBoard board, Color color) : base(board, color) { }

        public override string ToString() {
            return "R";
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
                pos.Row--;
            }

            //Down position (S)
            pos.SetValues(Position.Row + 1, Position.Column);
            while (Board.ValidPosition(pos) && CanMove(pos)) {
                matrix[pos.Row, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color) {
                    break;
                }
                pos.Row++;
            }

            //Right position (E)
            pos.SetValues(Position.Row , Position.Column + 1);
            while (Board.ValidPosition(pos) && CanMove(pos)) {
                matrix[pos.Row, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color) {
                    break;
                }
                pos.Column++;
            }

            //Left position
            pos.SetValues(Position.Row, Position.Column - 1);
            while (Board.ValidPosition(pos) && CanMove(pos)) {
                matrix[pos.Row, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color) {
                    break;
                }
                pos.Column--;
            }

            return matrix;
        }
    }
}