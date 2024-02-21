using System.IO;
using Avalonia;
using Avalonia.Styling;
using CloudSculpt.Models;
using Newtonsoft.Json;

namespace CloudSculpt.HelperClasses;

public static class ThemeHelper
{
    private const string ApplicationSettingsFile = "applicationSettings.json";
    public static string GetThemeFromSettings()
    {
        var json = File.ReadAllText(ApplicationSettingsFile);
        var applicationSettings = JsonConvert.DeserializeObject<ApplicationData>(json);
        return applicationSettings == null ? string.Empty : applicationSettings.Theme;
    }

    private static void SetThemeToSettings(string selectedTheme)
    {
        var json = File.ReadAllText(ApplicationSettingsFile);
        var applicationSettings = JsonConvert.DeserializeObject<ApplicationData>(json);
        
        if(applicationSettings == null) return;
        applicationSettings.Theme = selectedTheme;
        var output = JsonConvert.SerializeObject(applicationSettings, Formatting.Indented);
        File.WriteAllText(ApplicationSettingsFile,output);
    }

    public static void ChangeCurrentTheme(string selectedTheme)
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
        
        SetThemeToSettings(selectedTheme);
    }
}