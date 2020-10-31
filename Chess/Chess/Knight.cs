using Board;
using System;

namespace Chess {
    class Knight : Piece {

        public Knight(ChessBoard board, Color color) : base(board, color) {}

        public override string ToString() {
            return "H";
        }

        private bool CanMove(Position pos) {
            Piece piece = Board.Piece(pos);
            return piece == null || piece.Color != Color;
        }

        public override bool[,] PossibleMoves() {
            bool[,] matrix = new bool[Board.Rows, Board.Columns];
            Position pos = new Position(0, 0);

            //Position 1
            pos.SetValues(Position.Row - 1, Position.Column - 2);
            if (Board.ValidPosition(pos) && CanMove(pos)) {
                matrix[pos.Row, pos.Column] = true;
            }

            //Position 2
            pos.SetValues(Position.Row - 2, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos)) {
                matrix[pos.Row, pos.Column] = true;
            }

            //Position 3
            pos.SetValues(Position.Row - 2, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos)) {
                matrix[pos.Row, pos.Column] = true;
            }

            //Position 4
            pos.SetValues(Position.Row - 1, Position.Column + 2);
            if (Board.ValidPosition(pos) && CanMove(pos)) {
                matrix[pos.Row, pos.Column] = true;
            }

            //Position 5
            pos.SetValues(Position.Row + 1, Position.Column + 2);
            if (Board.ValidPosition(pos) && CanMove(pos)) {
                matrix[pos.Row, pos.Column] = true;
            }

            //Position 6
            pos.SetValues(Position.Row + 2, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos)) {
                matrix[pos.Row, pos.Column] = true;
            }

            //Position 7
            pos.SetValues(Position.Row + 2, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos)) {
                matrix[pos.Row, pos.Column] = true;
            }

            //Position 8
            pos.SetValues(Position.Row + 1, Position.Column - 2);
            if (Board.ValidPosition(pos) && CanMove(pos)) {
                matrix[pos.Row, pos.Column] = true;
            }

            return matrix;
        }
    }
}