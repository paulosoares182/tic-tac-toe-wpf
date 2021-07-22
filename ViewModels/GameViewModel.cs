using MaterialDesignThemes.Wpf;

using System;
using System.Windows;

using TicTacToe.Business;
using TicTacToe.Enums;
using TicTacToe.Helpers;
using TicTacToe.Models;
using TicTacToe.Shared.Dialog;
using TicTacToe.ViewsModels.Commands;

namespace TicTacToe.ViewsModels
{
    public class GameViewModel : ViewModelBase
    {
        private bool IsRunning = false;
        public GameBLL Bll { get; set; }
        public RelayCommand SetFieldCommand { get; private set; }
        public Array Board
        {
            get
            {
                PackIconKind[] value = new PackIconKind[9];

                var circle = PackIconKind.CircleOutline;
                var x = PackIconKind.CloseOutline;
                var empty = PackIconKind.DragHorizontalVariant;

                int index = 0;
                foreach (var field in Bll.Game.Board)
                {
                    value[index] = field == EPiece.EMPTY ? empty : (field == EPiece.CIRCLE ? circle : x);
                    index++;
                }

                return value;
            }
        }
        
        public GameViewModel(EDifficulty difficulty, string player1Nickname, string player2Nickname)
        {
            IninitalizeCommands();

            var player1 = new Player(player1Nickname, EPiece.X, false);
            var player2 = new Player(player2Nickname, EPiece.CIRCLE, false);

            var game = new Game(difficulty, new Player[] { player1, player2 });

            Bll = new GameBLL(game);

            try
            {
                Bll.Start();
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
        }
        private bool CanSetField(object parameter) => !IsRunning;
        public void SetField(object parameter)
        {
            IsRunning = true;
            
            string[] tag = (parameter as string).Split(','); //number,number

            int row = int.Parse(tag[0]);
            int column = int.Parse(tag[1]);

            var result = Bll.SetFieldAsync(new int[2] { row, column }, ShowMessageWins, ShowMessagDraw);

            SoundHelper.PlaySelect1();

            //Liberar Board após setar campo, porém antes do método inteiro terminar
            NotifyPropertyChanged(nameof(Board));

            //Liberar Board após o método inteiro terminar
            _ = result.ContinueWith(t =>
            {
                IsRunning = false;
                NotifyPropertyChanged(nameof(Board));
                MainWindowHelper.FocusContent();
            });
        }
        #endregion

        private void ShowMessageWins()
        {
            SoundHelper.PlayWins();
            Dialog.Wins("Vitória!", $"Jogador {Bll.CurrentPlayer.Nickname} venceu a partida!");
        }
        private void ShowMessagDraw()
        {
            SoundHelper.PlayDraw();
            Dialog.Draw("Empate", "Xi... deu velha!");
        }
    }
}
