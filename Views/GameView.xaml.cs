using System.Windows.Controls;
using TicTacToe.Enums;
using TicTacToe.ViewsModels;

namespace TicTacToe.Views
{
    /// <summary>
    /// Interaction logic for GameUI.xaml
    /// </summary>
    public partial class GameView : UserControl
    {
        public GameView(EDifficulty difficulty, string player1Nickname, string player2Nickname)
        {
            InitializeComponent();
            try
            {
                DataContext = new GameViewModel(difficulty, player1Nickname, player2Nickname);
            }
            catch
            {
                throw;
            }
        }
    }
}
