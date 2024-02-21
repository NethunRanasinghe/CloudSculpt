using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;

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
}