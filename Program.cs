using WindowsThemeSwitcher;
using static WindowsThemeSwitcher.ConfigEnumerations;

internal class Program
{
    internal static void Main(string[] args)
    {
        ThemeType themeToSet;
        if (DateTime.Now.Hour >= 8 && DateTime.Now.Hour < 22)
            themeToSet = ThemeType.Light;
        else
            themeToSet = ThemeType.Dark;

        ThemeTarget targetToSet = ThemeTarget.Apps;
        if (args.Length > 0)
        {
            switch (args[0].ToLower())
            {
                case "system": targetToSet = ThemeTarget.System; break;
                case "both": targetToSet = ThemeTarget.Both; break;
            }
        }

        ThemeChanger.ChangeTheme(targetToSet, themeToSet);
    }
}