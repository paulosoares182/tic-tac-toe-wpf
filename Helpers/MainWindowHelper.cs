using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TicTacToe.Helpers
{
    public static class MainWindowHelper
    {
        private static readonly MainWindow app = Application.Current.MainWindow as MainWindow;
        public static void AddContent(UserControl content)
        {
            app.Main.Dispatcher.Invoke(new Action(() => app.Main.Content = content));
        }

        public static void RemoveContent()
        {
            app.Main.Dispatcher.Invoke(new Action(() => app.Main.Content = null));
        }

        public static void FocusContent()
        {
            app.Main.Dispatcher.Invoke(new Action(() => app.Main.Focus()));
        }
    }
}
