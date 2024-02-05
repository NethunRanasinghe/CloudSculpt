using CloudSculpt.ViewModels;
using CloudSculpt.Views.UserControls;

namespace CloudSculpt.Commands;

public class ConfigCloudInfraEditWindowTerminalCommand (ConfigCloudInfraEditViewModel viewModel) : CommandBase
{
    public override void Execute(object? parameter)
    {
        if (viewModel.TerminalButtonSelected) return;
        
        viewModel.ConfigCloudInfraEditCurrentView = new ConfigCloudInfraEditTerminalView();
        viewModel.ConfigCloudInfraEditCurrentView.DataContext = viewModel.ConfigCloudInfraEditCurrentViewModel;
        
        viewModel.ConfigButtonSelected = false;
        viewModel.TerminalButtonSelected = true;
    }
}