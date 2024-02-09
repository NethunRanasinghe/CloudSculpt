using System;
using System.Windows.Input;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CloudSculpt.Commands;

namespace CloudSculpt.ViewModels;

public class ServiceElementViewModel : ViewModelBase
{
    private string _text;
    private Bitmap _image;
    private double _canvasLeft;
    private double _canvasTop;
    private int _elementIndex;
    private bool _isPressed;
    private string _elementType;
    private double _elementClickedLeft;
    private double _elementClickedTop;
    private string _configType;
    private string _configCloudInfraTerminalOutput;
    private bool _hasStarted;
    private string _containerId;
    private bool _hasGreeted;
    private string _distro;
    private string _tag;
    private string _osType;
    private string _tempName;
    private string _tempDistro;
    private string _tempTag;
    private bool _isLinux;
    
    public static int ElementCounter { get; set; } = 0;
    public static double CanvasWidth { get; set; } = 0;
    public static double CanvasHeight { get; set; } = 0;
    public static double CanvasScreenX { get; set; } = 0;
    public static double CanvasScreenY { get; set; } = 0;

    private const string DefaultOs = "linux";
    public const string DefaultDistro = "ubuntu";
    public const string DefaultTag = "22.04";

    public string Text
    {
        get => _text;
        set => SetField(ref _text, value);
    }
    
    public Bitmap Image
    {
        get => _image;
        set => SetField(ref _image, value);
    }
    
    public double CanvasLeft
    {
        get => _canvasLeft;
        set => SetField(ref _canvasLeft, value);
    }

    public double CanvasTop
    {
        get => _canvasTop;
        set => SetField(ref _canvasTop, value);
    }

    public int ElementIndex
    {
        get => _elementIndex;
        set => SetField(ref _elementIndex, value);
    }

    public bool IsPressed
    {
        get => _isPressed;
        set => SetField(ref _isPressed, value);
    }
    
    public string ElementType
    {
        get => _elementType;
        set => SetField(ref _elementType, value);
    }
    
    public double ElementClickedLeft
    {
        get => _elementClickedLeft;
        set => SetField(ref _elementClickedLeft, value);
    }

    public double ElementClickedTop
    {
        get => _elementClickedTop;
        set => SetField(ref _elementClickedTop, value);
    }

    public string ConfigType
    {
        get => _configType;
        set => SetField(ref _configType, value);
    }
    
    public string ConfigCloudInfraTerminalOutput
    {
        get => _configCloudInfraTerminalOutput;
        set => SetField(ref _configCloudInfraTerminalOutput, value);
    }
    
    public bool HasStarted
    {
        get => _hasStarted;
        set => SetField(ref _hasStarted, value);
    }

    public string ContainerId
    {
        get => _containerId;
        set => SetField(ref _containerId, value);
    }

    public bool HasGreeted
    {
        get => _hasGreeted;
        set => SetField(ref _hasGreeted, value);
    }
    
    public string Distro
    {
        get => _distro;
        set
        {
            if (SetField(ref _distro, value))
            {
                OnPropertyChanged(nameof(FullImageNameTag));
            }
        }
    }
    
    public string Tag
    {
        get => _tag;
        set
        {
            if (SetField(ref _tag, value))
            {
                OnPropertyChanged(nameof(FullImageNameTag));
            }
        }
    }
    
    public string OsType
    {
        get => _osType;
        set => SetField(ref _osType, value);
    }
    
    public string TempName
    {
        get => _tempName;
        set => SetField(ref _tempName, value);
    }
    
    public string TempDistro
    {
        get => _tempDistro;
        set => SetField(ref _tempDistro, value);
    }
    
    public string TempTag
    {
        get => _tempTag;
        set => SetField(ref _tempTag, value);
    }
    
    public bool IsLinux
    {
        get => _isLinux;
        set => SetField(ref _isLinux, value);
    }
    
    public string FullImageNameTag
    {
        get => $"{Distro} {Tag}";
    }
    
    public ICommand ServiceElementCommand { get; }
    public ICommand ServiceElementCanvasEditCommand { get; }
    public ICommand ServiceElementCanvasDeleteCommand { get; }
    public ICommand ServiceElementCanvasPointerPressedCommand { get; }
    public ICommand ServiceElementCanvasPointerReleasedCommand { get; }
    public ICommand ServiceElementCanvasPointerMovedCommand { get; }
    public ICommand ConfigCloudInfraEditConfigCancelCommand { get; }
    public ICommand ConfigCloudInfraEditTerminalKeyDownCommand { get; }
    public ICommand ConfigCloudInfraEditWindowTerminalStartCommand { get; }
    public ICommand ConfigCloudInfraEditWindowTerminalStopCommand { get; }
    public ICommand ConfigCloudInfraEditConfigApplyCommand { get; }
    public ICommand ConfigCloudInfraEditWindowDistroSelChangedCommand { get; }
    public ICommand ConfigCloudInfraEditWindowOsSelChangedCommand { get; }

    public ServiceElementViewModel()
    {
        // Text
        Text = "Default";
        ConfigType = "Default";
        
        // Create a new Bitmap
        var bitmap = new Bitmap(AssetLoader.Open(new Uri("avares://CloudSculpt/Assets/dockerBlack.png")));
        
        // Update the Image property
        Image = bitmap;
        
        // Initial Canvas Locations
        CanvasLeft = 0;
        CanvasTop = 0;
        
        // Initial Terminal Output
        ConfigCloudInfraTerminalOutput = string.Empty;
        
        // Initial TextBox Enable Status
        HasStarted = false;
        
        // Initial Has Greeted Status
        HasGreeted = false;
        
        // Initial container ID 
        ContainerId = string.Empty;
        
        // Initial element status
        ElementType = string.Empty;
        
        // Default Distro and Tag
        OsType = DefaultOs;
        Distro = DefaultDistro;
        Tag = DefaultTag;
        
        // Initial IsLinuxValue
        IsLinux = false;
        
        // Set Initial temp values
        TempDistro = string.Empty;
        TempTag = string.Empty;
        TempName = Text;
        
        // Commands
        ServiceElementCommand = new ServiceElementAddCommand(this);
        ServiceElementCanvasDeleteCommand = new ServiceElementCanvasRemoveCommand(this);
        ServiceElementCanvasEditCommand = new ServiceElementCanvasEditCommand(this);
        ServiceElementCanvasPointerPressedCommand = new ServiceElementCanvasPointerPressed(this);
        ServiceElementCanvasPointerReleasedCommand = new ServiceElementCanvasPointerReleased(this);
        ServiceElementCanvasPointerMovedCommand = new ServiceElementCanvasPointerMoved(this);
        ConfigCloudInfraEditConfigCancelCommand = new ConfigCloudInfraEditConfigCancelCommand();
        ConfigCloudInfraEditTerminalKeyDownCommand = new ConfigCloudInfraEditTerminalKeyDownCommand();
        ConfigCloudInfraEditWindowTerminalStartCommand = new ConfigCloudInfraEditWindowTerminalStartCommand(this);
        ConfigCloudInfraEditWindowTerminalStopCommand = new ConfigCloudInfraEditWindowTerminalStopCommand(this);
        ConfigCloudInfraEditConfigApplyCommand = new ConfigCloudInfraEditConfigApplyCommand(this);
        ConfigCloudInfraEditWindowDistroSelChangedCommand = new ConfigCloudInfraEditWindowDistroSelChangedCommand(this);
        ConfigCloudInfraEditWindowOsSelChangedCommand = new ConfigCloudInfraEditWindowOsSelChangedCommand(this);
    }
}