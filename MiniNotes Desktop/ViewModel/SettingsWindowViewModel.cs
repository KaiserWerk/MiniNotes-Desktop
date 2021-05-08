using Core;
using Core.Model;
using KaiserMVVMCore;
using WindowManager = GUI.Helper.WindowManager;

namespace GUI.ViewModel
{
    public class SettingsWindowViewModel : ViewModelBase
    {
        private SettingsService settingsService;

        public UserSettings Settings { get; set; }

        public RelayCommand SaveSettingsCommand { get; set; }

        public SettingsWindowViewModel(SettingsService ss)
        {
            this.SaveSettingsCommand = new RelayCommand(this.SaveSettingsCommandExecute);
            this.settingsService = ss;

            this.Settings = this.settingsService.ReadSettings();
        }

        private void SaveSettingsCommandExecute()
        {
            this.settingsService.StoreSettings(this.Settings);
            WindowManager.CloseSettingsWindow();
        }
    }
}
