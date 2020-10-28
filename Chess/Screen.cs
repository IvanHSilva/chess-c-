using System;
using System.Collections.Generic;
using Board;

namespace Chess {
    class Screen {

        public static void PrintMatch(ChessMatch match) {
            PrintBoard(match.Board);
            Console.WriteLine();
            PrintCapturedPieces(match);
            Console.WriteLine();
            Console.WriteLine("Turno: " + match.Turn);
            Console.WriteLine("Aguardando: " + (match.Player == Color.White ? "Branca" : "Preta"));
            if (match.Check) {
                Console.WriteLine("XEQUE!");
            }
        }

        public static void PrintCapturedPieces(ChessMatch match) {
            Console.WriteLine("Peças capturadas:");
            Console.Write("Brancas: ");
            PrintSet(match.CapturedPieces(Color.White));
            Console.WriteLine();
            Console.Write("Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintSet(match.CapturedPieces(Color.Black));
            Console.ForegroundColor = aux; 
            Console.WriteLine();
        }

        private static void PrintSet(HashSet<Piece> set) {
            Console.Write("[");
            foreach (Piece x in set) {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }

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
