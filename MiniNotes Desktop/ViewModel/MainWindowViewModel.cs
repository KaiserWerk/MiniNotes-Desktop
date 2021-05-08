using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Core;
using Core.Model;
using GUI.Message;
using GUI.View;
using KaiserMVVMCore;
using WindowManager = GUI.Helper.WindowManager;

namespace GUI.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly RestService restService;
        private readonly SettingsService settingsService;
        private readonly LocalContentService localContentService;

        private UserSettings settings;

        public string UserMessage { get; set; } = string.Empty;
        public string TextContent { get; set; } = string.Empty;

        public RelayCommand SaveChangesCommand { get; set; }
        public RelayCommand ChangeSettingsCommand { get; set; }

        public MainWindowViewModel(RestService rs, SettingsService ss, LocalContentService lcs)
        {
            this.SaveChangesCommand = new RelayCommand(this.SaveChangesCommandExecute);
            this.ChangeSettingsCommand = new RelayCommand(this.ChangeSettingsCommandExecute);

            Messenger.Register<SettingsHaveChangedMessage>(this.ChangedSettingsHandler);

            this.restService = rs;
            this.settingsService = ss;
            this.localContentService = lcs;

            this.settings = this.settingsService.ReadSettings();
            if (this.settings.SynchronizationEnabled)
            {
                try
                {
                    this.restService
                        .SetRemote(this.settings.RemoteAddress)
                        .SetId(this.settings.Identifier)
                        .SetSecret(this.settings.Secret);

                    this.TextContent = this.restService.GetContent();
                }
                catch (Exception e)
                {
                    this.UserMessage = "Could not fetch remote data!";
                    Task.Run(() =>
                    {
                        Task.Delay(4000);
                        this.UserMessage = string.Empty;
                    });
                    this.TextContent = this.localContentService.GetContent();
                }
            }
            else
            {
                this.TextContent = this.localContentService.GetContent();
            }
            
        }

        private void ChangedSettingsHandler(object obj)
        {
            this.settings = this.settingsService.ReadSettings();
            // duplicate code
            if (this.settings.SynchronizationEnabled)
            {
                this.restService
                    .SetRemote(this.settings.RemoteAddress)
                    .SetId(this.settings.Identifier)
                    .SetSecret(this.settings.Secret);
            }
            // TODO handle encryption
        }

        private void ChangeSettingsCommandExecute()
        {
            WindowManager.OpenSettingsWindow();
        }

        private void SaveChangesCommandExecute()
        {
            this.SaveChanges();
        }

        public void SaveChanges()
        {
            this.localContentService.StoreContent(this.TextContent);

            if (this.settings.SynchronizationEnabled)
            {
                this.restService.StoreContent(this.TextContent);
            }

            this.UserMessage = "Manually saved!";
            Task.Run(() =>
            {
                Task.Delay(4000);
                this.UserMessage = string.Empty;
            });
        }
    }
}
