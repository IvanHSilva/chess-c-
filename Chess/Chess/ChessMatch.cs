using Board;
using Chess.Board;
using System;

namespace Chess {
    class ChessMatch {

        public ChessBoard Board { get; private set; }
        public int Turn { get; private set; }
        public Color Player { get; private set; }
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

        public void MakePlay(Position origin, Position destiny) {
            MakeMove(origin, destiny);
            Turn++;
            ChangePlayer();
        }

        private void ChangePlayer() {
            if (Player == Color.White) {
                Player = Color.Black;
            } else {
                Player = Color.White;
            }
        }

        public void ValidOriginPosition(Position pos) {
            if (Board.Piece(pos) == null) {
                throw new BoardException("Não existe peça na origem selecionada!");
            }
            if (Player !=  Board.Piece(pos).Color) {
                throw new BoardException("A peça selecionada não é sua!");
            }
            if (!Board.Piece(pos).ExistsPossibleMoves()) {
                throw new BoardException("Não existem movimentos possíveis para a peça selecionada!");
            }
        }

        public void ValidDestinyPosition(Position origin, Position destiny) {
            if (!Board.Piece(origin).CanMoveTo(destiny)) {
                throw new BoardException("Destino inválido!");
            }
        }

        private void PutPieces() {
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
