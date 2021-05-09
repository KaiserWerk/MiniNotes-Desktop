using GUI.View;

namespace GUI.Helper
{
    public static class WindowManager
    {
        private static SettingsWindow settingsWindow;

        public static void OpenSettingsWindow()
        {
            settingsWindow = new SettingsWindow();
            settingsWindow.Show();
        }

        public static void CloseSettingsWindow()
        {
            settingsWindow?.Close();
            settingsWindow = null;
        }
    }
}