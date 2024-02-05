using CloudSculpt.ViewModels;
using CloudSculpt.Views.UserControls;

namespace CloudSculpt.Commands;

public class ConfigCloudInfraEditWindowConfigCommand (ConfigCloudInfraEditViewModel viewModel) : CommandBase
{
    public override void Execute(object? parameter)
    {
        if(viewModel.ConfigButtonSelected) return;
        
        viewModel.ConfigCloudInfraEditCurrentView = new ConfigCloudInfraEditConfigView();
        viewModel.ConfigCloudInfraEditCurrentView.DataContext = viewModel.ConfigCloudInfraEditCurrentViewModel;
        
        viewModel.ConfigButtonSelected = true;
        viewModel.TerminalButtonSelected = false;
    }
}