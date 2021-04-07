using System.IO;
using System.Media;

namespace TicTacToe.Helpers
{
    public static class SoundHelper
    {
        private static bool _soundOn = true;

        public static void On() => _soundOn = true;
        public static void Off() => _soundOn = false;

        public static void PlayClick()
        {
            if(!_soundOn) return;

            SoundPlayer player = new(Path.Combine(DirectoryHelper.AssetsPath, "click.wav"));
            player.ExecutePlay();
        }

        public static void PlayWins()
        {
            SoundPlayer player = new(Path.Combine(DirectoryHelper.AssetsPath, "wins.wav"));
            player.ExecutePlay();
        }

        public static void PlayDraw()
        {
            SoundPlayer player = new(Path.Combine(DirectoryHelper.AssetsPath, "draw.wav"));
            player.ExecutePlay();
        }

        public static void PlaySelect1()
        {
            SoundPlayer player = new(Path.Combine(DirectoryHelper.AssetsPath, "select_1.wav"));
            player.ExecutePlay();
        }

        public static void PlaySelect2()
        {
            SoundPlayer player = new(Path.Combine(DirectoryHelper.AssetsPath, "select_2.wav"));
            player.ExecutePlay();
        }

        public static void PlaySelect3()
        {
            SoundPlayer player = new(Path.Combine(DirectoryHelper.AssetsPath, "select_3.wav"));
            player.ExecutePlay();
        }

        private static void ExecutePlay(this SoundPlayer player)
        {
            if (!_soundOn) return;

            player.Load();
            player.Play();
        }
    }
}
