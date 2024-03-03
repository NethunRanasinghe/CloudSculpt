using System.Threading.Tasks;
using Avalonia;
using Avalonia.Styling;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace CloudSculpt.HelperClasses;

public static class ThemeHelper
{
    private static async Task SetThemeToSettings(string selectedTheme)
    {
        var rows = await DatabaseManage.SetApplicationTheme(selectedTheme);
        if(rows > 0) return;
        var box = MessageBoxManager
            .GetMessageBoxStandard("Error (D001)", "Cannot update Theme !",ButtonEnum.Ok,Icon.Error);

        await box.ShowAsync();
    }

    public static async Task ChangeCurrentTheme(string selectedTheme)
    {
        if (selectedTheme.Equals("Light"))
        {
            if (Application.Current != null) Application.Current.RequestedThemeVariant = ThemeVariant.Light;
        }
        
        if (selectedTheme.Equals("Dark"))
        {
            if (Application.Current != null) Application.Current.RequestedThemeVariant = ThemeVariant.Dark;
        }
        
        if (selectedTheme.Equals("Default"))
        {
            if (Application.Current != null) Application.Current.RequestedThemeVariant = ThemeVariant.Default;
        }
        
        await SetThemeToSettings(selectedTheme);
        await DatabaseManage.GetApplicationData();
    }
}