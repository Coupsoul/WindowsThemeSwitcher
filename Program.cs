using WindowsThemeSwitcher;
using static WindowsThemeSwitcher.Config.ThemeTarget;
using static WindowsThemeSwitcher.Config.ThemeType;

class Program
{
    public static void Main(string[] args)
    {
        TypeOfTheme themeToSet;
        if (DateTime.Now.Hour >= 8 && DateTime.Now.Hour < 22)
            themeToSet = TypeOfTheme.Light;
        else
            themeToSet = TypeOfTheme.Dark;

        TargetOfTheme targetToSet = TargetOfTheme.Apps;
        if (args.Length > 0)
        {
            switch (args[0].ToLower())
            {
                case "system": targetToSet = TargetOfTheme.System; break;
                case "both": targetToSet = TargetOfTheme.Both; break;
            }
        }

        ThemeChanger.ChangeTheme(targetToSet, themeToSet);
    }
}