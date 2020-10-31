using Board;
using System;

namespace Chess {
    class Pawn : Piece {

        public Pawn(ChessBoard board, Color color) : base(board, color) { }

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
                if (Board.ValidPosition(pos) && Free(pos) && TotalMoves == 0) {
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
            } else {
                pos.SetValues(Position.Row + 1, Position.Column);
                if (Board.ValidPosition(pos) && Free(pos)) {
                    matrix[pos.Row, pos.Column] = true;
                }
                pos.SetValues(Position.Row + 2, Position.Column);
                if (Board.ValidPosition(pos) && Free(pos) && TotalMoves == 0) {
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
            }

            return matrix;
        }
    }
}