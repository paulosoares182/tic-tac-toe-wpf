using System.ComponentModel;

namespace TicTacToe.Enums
{
    public enum EDifficulty : short
    {
        [Description("Fácil")]
        EASY = 0,
        [Description("Normal")]
        NORMAL = 1,
        [Description("Difícil")]
        HARD = 3
    }
}
