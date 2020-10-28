using Board;
using Chess.Board;
using System;
using System.Collections.Generic;

namespace Chess {
    class ChessMatch {

        public ChessBoard Board { get; private set; }
        public int Turn { get; private set; }
        public Color Player { get; private set; }
        public bool Ended { get; private set; }
        private HashSet<Piece> Pieces;
        private HashSet<Piece> Captured;

        public ChessMatch() {
            Board = new ChessBoard(8, 8);
            Turn = 1;
            Player = Color.White;
            Pieces = new HashSet<Piece>();
            Captured = new HashSet<Piece>();
            PutPieces();
            Ended = false;
        }

        public void MakeMove(Position origin, Position destiny) {
            Piece piece = Board.RemovePiece(origin);
            piece.IncrementMoves();
            Piece capturedPiece = Board.RemovePiece(destiny);
            Board.PutPiece(piece, destiny);
            if (capturedPiece != null) {
                Captured.Add(capturedPiece);
            }
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

        public HashSet<Piece> CapturedPieces(Color color) {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in Captured) {
                if (x.Color == color) {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Piece> GamePieces(Color color) {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in Captured) {
                if (x.Color == color) {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(CapturedPieces(color));
            return aux;
        }

        public void PutNewPiece(char column, int row, Piece piece) {
            Board.PutPiece(piece, new ChessPosition(column, row).ConvertPosition());
            Pieces.Add(piece);
        }

        private void PutPieces() {
            PutNewPiece('C', 1, new Rook(Board, Color.White));
            PutNewPiece('C', 2, new Rook(Board, Color.White));
            PutNewPiece('D', 2, new Rook(Board, Color.White));
            PutNewPiece('E', 2, new Rook(Board, Color.White));
            PutNewPiece('E', 1, new Rook(Board, Color.White));
            PutNewPiece('D', 1, new King(Board, Color.White));

            PutNewPiece('C', 7, new Rook(Board, Color.Black));
            PutNewPiece('C', 8, new Rook(Board, Color.Black));
            PutNewPiece('D', 7, new Rook(Board, Color.Black));
            PutNewPiece('E', 7, new Rook(Board, Color.Black));
            PutNewPiece('E', 8, new Rook(Board, Color.Black));
            PutNewPiece('D', 8, new King(Board, Color.Black));
        }
    }
}
