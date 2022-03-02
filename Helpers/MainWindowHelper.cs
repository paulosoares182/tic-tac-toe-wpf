
using System;
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
            //app.Main.Dispatcher.Invoke(new Action(() => app.Main.Focus()));

            Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() =>
            {
                app.Main.Focus();
            }));
        }

        public static void Minimize()
        {
            app.Main.Dispatcher.Invoke(new Action(() => app.WindowState = WindowState.Minimized));
        }

        public static void Maximize()
        {
            app.Main.Dispatcher.Invoke(new Action(() =>
            {
                app.WindowState = app.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
            }));
        }

        public static void Close()
        {
            app.Main.Dispatcher.Invoke(new Action(() => app.Close()));
        }
    }
}
