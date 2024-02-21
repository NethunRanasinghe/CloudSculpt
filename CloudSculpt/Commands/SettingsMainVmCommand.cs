using CloudSculpt.ViewModels;
using CloudSculpt.Views.UserControls;

namespace CloudSculpt.Commands;

public class SettingsMainVmCommand (SettingsV2ViewModel settingsV2ViewModel) : CommandBase
{
    public override void Execute(object? parameter)
    {
        settingsV2ViewModel.SettingsTitle = "VM Settings";
        settingsV2ViewModel.CurrentSettingsView = new SettingsVmView();
    }
}