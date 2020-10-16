using Board;
using Chess.Board;
using Chess.Chess;
using System;

namespace Chess {
    class Program {
        static void Main(string[] args) {

            //Position P = new Position(3, 4);
            //Console.WriteLine("Posição: " + P);
            try {
                ChessBoard Board = new ChessBoard(8, 8);
                Board.PutPiece(new Rook(Board, Color.Black), new Position(0, 0));
                Board.PutPiece(new Rook(Board, Color.Black), new Position(1, 3));
                Board.PutPiece(new King(Board, Color.Black), new Position(2, 4));
                
                Board.PutPiece(new King(Board, Color.White), new Position(3, 5));
                Board.PutPiece(new Rook(Board, Color.White), new Position(4, 7));
                Board.PutPiece(new Rook(Board, Color.White), new Position(5, 0));
                Screen.PrintBoard(Board);

                /*ChessPosition position = new ChessPosition('C', 7);
                Console.WriteLine(position);
                Console.WriteLine(position.ConvertPosition());*/
            }  catch(BoardException e) {
                Console.WriteLine(e.Message);
            }
         }
    }
}
