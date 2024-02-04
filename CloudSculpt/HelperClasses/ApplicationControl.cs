using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using CloudSculpt.Views;
using ProjectSelectionMainView = CloudSculpt.Views.UserControls.ProjectSelectionMainView;

namespace CloudSculpt.HelperClasses;

public static class ApplicationControl
{
    public static void ExitApplication()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopApp)
        {
            desktopApp.Shutdown();
        }
    }

    public static void ToMainMenuViewFromMainMenuWindow(Views.Windows.MainMenuWindow mainMenuWindow)
    {
        ProjectSelectionMainView projectSelectionMainView = new(mainMenuWindow);
        mainMenuWindow.CurrentUserControl = projectSelectionMainView;
    }
}