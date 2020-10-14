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

        public void PutPiece(Piece pc, Position pos) {
            Pieces[pos.Row, pos.Column] = pc;
            pc.Position = pos;
        }
    }
}
