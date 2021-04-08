using System.IO;
using System.Media;
using System.Threading.Tasks;

namespace TicTacToe.Helpers
{
    public static class SoundHelper
    {
        private static bool _soundOn = true;
        private static SoundPlayer _player = new();

        public static void On() => _soundOn = true;
        public static void Off() => _soundOn = false;

        public static async void PlayClick()
        {
            await Task.Run(() =>
            {
                _player.SoundLocation = Path.Combine(DirectoryHelper.AssetsPath, "click.wav");
                _player.ExecutePlay();
            });
        }

        public static async void PlayWins()
        {
            await Task.Run(() =>
            {
                _player.SoundLocation = Path.Combine(DirectoryHelper.AssetsPath, "wins.wav");
                _player.ExecutePlay();
            });
        }

        public static async void PlayDraw()
        {
            await Task.Run(() =>
            {
                _player.SoundLocation = Path.Combine(DirectoryHelper.AssetsPath, "draw.wav");
                _player.ExecutePlay();
            });
        }

        public static async void PlaySelect1()
        {
            await Task.Run(() =>
            {
                _player.SoundLocation = Path.Combine(DirectoryHelper.AssetsPath, "select_1.wav");
                _player.ExecutePlay();
            });
        }

        public static async void PlaySelect2()
        {
            await Task.Run(() =>
            {
                _player.SoundLocation = Path.Combine(DirectoryHelper.AssetsPath, "select_2.wav");
                _player.ExecutePlay();
            });
        }

        public static async void PlaySelect3()
        {
            await Task.Run(() =>
            {
                _player.SoundLocation = Path.Combine(DirectoryHelper.AssetsPath, "select_3.wav");
                _player.ExecutePlay();
            });
        }

        private static void ExecutePlay(this SoundPlayer player)
        {
            if (!_soundOn) return;

            player.Stop();
            player.LoadAsync();
            player.PlaySync();
        }
    }
}
