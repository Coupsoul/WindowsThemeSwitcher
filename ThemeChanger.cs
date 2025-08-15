using Microsoft.Win32;
using static WindowsThemeSwitcher.Config.ThemeTarget;
using static WindowsThemeSwitcher.Config.ThemeType;

namespace WindowsThemeSwitcher
{
    internal static class ThemeChanger
    {
        internal static void ChangeTheme(TargetOfTheme themeTarget, TypeOfTheme themeType)
        {
            string keyPath = "Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize";

            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(keyPath, true))
                {
                    if (key != null)
                    {
                        switch (themeTarget)
                        {
                            case TargetOfTheme.Apps: 
                                key.SetValue("AppsUseLightTheme", (int)themeType);
                                break;
                            case TargetOfTheme.System: 
                                key.SetValue("SystemUsesLightTheme", (int)themeType);
                                break;
                            case TargetOfTheme.Both:
                                key.SetValue("AppsUseLightTheme", (int)themeType);
                                key.SetValue("SystemUsesLightTheme", (int)themeType);
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }
    }
}