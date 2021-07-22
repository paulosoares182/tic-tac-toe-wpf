using System.Windows.Media;

using TicTacToe.Helpers;
using TicTacToe.Views;
using TicTacToe.ViewsModels.Commands;

namespace TicTacToe.ViewsModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public RelayCommand MinimizeCommand { get; private set; }
        public RelayCommand MaximizeCommand { get; private set; }
        public RelayCommand CloseCommand { get; private set; }

        private bool darkMode;
        public bool DarkMode
        {
            get
            {
                return darkMode;
            }
            set
            {
                darkMode = value;

                ThemeHelper.ModifyTheme(value);

                TopForegroundColor = value ? Brushes.WhiteSmoke : Brushes.Black;

                NotifyPropertyChanged(nameof(DarkMode));
                NotifyPropertyChanged(nameof(TopForegroundColor));
            }
        }

        public SolidColorBrush TopForegroundColor { get; private set; }

        public MainWindowViewModel()
        {
            MinimizeCommand = new RelayCommand(Minimize, CanExecute);
            MaximizeCommand = new RelayCommand(Maximize, CanExecute);
            CloseCommand = new RelayCommand(Close, CanExecute);

            DarkMode = false;
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
