using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CloudSculpt.HelperClasses;
using CloudSculpt.Interfaces;
using CloudSculpt.Services;
using CloudSculpt.Views.Windows;

namespace CloudSculpt;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        
        var navigationService = new NavigationService();

        // Register windows
        navigationService.RegisterWindow("MainMenuWindow", typeof(MainMenuWindow));
        navigationService.RegisterWindow("SettingsWindow", typeof(SettingsWindowV2));
        navigationService.RegisterWindow("ConfigureCloudInfraWindow", typeof(ConfigureCloudInfraWindow));
        navigationService.RegisterWindow("ConfigCloudInfraEditWindow", typeof(ConfigCloudInfraEditWindow));
        navigationService.RegisterWindow("KubeNetworkConfigHomeDefaultWindow", typeof(KubeNetworkConfigHomeDefaultWindow));
        
        // Store navigationService
        ServiceLocator.Register<INavigationService>(navigationService);
    }

    public override async void OnFrameworkInitializationCompleted()
    {
        // Set Theme
        await DatabaseManage.GetApplicationData();
        var savedTheme = DatabaseManage.ApplicationDataContent.Theme;
        await ThemeHelper.ChangeCurrentTheme(string.IsNullOrWhiteSpace(savedTheme) ? "Default" : savedTheme);
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainMenuWindow();
        }

        base.OnFrameworkInitializationCompleted();
    }
}