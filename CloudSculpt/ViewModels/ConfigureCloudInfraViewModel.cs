using System.Windows.Input;
using Avalonia.Controls;
using CloudSculpt.Commands;

namespace CloudSculpt.ViewModels;

public class ConfigureCloudInfraViewModel : ViewModelBase
{
    private GridLength _secondColumnDefWidth;
    private string _deployButtonText;
    
    public GridLength SecondColumnDefWidth
    {
        get => _secondColumnDefWidth;
        set => SetField(ref _secondColumnDefWidth, value);
    }

    public string DeployButtonText
    {
        get => _deployButtonText;
        set => SetField(ref _deployButtonText, value);
    }
    
    public ICommand ToggleWidthCommand { get; }
    
    public ConfigureCloudInfraViewModel()
    {
        // Initial Second Column Width
        SecondColumnDefWidth = new GridLength(0);
        DeployButtonText = "Deploy ->";
        
        // Commands
        ToggleWidthCommand = new ToggleDeployWidthCommand(this);
    }
}