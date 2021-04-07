
using TicTacToe.Helpers;
using TicTacToe.Views;
using TicTacToe.ViewsModels.Commands;

namespace TicTacToe.ViewsModels
{
    public class HomeViewModel
    {
        public RelayCommand GotItCommand { get; private set; }

        public HomeViewModel()
        {
            GotItCommand = new RelayCommand(Next, CanExecute);
        }

        private void Next(object parameter)
        {
            MainWindowHelper.AddContent(new SettingsView());
        }

        private bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
