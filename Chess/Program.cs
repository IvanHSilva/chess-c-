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
                    Console.Write("Origem: ");
                    Position origin = Screen.ReadPosition().ConvertPosition();
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
