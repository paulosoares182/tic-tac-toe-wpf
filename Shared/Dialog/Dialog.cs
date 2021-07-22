using MaterialDesignThemes.Wpf;

using System.Windows;
using System.Windows.Media;

namespace TicTacToe.Shared.Dialog
{
    public static class Dialog
    {
        public static MessageBoxResult YesOrNo(string title, string message)
        {
            return ShowDialog(EDialogType.YES_NO, title, message, PackIconKind.HelpCircle, Brushes.CadetBlue, acceptText:"Sim", cancelText:"Não");
        }
        public static MessageBoxResult OkOrCancel(string title, string message)
        {
            return ShowDialog(EDialogType.OK_CANCEL, title, message, PackIconKind.MessageText, Brushes.CadetBlue, acceptText: "Ok", cancelText: "Cancelar");
        }
        public static MessageBoxResult Wins(string title, string message)
        {
            return ShowDialog(EDialogType.OK, title, message, PackIconKind.Trophy, Brushes.DarkGoldenrod);
        }
        public static MessageBoxResult Draw(string title, string message)
        {
            return ShowDialog(EDialogType.OK, title, message, PackIconKind.Handshake, Brushes.DarkGoldenrod);
        }
        public static MessageBoxResult Error(string title, string message)
        {
            return ShowDialog(EDialogType.OK, title, message, PackIconKind.Error, Brushes.IndianRed);
        }

        private static MessageBoxResult ShowDialog(EDialogType dialogType, string title, string message, 
            PackIconKind icon, SolidColorBrush iconForeground, string acceptText = null, string cancelText = null)
        {
            var window = new DialogView(dialogType, title, message, icon, iconForeground, acceptText, cancelText);
            window.ShowDialog();

            return (window.DataContext as DialogViewModel).Result;
        }
    }
}