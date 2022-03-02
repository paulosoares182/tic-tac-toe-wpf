using TicTacToe.Enums;

namespace TicTacToe.Models
{
    public class GameModel
    {
        public EDifficulty Difficulty { get; private set; }
        public PlayerModel[] Players { get; private set; }
        public bool IsMultiplayer { get; set; }
        public EPiece[,] Board { get; private set; }
        public int CountMatches { get; private set; }
        public int CountDraw
        {
            get
            {
                int countWins = Players[0].CountWins + Players[1].CountWins;
                return (CountMatches > 0) ? CountMatches - countWins : CountMatches;
            }
        }

        public GameModel(EDifficulty difficulty, PlayerModel[] players, bool isMultiplayer)
        {
            Difficulty = difficulty;
            Players = players;
            IsMultiplayer = isMultiplayer;

            InitBoard();
        }

        public void SetMatch() => CountMatches++;
        public void ClearMatches() => CountMatches = 0;
        public void InitBoard()
        {
            Board = new EPiece[3, 3]
            {
                {EPiece.EMPTY, EPiece.EMPTY, EPiece.EMPTY },
                {EPiece.EMPTY, EPiece.EMPTY, EPiece.EMPTY },
                {EPiece.EMPTY, EPiece.EMPTY, EPiece.EMPTY },
            };
        }
    }
}
