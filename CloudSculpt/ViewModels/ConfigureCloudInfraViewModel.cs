using System.Collections.ObjectModel;
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
    private double _serviceTypeButtonWidth;
    private ObservableCollection<ServiceElementViewModel> _serviceElements;
    private double _iaaSOpacity;
    private double _paaSOpacity;


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

    public double ServiceTypeButtonWidth
    {
        get => _serviceTypeButtonWidth;
        set => SetField(ref _serviceTypeButtonWidth, value);
    }
    
    public ObservableCollection<ServiceElementViewModel> ServiceElements
    {
        get => _serviceElements;
        set => SetField(ref _serviceElements, value);
    }

    public double IaaSOpacity
    {
        get => _iaaSOpacity;
        set => SetField(ref _iaaSOpacity, value);
    }
    
    public double PaaSOpacity
    {
        get => _paaSOpacity;
        set => SetField(ref _paaSOpacity, value);
    }
    
    public ICommand ToggleWidthCommand { get; }
    public ICommand IaaSCommand { get; }
    public ICommand PaaSCommand { get; }
    
    public ConfigureCloudInfraViewModel()
    {
        // Initial Second Column Width
        SecondColumnDefWidth = 0;
        DeployButtonText = "Deploy ->";
        
        // Initial set service type button length
        ServiceTypeButtonWidth = 120;
        
        // Animation State
        IsNone = true;
        IsCollapsed = false;
        IsExpand = false;
        
        // Service Elements
        ServiceElements = [];
        
        // Button Opacity
        IaaSOpacity = 1;
        PaaSOpacity = 1;
        
        // Commands
        ToggleWidthCommand = new ToggleDeployWidthCommand(this);
        IaaSCommand = new IaaSCommand(this);
        PaaSCommand = new PaaSCommand(this);
    }
}
