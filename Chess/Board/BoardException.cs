using System;

namespace Chess.Board {
    class BoardException : Exception {

        public BoardException(string msg) : base(msg) {}
    }
}
