using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using TicTacToe.Enums;
using TicTacToe.Models;
using TicTacToe.ViewsModels;

namespace TicTacToe.Business
{
    public class GameRules : ViewModelBase
    {
        public GameModel Game { get; set; }
        public PlayerModel CurrentPlayer { get; set; }
        public int CountPlays { get; private set; }
        public Tuple<int, int> LastPlayPosition { get; private set; }

        public GameRules(GameModel game)
        {
            CountPlays = 0;
            Game = game;
        }

        public void CPUSetField(Action actionWins, Action actionDraw)
        {
            Tuple<int, int> position = Game.Difficulty switch
            {
                EDifficulty.EASY => CalculateCPUPlayEasy(),
                EDifficulty.NORMAL => CalculateCPUPlayNormal(),
                EDifficulty.HARD => CalculateCPUPlayerHard(),
                _ => GetEmptyFieldRandomly()
            };

            Debug.WriteLine($"\n\n-------- POS: {position.Item1},{position.Item2}");
            LastPlayPosition = position;

            SetField(position, actionWins, actionDraw);
        }
        public void SetField(Tuple<int, int> position, Action actionWins, Action actionDraw)
        {
            if (!CanSetField(position))
                return;

            Game.Board[position.Item1, position.Item2] = CurrentPlayer.Piece;

            if (IsFinished(out bool draw))
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
                CountPlays++;
                ChangePlayer();
            }

            LastPlayPosition = position;
            NotifyPropertyChanged(nameof(Game));

        }

        public void Start()
        {
            if (!CanStart())
            {
                throw new Exception("Invalid game.");
            }

            CurrentPlayer = Game.Players[0];
        }

        public void Restart()
        {
            Game.ClearMatches();

            foreach (var player in Game.Players)
            {
                player.ClearWins();
            }
        }

        public void ClearBoard()
        {
            Game.InitBoard();
            CurrentPlayer = Game.Players[0];
            CountPlays = 0;
        }

        private bool IsFinished(out bool draw)
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
            if (Game.Board.Length != 9)
                return false;

            //Check if board is empty
            foreach (EPiece piece in Game.Board)
            {
                if (piece != EPiece.EMPTY)
                    return false;
            }

            //Check if number of players is two (can be CPU)
            if (Game.Players.Length != 2)
                return false;

            PlayerModel player1 = Game.Players[0];
            PlayerModel player2 = Game.Players[1];

            //Check if nickname is empty
            if (string.IsNullOrWhiteSpace(player1.Nickname))
                return false;

            if (string.IsNullOrWhiteSpace(player2.Nickname) && !player2.IsCPU)
                return false;

            //Check if the nickname is the same
            if (player1.Nickname == player2.Nickname)
                return false;

            //Check if the piece is the same
            if (player1.Piece == player2.Piece)
                return false;

            return true;
        }

        public bool CanSetField(Tuple<int, int> position)
        {
            return Game.Board[position.Item1, position.Item2] == EPiece.EMPTY && Game.Board[position.Item1, position.Item2] != CurrentPlayer.Piece;
        }

        private Tuple<int, int> CalculateCPUPlayEasy()
        {
            return GetEmptyFieldRandomly();
        }

        private Tuple<int, int> CalculateCPUPlayNormal()
        {
            Tuple<int, int> position = GetPossibleVictory();

            if (position is null)
            {
                position = GetPossibleVictory(inverse: true);

                if (position is null)
                {
                    position = GetEmptyFieldRandomly();
                }
            }

            return position;
        }

        private Tuple<int, int> CalculateCPUPlayerHard()
        {
            Tuple<int, int> position = GetPossibleVictory();

            if (position is null)
            {
                position = GetPossibleVictory(inverse: true);

                if (position is null)
                {

                    if (CountPlays == 1) //First Play
                    {
                        List<Tuple<int, int>> possibleFields = new();

                        /*
                         * GET THE BEST DEFENSIVE PLAY
                         * 
                         * If the first play is in the center, set in the corners
                            If the first play is int the central corners, set in the corner or center
                            If the first play is in the corner, set in the center

                            Corner	        | CenterCorner	| Corner
                            CenterCorner	| Center	    | CenterCorner
                            Corner	        | CenterCorner	| corner
                         */

                        //Corners
                        if (LastPlayPosition.Item1 == 0 && LastPlayPosition.Item2 == 0 || LastPlayPosition.Item1 == 0 && LastPlayPosition.Item2 == 2
                            ||
                            LastPlayPosition.Item1 == 2 && LastPlayPosition.Item2 == 0 || LastPlayPosition.Item1 == 2 && LastPlayPosition.Item2 == 2)
                        {
                            //Center
                            possibleFields.Add(new Tuple<int, int>(1, 1));
                        }

                        //CenterCorners
                        else if (LastPlayPosition.Item1 == 0 && LastPlayPosition.Item2 == 1
                            ||
                            LastPlayPosition.Item1 == 1 && LastPlayPosition.Item2 == 0 || LastPlayPosition.Item1 == 1 && LastPlayPosition.Item2 == 2
                            ||
                            LastPlayPosition.Item1 == 2 && LastPlayPosition.Item2 == 1)
                        {
                            //Center
                            possibleFields.Add(new Tuple<int, int>(1, 1));

                            //Corners
                            possibleFields.Add(new Tuple<int, int>(0, 0));
                            possibleFields.Add(new Tuple<int, int>(0, 2));
                            possibleFields.Add(new Tuple<int, int>(2, 0));
                            possibleFields.Add(new Tuple<int, int>(2, 2));
                        }

                        //Center
                        else if (LastPlayPosition.Item1 == 1 && LastPlayPosition.Item2 == 1)
                        {
                            //Corners
                            possibleFields.Add(new Tuple<int, int>(0, 0));
                            possibleFields.Add(new Tuple<int, int>(0, 2));
                            possibleFields.Add(new Tuple<int, int>(2, 0));
                            possibleFields.Add(new Tuple<int, int>(2, 2));
                        }

                        //Get random field
                        position = possibleFields[new Random().Next(0, possibleFields.Count)];
                    }
                    else
                    {
                        position = GetEmptyFieldRandomly();
                    }
                }
            }

            return position;
        }

        private Tuple<int, int> GetEmptyFieldRandomly()
        {
            List<Tuple<int, int>> emptyFields = new();

            //Getting all empty fields
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (Game.Board[row, col] == EPiece.EMPTY)
                    {
                        emptyFields.Add(new Tuple<int, int>(row, col));
                    }
                }
            }

            //Select random field
            Tuple<int, int> randomPosition = emptyFields[new Random().Next(0, emptyFields.Count)];

            return randomPosition;
        }

        private Tuple<int, int> GetPossibleVictory(bool inverse = false)
        {
            EPiece piece = inverse ? (CurrentPlayer.Piece == EPiece.CIRCLE ? EPiece.X : EPiece.CIRCLE) : CurrentPlayer.Piece;
            //Checks possible horizontal victory
            for (int i = 0; i < 3; i++)
            {
                List<EPiece> row = new() { Game.Board[i, 0], Game.Board[i, 1], Game.Board[i, 2] };

                int countCurrentPlayerPieces = row.Count(p => p == piece);

                if (countCurrentPlayerPieces == 2 && row.Contains(EPiece.EMPTY))
                {
                    int index = row.IndexOf(row.FirstOrDefault(p => p == EPiece.EMPTY));

                    return new Tuple<int, int>(i, index);
                }
            }

            //Checks possible vertical victory
            for (int i = 0; i < 3; i++)
            {
                List<EPiece> col = new() { Game.Board[0, i], Game.Board[1, i], Game.Board[2, i] };

                int countPlayerPieces = col.Count(p => p == piece);

                if (countPlayerPieces == 2 && col.Contains(EPiece.EMPTY))
                {
                    int index = col.IndexOf(col.FirstOrDefault(p => p == EPiece.EMPTY));

                    return new Tuple<int, int>(index, i);
                }
            }

            //Checks possible diagonal victory
            {
                List<EPiece> diagonalTopLeft = new() { Game.Board[0, 0], Game.Board[1, 1], Game.Board[2, 2] };
                List<EPiece> diagonalTopRight = new() { Game.Board[0, 2], Game.Board[1, 1], Game.Board[2, 0] };

                int countPlayerPieces = diagonalTopLeft.Count(p => p == piece);

                if (countPlayerPieces == 2 && diagonalTopLeft.Contains(EPiece.EMPTY))
                {
                    int index = diagonalTopLeft.IndexOf(diagonalTopLeft.FirstOrDefault(p => p == EPiece.EMPTY));

                    return new Tuple<int, int>(index, index);
                }

                countPlayerPieces = diagonalTopRight.Count(p => p == piece);

                if (countPlayerPieces == 2 && diagonalTopRight.Contains(EPiece.EMPTY))
                {
                    int index1 = diagonalTopRight.IndexOf(diagonalTopRight.FirstOrDefault(p => p == EPiece.EMPTY));
                    diagonalTopRight.Reverse();
                    int index2 = diagonalTopRight.IndexOf(diagonalTopRight.FirstOrDefault(p => p == EPiece.EMPTY));
                    return new Tuple<int, int>(index1, index2);
                }
            }

            return null;
        }
    }
}
