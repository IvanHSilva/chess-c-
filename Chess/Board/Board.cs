using Chess.Board;

namespace Board {
    class ChessBoard {

        public int Rows { get; set; }
        public int Columns { get; set; }
        private Piece[,] Pieces;

        public ChessBoard(int rows, int columns) {
            Rows = rows;
            Columns = columns;
            Pieces = new Piece[rows, columns];
        }

        public Piece Piece(int row, int column) {
            return Pieces[row, column];
        }

        public Piece Piece(Position pos) {
            return Pieces[pos.Row, pos.Column];
        }

        public void PutPiece(Piece pc, Position pos) {
            if (PieceExists(pos)) {
                throw new BoardException("Já existe uma peça nesta posição!");
            }
            Pieces[pos.Row, pos.Column] = pc;
            pc.Position = pos;
        }

        public bool PieceExists(Position pos) {
            ValidatePosition(pos);
            return Piece(pos) != null;
        }

        public bool ValidPosition(Position pos) {
            if (pos.Row < 0 || pos.Row >= Rows || pos.Column < 0 || pos.Column >= Columns) {
                return false;
            }
            return true;
        }

        public void ValidatePosition(Position pos) {
            if (!ValidPosition(pos)) {
                throw new BoardException("Posição Inválida!");
            }
        }
    }
}
