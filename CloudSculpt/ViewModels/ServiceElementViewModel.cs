using System;
using System.Windows.Input;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CloudSculpt.Commands;
using CloudSculpt.HelperClasses;

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
    private string _tempOsType;
    private string _tempName;
    private string _tempDistro;
    private string _tempTag;
    private bool _isLinux;
    private bool _tempIsLinux;
    private string _imageName;
    private string _dockerFilePath;
    private string _tempDockerFilePath;
    private bool _buttonState;
    private string _tempRamAmount;
    private double _ramAmount;
    private string? _tempCoreCount;
    private double _coreCount;
    private string? _tempStorageAmount;
    private double _storageAmount;

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
    
    public string TempOsType
    {
        get => _tempOsType;
        set => SetField(ref _tempOsType, value);
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
    
    public bool TempIsLinux
    {
        get => _tempIsLinux;
        set => SetField(ref _tempIsLinux, value);
    }
    
    public string FullImageNameTag
    {
        get => $"{Distro} {Tag}";
    }
    
    public string ImageName
    {
        get => _imageName;
        set => SetField(ref _imageName, value);
    }
    
    public string TempDockerFilePath
    {
        get => _tempDockerFilePath;
        set => SetField(ref _tempDockerFilePath, value);
    }
    
    public string DockerFilePath
    {
        get => _dockerFilePath;
        set => SetField(ref _dockerFilePath, value);
    }
    
    public bool ButtonState
    {
        get => _buttonState;
        set => SetField(ref _buttonState, value);
    }
    
    public double StorageAmount
    {
        get => _storageAmount;
        set => SetField(ref _storageAmount, value);
    }

    public string? TempStorageAmount
    {
        get => _tempStorageAmount;
        set => SetField(ref _tempStorageAmount, value);
    }

    public double CoreCount
    {
        get => _coreCount;
        set => SetField(ref _coreCount, value);
    }

    public string? TempCoreCount
    {
        get => _tempCoreCount;
        set => SetField(ref _tempCoreCount, value);
    }

    public double RamAmount
    {
        get => _ramAmount;
        set => SetField(ref _ramAmount, value);
    }

    public string TempRamAmount
    {
        get => _tempRamAmount;
        set => SetField(ref _tempRamAmount, value);
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
    public ICommand ConfigCloudInfraEditConfigContainerFileSelectCommand { get; }
    public ICommand ConfigCloudInfraEditSetTempRamValueCommand { get; }
    public ICommand ConfigCloudInfraEditSetTempCoreValueCommand { get; }
    public ICommand ConfigCloudInfraEditSetTempStorageValueCommand { get; }

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
        IsLinux = true;
        
        // Initial ButtonState
        ButtonState = false;
        
        // Set Initial temp values
        TempDistro = Distro;
        TempTag = Tag;
        TempName = Text;
        
        // Initial image Name and Dockerfile Path
        ImageName = StringFormatExtra.GetRandomStringForDocker();
        TempDockerFilePath = string.Empty;
        DockerFilePath = string.Empty;

        // Initial Hardware Config
        TempRamAmount = "0";
        RamAmount = 0;
        TempCoreCount = "0";
        CoreCount = 0;
        TempStorageAmount = "0";
        StorageAmount = 0;
        
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
        ConfigCloudInfraEditConfigContainerFileSelectCommand = new ConfigCloudInfraEditConfigContainerFileSelectCommand(this);
        ConfigCloudInfraEditSetTempRamValueCommand = new ConfigCloudInfraEditSetTempRamValueCommand(this);
        ConfigCloudInfraEditSetTempCoreValueCommand = new ConfigCloudInfraEditSetTempCoreValueCommand(this);
        ConfigCloudInfraEditSetTempStorageValueCommand = new ConfigCloudInfraEditSetTempStorageValueCommand(this);
    }
}