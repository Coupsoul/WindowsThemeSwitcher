using System.Runtime.InteropServices;
using Microsoft.Win32;
using static WindowsThemeSwitcher.ConfigEnumerations;

namespace WindowsThemeSwitcher
{
    internal static class ThemeChanger
    {
        internal const int HWND_BROADCAST = 0xffff;
        internal const int WM_SETTINGCHANGE = 0x001A;

        internal static void ChangeTheme(ThemeTarget themeTarget, ThemeType themeType)
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
                            case ThemeTarget.Apps:
                                key.SetValue("AppsUseLightTheme", (int)themeType);
                                break;
                            case ThemeTarget.System:
                                key.SetValue("SystemUsesLightTheme", (int)themeType);
                                break;
                            case ThemeTarget.Both:
                                key.SetValue("AppsUseLightTheme", (int)themeType);
                                key.SetValue("SystemUsesLightTheme", (int)themeType);
                                break;
                        }
                    }
                }
                NotifyThemeChange();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern IntPtr SendMessageTimeout(IntPtr hWnd,
            uint Msg,
            UIntPtr wParam,
            string lParam,
            uint fuFlags,
            uint uTimeout,
            out UIntPtr lpdwResult);

        internal static void NotifyThemeChange()
        {
            SendMessageTimeout(new IntPtr(HWND_BROADCAST), (uint)WM_SETTINGCHANGE, UIntPtr.Zero, "ImmersiveColorSet", 0, 500, out _);

            int errorCode = Marshal.GetLastWin32Error();
            if (errorCode != 0)
                System.Diagnostics.Debug.WriteLine($"Ошибка вызова Win32 API: {errorCode}");
        }
    }
}