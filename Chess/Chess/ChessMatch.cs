using Board;
using System;

namespace Chess {
    class ChessMatch {

        public ChessBoard Board { get; private set; }
        private int Turn;
        private Color Player;
        public bool Ended { get; private set; }

        public ChessMatch() {
            Board = new ChessBoard(8, 8);
            Turn = 1;
            Player = Color.White;
            PutPieces();
            Ended = false;
        }

        public void MakeMove(Position origin, Position destiny) {
            Piece piece = Board.RemovePiece(origin);
            piece.IncrementMoves();
            Piece capturedPeace = Board.RemovePiece(destiny);
            Board.PutPiece(piece, destiny);
        }

        private  void PutPieces() {
            Board.PutPiece(new Rook(Board, Color.White), new ChessPosition('C', 1).ConvertPosition());
            Board.PutPiece(new Rook(Board, Color.White), new ChessPosition('C', 2).ConvertPosition());
            Board.PutPiece(new Rook(Board, Color.White), new ChessPosition('D', 2).ConvertPosition());
            Board.PutPiece(new Rook(Board, Color.White), new ChessPosition('E', 2).ConvertPosition());
            Board.PutPiece(new Rook(Board, Color.White), new ChessPosition('E', 1).ConvertPosition());
            Board.PutPiece(new King(Board, Color.White), new ChessPosition('D', 1).ConvertPosition());

            Board.PutPiece(new Rook(Board, Color.Black), new ChessPosition('C', 7).ConvertPosition());
            Board.PutPiece(new Rook(Board, Color.Black), new ChessPosition('C', 8).ConvertPosition());
            Board.PutPiece(new Rook(Board, Color.Black), new ChessPosition('D', 7).ConvertPosition());
            Board.PutPiece(new Rook(Board, Color.Black), new ChessPosition('E', 7).ConvertPosition());
            Board.PutPiece(new Rook(Board, Color.Black), new ChessPosition('E', 8).ConvertPosition());
            Board.PutPiece(new King(Board, Color.Black), new ChessPosition('D', 8).ConvertPosition());
        }
    }
}
