using System;
using System.Diagnostics;
using System.Windows.Controls;

using TicTacToe.Enums;
using TicTacToe.Helpers;
using TicTacToe.Views;
using TicTacToe.ViewsModels.Commands;

namespace TicTacToe.ViewsModels
{
    public class SettingsViewModel
    {
        public RelayCommand StartCommand { get; private set; }

        public bool IsMultiplayer { get; set; }
        public string Player1Nickname { get; set; }
        public string Player2Nickname { get; set; }
        public EDifficulty Difficulty { get; set; }

        public SettingsViewModel()
        {
            IsMultiplayer = false;
            Difficulty = EDifficulty.NORMAL;
            Player1Nickname = "Player 1";
            Player2Nickname = "Player 2";

            StartCommand = new RelayCommand(StartGame, CanStart);
        }

        private void StartGame(object parameter)
        {
            try
            {
                SoundHelper.PlayClick();

                UserControl gameUI = new GameView(Difficulty, Player1Nickname, Player2Nickname, IsMultiplayer);
                MainWindowHelper.AddContent(gameUI);
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        private bool CanStart(object parameter)
        {
            return IsMultiplayer
                ? !(string.IsNullOrWhiteSpace(Player1Nickname) || string.IsNullOrWhiteSpace(Player2Nickname))
                : !string.IsNullOrWhiteSpace(Player1Nickname);
        }
    }
}
