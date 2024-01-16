using System.Collections.Generic;
using System.Windows.Input;
using CloudSculpt.Commands;

namespace CloudSculpt.ViewModels;

public class ConfigureCloudInfraViewModel : ViewModelBase
{
    private int _secondColumnDefWidth;
    private string _deployButtonText;
    private bool _isExpand;
    private bool _isCollapsed;
    private bool _isNone;

    public int SecondColumnDefWidth
    {
        get => _secondColumnDefWidth;
        set => SetField(ref _secondColumnDefWidth, value);
    }

    public string DeployButtonText
    {
        get => _deployButtonText;
        set => SetField(ref _deployButtonText, value);
    }
    
    public bool IsExpand
    {
        get => _isExpand;
        set => SetField(ref _isExpand, value);
    }
    
    public bool IsCollapsed
    {
        get => _isCollapsed;
        set => SetField(ref _isCollapsed, value);
    }
    
    public bool IsNone
    {
        get => _isNone;
        set => SetField(ref _isNone, value);
    }
    
    public ICommand ToggleWidthCommand { get; }
    
    public ConfigureCloudInfraViewModel()
    {
        // Initial Second Column Width
        SecondColumnDefWidth = 0;
        DeployButtonText = "Deploy ->";
        
        // Animation State
        IsNone = true;
        IsCollapsed = false;
        IsExpand = false;
        
        // Commands
        ToggleWidthCommand = new ToggleDeployWidthCommand(this);
    }
}