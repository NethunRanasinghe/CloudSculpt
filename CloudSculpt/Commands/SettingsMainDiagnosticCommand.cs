using CloudSculpt.ViewModels;
using CloudSculpt.Views.UserControls;

namespace CloudSculpt.Commands;

public class SettingsMainDiagnosticCommand (SettingsV2ViewModel settingsV2ViewModel) : CommandBase
{
    public override void Execute(object? parameter)
    {
        settingsV2ViewModel.SettingsTitle = "Diagnostics";
        settingsV2ViewModel.CurrentSettingsView = new SettingsDiagnosticsView();
    }
}