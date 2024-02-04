using System.Collections.ObjectModel;
using System.Windows.Input;
using CloudSculpt.Commands;
using CloudSculpt.Events;

namespace CloudSculpt.ViewModels;

public class ConfigureCloudInfraViewModel : ViewModelBase
{
    private int _secondColumnDefWidth;
    private string _deployButtonText;
    private bool _isExpand;
    private bool _isCollapsed;
    private bool _isNone;
    private double _serviceTypeButtonWidth;
    private double _iaaSOpacity;
    private double _paaSOpacity;
    private ObservableCollection<ServiceElementViewModel> _serviceElements;
    private ObservableCollection<ServiceElementViewModel> _infraCanvasCollection;
    
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
    
    public ObservableCollection<ServiceElementViewModel> ServiceElements
    {
        get => _serviceElements;
        set => SetField(ref _serviceElements, value);
    }
    
    public ObservableCollection<ServiceElementViewModel> InfraCanvasCollection
    {
        get => _infraCanvasCollection;
        set => SetField(ref _infraCanvasCollection, value);
    }
    
    
    public ICommand ToggleWidthCommand { get; }
    public ICommand IaaSCommand { get; }
    public ICommand PaaSCommand { get; }
    public ICommand InfraCanvasLayoutUpdatedCommand { get; }

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
        
        // Infra collection
        InfraCanvasCollection = [];
        
        // Button Opacity
        IaaSOpacity = 1;
        PaaSOpacity = 1;
        
        // Commands
        ToggleWidthCommand = new ToggleDeployWidthCommand(this);
        IaaSCommand = new IaaSCommand(this);
        PaaSCommand = new PaaSCommand(this);
        InfraCanvasLayoutUpdatedCommand = new InfraCanvasLayoutUpdatedCommand();

        
        // Events
        EventAggregator.Instance.Subscribe<AddServiceElementEvent>(OnAddServiceElement);
        EventAggregator.Instance.Subscribe<RemoveServiceElementEvent>(OnRemoveServiceElement);
    }

    private void OnRemoveServiceElement(RemoveServiceElementEvent obj)
    {
        foreach (var element in InfraCanvasCollection)
        {
            if (element.ElementIndex == obj.ElementIndex)
            {
                InfraCanvasCollection.Remove(element);
                return;
            }
        }
        
        // Decrement Element Count
        ServiceElementViewModel.ElementCounter -= 1;
    }

    private void OnAddServiceElement(AddServiceElementEvent obj)
    {
        InfraCanvasCollection.Add(obj.ServiceElement);
        
        // Increment Element Count
        ServiceElementViewModel.ElementCounter += 1;
    }
    
}
