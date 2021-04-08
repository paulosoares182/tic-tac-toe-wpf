using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using TicTacToe.Business;
using TicTacToe.Enums;
using TicTacToe.Helpers;
using TicTacToe.Models;
using TicTacToe.Views;
using TicTacToe.ViewsModels.Commands;

namespace TicTacToe.ViewsModels
{
    public class SettingsViewModel
    {
        public RelayCommand StartCommand { get; private set; }

        public string Player1Nickname { get; set; }
        public string Player2Nickname { get; set; }
        public EDifficulty Difficulty { get; set; }

        public SettingsViewModel()
        {
            Difficulty = EDifficulty.NORMAL;
            Player1Nickname = string.Empty;
            Player2Nickname = string.Empty;

            StartCommand = new RelayCommand(StartGame, CanStart);
        }

        private void StartGame(object parameter)
        {
            try
            {
                SoundHelper.PlayClick();

                UserControl gameUI = new GameView(Difficulty, Player1Nickname, Player2Nickname);
                MainWindowHelper.AddContent(gameUI);
            }
            catch
            {
                
            }
        }

        private bool CanStart(object parameter)
        {
            return !(String.IsNullOrWhiteSpace(Player1Nickname) || String.IsNullOrWhiteSpace(Player2Nickname));
        }
    }
}
