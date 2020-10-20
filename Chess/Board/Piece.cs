namespace Board {
    class Piece {

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
    }
}
