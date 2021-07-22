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

            if (isDarkTheme)
            {
                theme.SetBaseTheme(Theme.Dark);
                paletteHelper.ChangeThemeColors(Color.FromRgb(0, 255, 255), Color.FromRgb(0, 255, 255));
            }
            else
            {
                theme.SetBaseTheme(Theme.Light);
                paletteHelper.ChangeThemeColors(Color.FromRgb(0, 255, 255), Color.FromRgb(0, 255, 255));
            }
        }

        private static void ChangeThemeColors(this PaletteHelper paletteHelper, Color primaryColor, Color secondaryColor)
        {
            ITheme theme = paletteHelper.GetTheme();

            theme.PrimaryLight = new ColorPair(primaryColor.Lighten());
            theme.PrimaryMid = new ColorPair(primaryColor);
            theme.PrimaryDark = new ColorPair(primaryColor.Darken());

            theme.SecondaryLight = new ColorPair(secondaryColor.Lighten());
            theme.SecondaryMid = new ColorPair(secondaryColor);
            theme.SecondaryDark = new ColorPair(secondaryColor.Darken());

            paletteHelper.SetTheme(theme);
        }
    }
}
