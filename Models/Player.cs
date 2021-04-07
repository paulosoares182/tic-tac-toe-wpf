using TicTacToe.Enums;

namespace TicTacToe.Models
{
    public class Player
    {
        public string Nickname { get; private set; }
        public EPiece Piece { get; private set; }
        public bool IsCPU { get; private set; }
        public int CountWins { get; private set; }

        public Player(string nickname, EPiece piece, bool isCPU)
        {
            Nickname = nickname;
            Piece = piece;
            IsCPU = isCPU;
            CountWins = 0;
        }

        public void SetWins() => CountWins++;
        public void ClearWins() => CountWins = 0;
    }
}
