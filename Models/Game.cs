using TicTacToe.Enums;

namespace TicTacToe.Models
{
    public class Game
    {
        public EDifficulty Difficulty { get; private set; }
        public Player[] Players { get; private set; }
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

        public Game(EDifficulty difficulty, Player[] players)
        {
            Difficulty = difficulty;
            Players = players;

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
