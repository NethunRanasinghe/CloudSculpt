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
        
        // Store navigationService
        ServiceLocator.Register<INavigationService>(navigationService);
        
        // Set Theme
        var savedTheme = ThemeHelper.GetThemeFromSettings();
        ThemeHelper.ChangeCurrentTheme(string.IsNullOrWhiteSpace(savedTheme) ? "Default" : savedTheme);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new Views.Windows.MainMenuWindow();
        }

        base.OnFrameworkInitializationCompleted();
    }
}