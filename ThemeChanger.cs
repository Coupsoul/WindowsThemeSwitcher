using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using WindowsThemeSwitcher.Config;

namespace WindowsThemeSwitcher
{
    internal static class ThemeChanger
    {
        internal static void ChangeTheme(ThemeTarget.TargetOfTheme themeTarget, ThemeType.TypeOfTheme themeType)
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
                            case ThemeTarget.TargetOfTheme.Apps: key.SetValue("AppsUseLightTheme", (int)themeType);
                                break;
                            case ThemeTarget.TargetOfTheme.System: key.SetValue("SystemUsesLightTheme", (int)themeType);
                                break;
                            case ThemeTarget.TargetOfTheme.Both:
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
