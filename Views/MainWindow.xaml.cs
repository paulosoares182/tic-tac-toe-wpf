using System.Windows;
using TicTacToe.Helpers;
using TicTacToe.Views;
using TicTacToe.ViewsModels;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ThemeHelper.ModifyTheme(false);

            SoundHelper.PlayClick();

            DataContext = new MainWindowViewModel();

            Main.Content = new HomeView();
        }
    }
}
