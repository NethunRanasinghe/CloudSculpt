using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using CloudSculpt.Views;

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

    public static void ToMainMenuViewFromMainMenuWindow(MainMenuWindow mainMenuWindow)
    {
        ProjectSelectionMainView projectSelectionMainView = new(mainMenuWindow);
        mainMenuWindow.CurrentUserControl = projectSelectionMainView;
    }
}