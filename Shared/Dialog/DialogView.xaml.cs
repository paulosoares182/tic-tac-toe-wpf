using MaterialDesignThemes.Wpf;

using System.Windows;
using System.Windows.Media;

namespace TicTacToe.Shared.Dialog
{
    /// <summary>
    /// Interaction logic for DialogView.xaml
    /// </summary>
    public partial class DialogView : Window
    {
        public DialogView(EDialogType dialogType, string title, string message, PackIconKind icon, SolidColorBrush iconForeground, string acceptText = null, string cancelText = null)
        {
            InitializeComponent();
            DataContext = new DialogViewModel(dialogType, title, message, icon, iconForeground, acceptText, cancelText);
        }

        private void DialogHost_Loaded(object sender, RoutedEventArgs e)
        {
            ((DialogViewModel)DataContext).OpenDialog();
        }
    }
}
