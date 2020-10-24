using System;
using Board;

namespace Chess {
    class Screen {

        public static void PrintBoard(ChessBoard board) {
            for (int i = 0; i < board.Rows; i++) {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++) {
                    PrintPiece(board.Piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        public static void PrintBoard(ChessBoard board, bool[,] possibleMoves) {

            ConsoleColor originalColor = Console.BackgroundColor;
            ConsoleColor markedColor = ConsoleColor.DarkGray;
            for (int i = 0; i < board.Rows; i++) {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++) {
                    if (possibleMoves[i, j] == true) {
                        Console.BackgroundColor = markedColor;
                    } else {
                        Console.BackgroundColor = originalColor;
                    }
                    PrintPiece(board.Piece(i, j));
                    Console.BackgroundColor = originalColor;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
            //Console.BackgroundColor = originalColor;
        }

        public static ChessPosition ReadPosition() {
            string coord = Console.ReadLine().ToUpper();
            char column = coord[0];
            int row = int.Parse(coord[1] + "");
            return new ChessPosition(column, row);
        }

        public static void PrintPiece(Piece piece) {
            if (piece == null) {
                Console.Write("- ");
            } else {
                if (piece.Color == Color.White) {
                    Console.Write(piece);
                } else {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }
    }
}
