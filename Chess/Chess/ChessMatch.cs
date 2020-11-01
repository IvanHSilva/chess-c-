using Board;
using Chess.Board;
using System.Collections.Generic;

namespace Chess {
    class ChessMatch {

        public ChessBoard Board { get; private set; }
        public int Turn { get; private set; }
        public Color Player { get; private set; }
        public bool Ended { get; private set; }
        private HashSet<Piece> Pieces;
        private HashSet<Piece> Captured;
        public bool Check { get; private set; }
        public Piece vulnerableEnPassant { get; private set; }

    public ChessMatch() {
            Board = new ChessBoard(8, 8);
            Turn = 1;
            Player = Color.White;
            Pieces = new HashSet<Piece>();
            Captured = new HashSet<Piece>();
            PutPieces();
            Ended = false;
            Check = false;
            vulnerableEnPassant = null;
        }

        public Piece MakeMove(Position origin, Position destiny) {
            Piece p = Board.RemovePiece(origin);
            p.IncrementMoves();
            Piece capturedPiece = Board.RemovePiece(destiny);
            Board.PutPiece(p, destiny);
            if (capturedPiece != null) {
                Captured.Add(capturedPiece);
            }
            //Little Castle
            if (p is King && destiny.Column == origin.Column + 2) {
                Position rookOrigin = new Position(origin.Row, origin.Column + 3);
                Position rookDestiny = new Position(origin.Row, origin.Column + 1);
                Piece R = Board.RemovePiece(rookOrigin);
                R.IncrementMoves();
                Board.PutPiece(R, rookDestiny);
            }
            //Big Castle
            if (p is King && destiny.Column == origin.Column - 2) {
                Position rookOrigin = new Position(origin.Row, origin.Column - 4);
                Position rookDestiny = new Position(origin.Row, origin.Column - 1);
                Piece R = Board.RemovePiece(rookOrigin);
                R.IncrementMoves();
                Board.PutPiece(R, rookDestiny);
            }
            //En Passant
            if (p is Pawn) {
                if (origin.Column != destiny.Column && capturedPiece == null) {
                    Position posP;
                    if (p.Color == Color.White) {
                        posP = new Position(destiny.Row + 1, destiny.Column);
                    } else {
                        posP = new Position(destiny.Row - 1, destiny.Column);
                    }
                    capturedPiece = Board.RemovePiece(posP);
                    Captured.Add(capturedPiece);
                }
            }

            return capturedPiece;
        }

        public void UndoMove(Position origin, Position destiny, Piece capturedPiece) {
            Piece p = Board.RemovePiece(destiny);
            p.DecrementMoves();
            if (capturedPiece != null) {
                Board.PutPiece(capturedPiece, destiny);
                Captured.Remove(capturedPiece);
            }
            Board.PutPiece(p, origin);

            //Little Castle
            if (p is King && destiny.Column == origin.Column + 2) {
                Position rookOrigin = new Position(origin.Row, origin.Column + 3);
                Position rookDestiny = new Position(origin.Row, origin.Column + 1);
                Piece R = Board.RemovePiece(rookDestiny);
                R.DecrementMoves();
                Board.PutPiece(R, rookOrigin);
            }
            //Big Castle
            if (p is King && destiny.Column == origin.Column - 2) {
                Position rookOrigin = new Position(origin.Row, origin.Column - 4);
                Position rookDestiny = new Position(origin.Row, origin.Column - 1);
                Piece R = Board.RemovePiece(rookDestiny);
                R.DecrementMoves();
                Board.PutPiece(R, rookOrigin);
            }
            //En Passant
            if (p is Pawn) {
                if (origin.Column != destiny.Column && capturedPiece == vulnerableEnPassant) {
                    Position posP;
                    Piece pawn = Board.RemovePiece(destiny);
                    if (p.Color == Color.White) {
                        posP = new Position(3, destiny.Column);
                    } else {
                        posP = new Position(4, destiny.Column);
                    }
                    Board.PutPiece(pawn, posP);
                }
            }
        }

        public void MakePlay(Position origin, Position destiny) {
            Piece capturedPiece = MakeMove(origin, destiny);
            if (IsChecked(Player)) {
                UndoMove(origin, destiny, capturedPiece);
                throw new BoardException("Você não pode se colocar em xeque!");
            }
            if (IsChecked(Adversary(Player))) {
                Check = true;
            } else {
                Check = false;
            }
            if (TestCheckMate(Adversary(Player))) {
                Ended = true;
            } else {
                Turn++;
                ChangePlayer();
            }
            Piece p = Board.Piece(destiny);
            //En Passant
            if (p is Pawn && (destiny.Row == origin.Row - 2 || destiny.Row == origin.Row + 2)) {
                vulnerableEnPassant = p;
            } else {
                vulnerableEnPassant = null;
            }
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
            if (!Board.Piece(origin).PossibleMove(destiny)) {
                throw new BoardException("Destino inválido!");
            }
        }

        public HashSet<Piece> CapturedPieces(Color color) {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece p in Captured) {
                if (p.Color == color) {
                    aux.Add(p);
                }
            }
            return aux;
        }

        public HashSet<Piece> GamePieces(Color color) {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece p in Pieces) {
                if (p.Color == color) {
                    aux.Add(p);
                }
            }
            aux.ExceptWith(CapturedPieces(color));
            return aux;
        }

        private Color Adversary(Color color) {
            if (color == Color.White) {
                return Color.Black;
            } else {
                return Color.White;
            }
        }

        private Piece IsKing(Color color) {
            foreach (Piece p in GamePieces(color)) {
                if (p is King) {
                    return p;
                }
            }
            return null;
        }

        public bool IsChecked(Color color) {
            Piece K = IsKing(color);
            if (K == null) {
                throw new BoardException("Não há rei da cor " + (color == Color.White ? "preta" : "branca") + " no tabuleiro!");
            }
            foreach(Piece p in GamePieces(Adversary(color))) {
                bool[,] matrix = p.PossibleMoves();
                if (matrix[K.Position.Row, K.Position.Column]) {
                    return true;
                }
            }
            return false;
        }

        public bool TestCheckMate(Color color) {
            if (!IsChecked(color)) {
                return false;
            }
            foreach (Piece p in GamePieces(color)) {
                bool[,] matrix = p.PossibleMoves();
                for (int i = 0; i < Board.Rows; i++) {
                    for (int j = 0; j < Board.Columns; j++) {
                        if (matrix[i, j]) {
                            Position origin = p.Position;
                            Position destiny = new Position(i, j);
                            Piece capturedPiece = MakeMove(origin, destiny);
                            bool testCheck = IsChecked(color);
                            UndoMove(origin, destiny, capturedPiece);
                            if (!testCheck) {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void PutNewPiece(char column, int row, Piece piece) {
            Board.PutPiece(piece, new ChessPosition(column, row).ConvertPosition());
            Pieces.Add(piece);
        }

        private void PutPieces() {
            PutNewPiece('A', 1, new Rook(Board, Color.White));
            PutNewPiece('B', 1, new Knight(Board, Color.White));
            PutNewPiece('C', 1, new Bishop(Board, Color.White));
            PutNewPiece('D', 1, new Queen(Board, Color.White));
            PutNewPiece('E', 1, new King(Board, Color.White, this));
            PutNewPiece('F', 1, new Bishop(Board, Color.White));
            PutNewPiece('G', 1, new Knight(Board, Color.White));
            PutNewPiece('H', 1, new Rook(Board, Color.White));

            PutNewPiece('A', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('B', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('C', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('D', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('E', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('F', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('G', 2, new Pawn(Board, Color.White, this));
            PutNewPiece('H', 2, new Pawn(Board, Color.White, this));

            PutNewPiece('A', 8, new Rook(Board, Color.Black));
            PutNewPiece('B', 8, new Knight(Board, Color.Black));
            PutNewPiece('C', 8, new Bishop(Board, Color.Black));
            PutNewPiece('D', 8, new Queen(Board, Color.Black));
            PutNewPiece('E', 8, new King(Board, Color.Black, this));
            PutNewPiece('F', 8, new Bishop(Board, Color.Black));
            PutNewPiece('G', 8, new Knight(Board, Color.Black));
            PutNewPiece('H', 8, new Rook(Board, Color.Black));

            PutNewPiece('A', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('B', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('C', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('D', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('E', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('F', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('G', 7, new Pawn(Board, Color.Black, this));
            PutNewPiece('H', 7, new Pawn(Board, Color.Black, this));
        }
    }
}
