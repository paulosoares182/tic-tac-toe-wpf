using System;
using System.Threading.Tasks;
using System.Windows;

using TicTacToe.Enums;
using TicTacToe.Models;
using TicTacToe.ViewsModels;

namespace TicTacToe.Business
{
    public class GameBLL : ViewModelBase
    {
        private Game Init { get; set; }
        public Game Game { get; set; }
        public Player CurrentPlayer { get; set; }
        public GameBLL(Game game)
        {
            Init = game;
        }

        public async Task SetFieldAsync(int[] position, Action actionWins, Action actionDraw)
        {
            if (Game.Board[position[0], position[1]] != EPiece.EMPTY || Game.Board[position[0], position[1]] == CurrentPlayer.Piece)
                return;

            await Task.Run(() => Game.Board[position[0], position[1]] = CurrentPlayer.Piece);

            if (IsFinish(out bool draw))
            {
                if (draw)
                {
                    Game.SetMatch();
                    actionDraw.Invoke();
                }
                else
                {
                    Game.SetMatch();
                    CurrentPlayer.SetWins();
                    actionWins.Invoke();
                }

                ClearBoard();
            }
            else
            {
                ChangePlayer();
            }

            NotifyPropertyChanged(nameof(Game));

        }

        public void Start()
        {
            if (!CanStart())
            {
                throw new Exception("Invalid game.");
            }
            
            Game = Init;
            CurrentPlayer = Game.Players[0];
        }

        public void Restart()
        {
            //this.Game = Init;

            Game.ClearMatches();

            foreach (var player in Game.Players)
            {
                player.ClearWins();
            }
        }

        public void Stop()
        {

        }

        public void ClearBoard()
        {
            Game.InitBoard();
        }

        private bool IsFinish(out bool draw)
        {
            draw = false;

            //Check Horizontal
            if (Game.Board[0, 0] == Game.Board[0, 1] && Game.Board[0, 0] == Game.Board[0, 2] && Game.Board[0, 0] != EPiece.EMPTY)
                return true;
            if (Game.Board[1, 0] == Game.Board[1, 1] && Game.Board[1, 0] == Game.Board[1, 2] && Game.Board[1, 0] != EPiece.EMPTY)
                return true;
            if (Game.Board[2, 0] == Game.Board[2, 1] && Game.Board[2, 0] == Game.Board[2, 2] && Game.Board[2, 0] != EPiece.EMPTY)
                return true;

            //Check Vertical
            if (Game.Board[0, 0] == Game.Board[1, 0] && Game.Board[0, 0] == Game.Board[2, 0] && Game.Board[0, 0] != EPiece.EMPTY)
                return true;
            if (Game.Board[0, 1] == Game.Board[1, 1] && Game.Board[0, 1] == Game.Board[2, 1] && Game.Board[0, 1] != EPiece.EMPTY)
                return true;
            if (Game.Board[0, 2] == Game.Board[1, 2] && Game.Board[0, 2] == Game.Board[2, 2] && Game.Board[0, 2] != EPiece.EMPTY)
                return true;

            //Check Diagonal
            if (Game.Board[0, 0] == Game.Board[1, 1] && Game.Board[0, 0] == Game.Board[2, 2] && Game.Board[0, 0] != EPiece.EMPTY)
                return true;
            if (Game.Board[0, 2] == Game.Board[1, 1] && Game.Board[0, 2] == Game.Board[2, 0] && Game.Board[0, 2] != EPiece.EMPTY)
                return true;

            int countEmpty = 0;
            foreach (var field in Game.Board)
            {
                countEmpty += field == EPiece.EMPTY ? 1 : 0;
            }

            if (countEmpty == 0)
            {
                draw = true;
                return draw;
            }

            return false;
        }

        private void ChangePlayer()
        {
            int index = Array.IndexOf(Game.Players, CurrentPlayer);
            CurrentPlayer = index == 0 ? Game.Players[1] : Game.Players[0];

            NotifyPropertyChanged(nameof(CurrentPlayer));
        }

        private bool CanStart()
        {
            //Check if board was initialized
            if (Init.Board.Length != 9)
                return false;

            //Check if board is empty
            foreach (EPiece piece in Init.Board)
            {
                if (piece != EPiece.EMPTY)
                    return false;
            }

            //Check if number of players is two (can be CPU)
            if (Init.Players.Length != 2)
                return false;

            Player player1 = Init.Players[0];
            Player player2 = Init.Players[1];

            //Check if nickname is empty
            if (String.IsNullOrWhiteSpace(player1.Nickname))
                return false;

            if (String.IsNullOrWhiteSpace(player2.Nickname))
                return false;

            //Check that the nickname is the same
            if (player1.Nickname == player2.Nickname)
                return false;

            //Check that the piece is the same
            if(player1.Piece == player2.Piece)
                return false;

            return true;
        }
    }
}
