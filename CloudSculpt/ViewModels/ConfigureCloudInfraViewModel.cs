using System.Windows.Input;
using Avalonia.Controls;
using CloudSculpt.Commands;

namespace CloudSculpt.ViewModels;

public class ConfigureCloudInfraViewModel : ViewModelBase
{
    private GridLength _secondColumnDefWidth;
    
    public GridLength SecondColumnDefWidth
    {
        get => _secondColumnDefWidth;
        set => SetField(ref _secondColumnDefWidth, value);
    }
    
    public ICommand ToggleWidthCommand { get; }
    
    public ConfigureCloudInfraViewModel()
    {
        // Initial Second Column Width
        SecondColumnDefWidth = new GridLength(0);
        
        // Commands
        ToggleWidthCommand = new ToggleDeployWidthCommand(this);
    }
}