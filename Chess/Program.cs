using Board;
using Chess.Board;
using System;

namespace Chess {
    class Program {
        static void Main(string[] args) {

            try {
                ChessMatch Match = new ChessMatch();

                while (!Match.Ended) {
                    try {
                        Console.Clear();
                        Screen.PrintMatch(Match);

                        Console.WriteLine();
                        Console.Write("Origem: ");
                        Position origin = Screen.ReadPosition().ConvertPosition();
                        Match.ValidOriginPosition(origin);

                        bool[,] possibleMoves = Match.Board.Piece(origin).PossibleMoves();

                        Console.Clear();
                        Screen.PrintBoard(Match.Board, possibleMoves);

                        Console.WriteLine();
                        Console.Write("Destino: ");
                        Position destiny = Screen.ReadPosition().ConvertPosition();
                        Match.ValidDestinyPosition(origin, destiny);
                        Match.MakePlay(origin, destiny);
                    } catch (BoardException e) {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
            }  catch (BoardException e) {
                Console.WriteLine(e.Message);
            }
         }
    }
}
