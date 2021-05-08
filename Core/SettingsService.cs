using Core.Model;
using System;
using System.IO;
using System.Text.Json;

namespace Core
{
    public class SettingsService
    {
        private readonly string settingsPath;
        private readonly string settingsFile;

        public SettingsService()
        {
            this.settingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MiniNotes");
            this.settingsFile = Path.Combine(settingsPath, "settings.json");
        }

        public UserSettings ReadSettings()
        {
            if (!File.Exists(this.settingsFile))
                return new UserSettings();

            return JsonSerializer.Deserialize<UserSettings>(File.ReadAllText(this.settingsFile));
        }

        public void StoreSettings(UserSettings settings)
        {
            if (!Directory.Exists(this.settingsPath))
                Directory.CreateDirectory(this.settingsPath);

            File.WriteAllText(this.settingsFile, JsonSerializer.Serialize(settings, new () { WriteIndented = true }));
        }
    }
}
