using TicTacToe.Helpers;
using TicTacToe.Views;
using TicTacToe.ViewsModels.Commands;

namespace TicTacToe.ViewsModels
{
    public class MainWindowViewModel
    {
        public RelayCommand MinimizeCommand { get; private set; }
        public RelayCommand MaximizeCommand { get; private set; }
        public RelayCommand CloseCommand { get; private set; }

        public MainWindowViewModel()
        {
            MinimizeCommand = new RelayCommand(Minimize, CanExecute);
            MaximizeCommand = new RelayCommand(Maximize, CanExecute);
            CloseCommand = new RelayCommand(Close, CanExecute);
        }

        private void Minimize(object parameter)
        {
            MainWindowHelper.Minimize();
        }

        private void Maximize(object parameter)
        {
            MainWindowHelper.Maximize();
        }

        private void Close(object parameter)
        {
            MainWindowHelper.Close();
        }

        private bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
