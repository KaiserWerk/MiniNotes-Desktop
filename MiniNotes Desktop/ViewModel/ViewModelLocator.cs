using Core;
using KaiserMVVMCore;

namespace GUI.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            IocContainer.Register<MainWindowViewModel>(RegisterMode.Singleton, new RestService(), new SettingsService(), new LocalContentService());
            IocContainer.Register<SettingsWindowViewModel>(RegisterMode.Singleton, new SettingsService());
        }

        public MainWindowViewModel MainWindowViewModelInstance => IocContainer.GetInstance<MainWindowViewModel>();
        public SettingsWindowViewModel SettingsWindowViewModelInstance => IocContainer.GetInstance<SettingsWindowViewModel>();
    }
}
