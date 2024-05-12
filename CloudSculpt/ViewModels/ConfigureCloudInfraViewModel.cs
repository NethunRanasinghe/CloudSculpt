using System.Collections.ObjectModel;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Input;
using CloudSculpt.Commands;
using CloudSculpt.Events;
using CloudSculpt.Interfaces;
using CloudSculpt.Services;
using CloudSculpt.Views.UserControls;

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
    private bool _isBillingSelected;
    private UserControl _billingCalView;
    private Cursor _cursorType;
    
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

    public bool IsNone {
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
    
    public bool IsBillingSelected
    {
        get => _isBillingSelected;
        set => SetField(ref _isBillingSelected, value);
    }
    
    public UserControl BillingCalView
    {
        get => _billingCalView;
        set => SetField(ref _billingCalView, value);
    }
    
    public Cursor CursorType
    {
        get => _cursorType;
        set => SetField(ref _cursorType, value);
    }
    
    public ICommand ToggleWidthCommand { get; }
    public ICommand IaaSCommand { get; }
    public ICommand PaaSCommand { get; }
    public ICommand InfraCanvasLayoutUpdatedCommand { get; }
    public ICommand ConfigureCloudInfraWindowBillingButton { get; }
    public ICommand ConfigureCloudInfraWindowDeployButton { get; }
    public ICommand ConfigCloudInfraDeployListClearList { get; }
    public ICommand ConfigCloudInfraWindowBack { get; }
    public ICommand DeployTerraformConfig { get; }

    public readonly Window CurrentWindow;
    public readonly INavigationService NavigationService;
    public static ObservableCollection<ServiceElementViewModel> InfraCanvasCollectionStatic { get; set; } = [];

    public ConfigureCloudInfraViewModel(Window window)
    {
        // Current Window and Navigation Service
        NavigationService = ServiceLocator.Resolve<INavigationService>();
        CurrentWindow = window;
        
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
        if (InfraCanvasCollectionStatic.Count > 0)
        {
            InfraCanvasCollection = InfraCanvasCollectionStatic;
        }
        else
        {
            InfraCanvasCollection = [];
            InfraCanvasCollectionStatic = [];
        }
        
        // Button Opacity
        IaaSOpacity = 1;
        PaaSOpacity = 1;
        
        // Initial Cursor Type
        CursorType = new Cursor(StandardCursorType.Arrow);
        
        // Commands
        ToggleWidthCommand = new ToggleDeployWidthCommand(this);
        IaaSCommand = new IaaSCommand(this);
        PaaSCommand = new PaaSCommand(this);
        InfraCanvasLayoutUpdatedCommand = new InfraCanvasLayoutUpdatedCommand();
        ConfigureCloudInfraWindowBillingButton = new ConfigureCloudInfraWindowBillingButton(this);
        ConfigureCloudInfraWindowDeployButton = new ConfigureCloudInfraWindowDeployListButton(this);
        ConfigCloudInfraDeployListClearList = new ConfigCloudInfraDeployListClearList(this);
        ConfigCloudInfraWindowBack = new ConfigCloudInfraWindowBackCommand(this);
        DeployTerraformConfig = new DeployTerraformConfigCommand(this);

        // Initial Billing Selected State
        IsBillingSelected = false;
        
        // Initial BillingCallUserControl
        BillingCalView = new ConfigCloudInfraDeployList();
        
        // Events
        EventAggregator.Instance.Subscribe<AddServiceElementEvent>(OnAddServiceElement);
        EventAggregator.Instance.Subscribe<RemoveServiceElementEvent>(OnRemoveServiceElement);
        EventAggregator.Instance.Subscribe<WaitCursorChangeEvent>(OnApplicationWait);
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

    private void OnApplicationWait(WaitCursorChangeEvent obj)
    {
        if (obj.IsWait)
        {
            CursorType = new Cursor(StandardCursorType.Wait);
        }
        else
        {
            CursorType = new Cursor(StandardCursorType.Arrow);
        }
    }
    
}
