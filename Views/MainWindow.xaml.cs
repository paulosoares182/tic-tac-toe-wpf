using System.Windows;

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

            DataContext = new MainWindowViewModel();
            Main.Content = new HomeView();
        }
    }
}
