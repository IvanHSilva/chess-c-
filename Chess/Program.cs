using Board;
using Chess.Board;
using System;

namespace Chess {
    class Program {
        static void Main(string[] args) {

            try {
                ChessMatch Match = new ChessMatch();

                while (!Match.Ended) {
                    Console.Clear();
                    Screen.PrintBoard(Match.Board);

                    Console.WriteLine();
                    Console.Write("Origem: ");
                    Position origin = Screen.ReadPosition().ConvertPosition();

                    bool[,] possibleMoves = Match.Board.Piece(origin).PossibleMoves();

                    Console.Clear();
                    Screen.PrintBoard(Match.Board, possibleMoves);

                    Console.WriteLine();
                    Console.Write("Destino: ");
                    Position destiny  = Screen.ReadPosition().ConvertPosition();
                    Match.MakeMove(origin, destiny);
                }

            }  catch(BoardException e) {
                Console.WriteLine(e.Message);
            }
         }
    }
}
