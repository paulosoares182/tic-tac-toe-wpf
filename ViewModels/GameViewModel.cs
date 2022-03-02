using MaterialDesignThemes.Wpf;

using System;
using System.Threading.Tasks;
using System.Windows;

using TicTacToe.Business;
using TicTacToe.Enums;
using TicTacToe.Helpers;
using TicTacToe.Models;
using TicTacToe.Shared.Dialog;
using TicTacToe.Views;
using TicTacToe.ViewsModels.Commands;

namespace TicTacToe.ViewsModels
{
    public class GameViewModel : ViewModelBase
    {
        private bool IsRunning;
        public GameRules Controller { get; set; }
        public RelayCommand SetFieldCommand { get; private set; }
        public RelayCommand StopCommand { get; private set; }

        public Array Board
        {
            get
            {
                PackIconKind[] value = new PackIconKind[9];

                var circle = PackIconKind.CircleOutline;
                var x = PackIconKind.CloseOutline;
                var empty = PackIconKind.DragHorizontalVariant;

                int index = 0;
                foreach (var field in Controller.Game.Board)
                {
                    value[index] = field == EPiece.EMPTY ? empty : (field == EPiece.CIRCLE ? circle : x);
                    index++;
                }

                return value;
            }
        }

        public GameViewModel(EDifficulty difficulty, string player1Nickname, string player2Nickname, bool isMultiplayer)
        {
            IninitalizeCommands();

            var player1 = new PlayerModel(player1Nickname, EPiece.X, false);
            var player2 = new PlayerModel(player2Nickname, EPiece.CIRCLE, !isMultiplayer);

            var game = new GameModel(difficulty, new PlayerModel[] { player1, player2 }, isMultiplayer);

            Controller = new GameRules(game);

            try
            {
                Controller.Start();
            }
            catch (Exception e)
            {
                Dialog.Error("Inicio de partida", e.Message);
                throw;
            }
        }

        #region Commands
        private void IninitalizeCommands()
        {
            SetFieldCommand = new RelayCommand(SetField, CanSetField);
            StopCommand = new RelayCommand(Stop, CanSetField);
        }
        private bool CanSetField(object parameter) => !IsRunning;
        public void SetField(object parameter)
        {
            int[] arrayPosition = Array.ConvertAll(parameter.ToString().Split(','), int.Parse);
            Tuple<int, int> position = new(arrayPosition[0], arrayPosition[1]);

            if (!Controller.CanSetField(position))
                return;

            IsRunning = true;

            SoundHelper.PlaySelect();

            _ = Task.Run(async () =>
            {
                Controller.SetField(position, ShowMessageWins, ShowMessagDraw);

                NotifyPropertyChanged(nameof(Board));

                if (Controller.CurrentPlayer.IsCPU)
                {
                    await Task.Delay(800);

                    SoundHelper.PlaySelect();
                    Controller.CPUSetField(ShowMessageWins, ShowMessagDraw);
                }

                ReleaseBoard();
            });
        }
        public void Stop(object parameter)
        {
            MainWindowHelper.AddContent(new SettingsView());
        }
        #endregion

        private void ShowMessageWins()
        {
            ReleaseBoard();

            Application.Current.Dispatcher.Invoke(delegate
            {
                SoundHelper.PlayWins();
                Dialog.Wins("Vitória!", $"{Controller.CurrentPlayer.Nickname} venceu a partida!");
            });
        }
        private void ShowMessagDraw()
        {
            ReleaseBoard();

            Application.Current.Dispatcher.Invoke(delegate
            {
                SoundHelper.PlayDraw();
                Dialog.Draw("Empate", "Xi... deu velha!");
            });
        }
        private void ReleaseBoard()
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                IsRunning = false;
                NotifyPropertyChanged(nameof(Controller));
                NotifyPropertyChanged(nameof(Board));
            });

            MainWindowHelper.FocusContent();
        }
    }
}
