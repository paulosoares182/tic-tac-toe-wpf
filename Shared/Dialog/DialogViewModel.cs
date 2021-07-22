using MaterialDesignThemes.Wpf;

using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using TicTacToe.Shared.Dialog.Ok;
using TicTacToe.Shared.Dialog.OkOrCancel;
using TicTacToe.ViewsModels;
using TicTacToe.ViewsModels.Commands;

namespace TicTacToe.Shared.Dialog
{
    public class DialogViewModel : ViewModelBase
    {
        public RelayCommand CloseDialogCommand { get; private set; }

        #region Properties
        public EDialogType DialogType { get; private set; }
        public string Title { get; private set; }
        public string Message { get; private set; }
        public MessageBoxResult Result { get; private set; }
        public PackIconKind Icon { get; private set; }
        public SolidColorBrush IconForeground { get; private set; }
        public string AcceptText { get; private set; }
        public string CancelText { get; private set; }
        #endregion

        public DialogViewModel(
            EDialogType dialogType, 
            string title,  string message, 
            PackIconKind icon, SolidColorBrush iconForeground, 
            string acceptText = "Accept", string cancelText = "Cancel")
        {
            CloseDialogCommand = new RelayCommand(CloseDialog, CanExecute);

            DialogType = dialogType;
            Title = title;
            Message = message;
            Icon = icon;
            IconForeground = iconForeground;
            AcceptText = acceptText;
            CancelText = cancelText;
        }

        private void DialogClosingEventHandler(object sender, DialogClosingEventArgs args)
        {
            Window window = Application.Current.Windows.OfType<Window>()
               .Where(w => w.GetType().Name == nameof(DialogView)).LastOrDefault();

            window?.Close();
        }
        public async void OpenDialog()
        {
            UserControl view = DialogType switch
            {
                EDialogType.OK_CANCEL => new OkOrCancelView(),
                EDialogType.YES_NO => new OkOrCancelView(),
                EDialogType.OK => new OkView(),
                _ => null
            };

            if (view is null)
            {
                DialogClosingEventHandler(null, null);
                return;
            }

            view.DataContext = this;

            await DialogHost.Show(view, "CustomDialog", DialogClosingEventHandler);
        }
        private void CloseDialog(object obj)
        {
            Result = DialogType switch
            {
                EDialogType.OK_CANCEL => (bool)obj ? MessageBoxResult.OK : MessageBoxResult.Cancel,
                EDialogType.YES_NO => (bool)obj ? MessageBoxResult.Yes : MessageBoxResult.No,
                EDialogType.OK => (bool)obj ? MessageBoxResult.OK : MessageBoxResult.Cancel,
                _ => MessageBoxResult.Cancel
            };

            DialogHost.Close("CustomDialog");
        }
        private bool CanExecute(object arg) => true;
    }
}