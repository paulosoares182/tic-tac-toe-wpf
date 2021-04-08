using MaterialDesignColors;
using MaterialDesignColors.ColorManipulation;
using MaterialDesignThemes.Wpf;
using System.Windows.Media;

namespace TicTacToe.Helpers
{
    public static class ThemeHelper
    {
        public static void ModifyTheme(bool isDarkTheme)
        {
            var paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();

            theme.SetBaseTheme(isDarkTheme ? Theme.Dark : Theme.Light);

            //if (isDarkTheme)
            //{
            //    theme.SetBaseTheme(Theme.Dark);
            //    paletteHelper.ChangePrimaryColor(Color.FromRgb(100, 0, 0));
            //}
            //else
            //{
            //    theme.SetBaseTheme(Theme.Light);
            //    paletteHelper.ChangePrimaryColor(Color.FromRgb(0, 0, 100));
            //}

            paletteHelper.SetTheme(theme);
        }

        public static void ChangePrimaryColor(this PaletteHelper paletteHelper, Color color)
        {
            ITheme theme = paletteHelper.GetTheme();

            theme.PrimaryLight = new ColorPair(color.Lighten());
            theme.PrimaryMid = new ColorPair(color);
            theme.PrimaryDark = new ColorPair(color.Darken());

            paletteHelper.SetTheme(theme);
        }

        public static void ChangeSecondaryColor(this PaletteHelper paletteHelper, Color color)
        {
            ITheme theme = paletteHelper.GetTheme();

            theme.SecondaryLight = new ColorPair(color.Lighten());
            theme.SecondaryMid = new ColorPair(color);
            theme.SecondaryDark = new ColorPair(color.Darken());

            paletteHelper.SetTheme(theme);
        }
    }
}
