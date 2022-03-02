using TicTacToe.Enums;

namespace TicTacToe.Models
{
    public class PlayerModel
    {
        public string Nickname { get; private set; }
        public EPiece Piece { get; private set; }
        public bool IsCPU { get; private set; }
        public int CountWins { get; private set; }

        public PlayerModel(string nickname, EPiece piece, bool isCPU)
        {
            IsCPU = isCPU;
            Nickname = IsCPU ? "CPU" : nickname;
            Piece = piece;
            CountWins = 0;
        }

        public void SetWins() => CountWins++;
        public void ClearWins() => CountWins = 0;
    }
}
