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
    private double _elementClickedLeft;
    private double _elementClickedTop;

    public static int ElementCounter = 0;
    
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
    
    public ICommand ServiceElementCommand { get; }
    public ICommand ServiceElementCanvasEditCommand { get; }
    public ICommand ServiceElementCanvasDeleteCommand { get; }
    public ICommand ServiceElementCanvasPointerPressed { get; }
    public ICommand ServiceElementCanvasPointerReleased { get; }
    public ICommand ServiceElementCanvasPointerMoved { get; }

    public ServiceElementViewModel()
    {
        // Text
        Text = "Default";
        
        // Create a new Bitmap
        var bitmap = new Bitmap(AssetLoader.Open(new Uri("avares://CloudSculpt/Assets/dockerBlack.png")));
        
        // Update the Image property
        Image = bitmap;
        
        // Initial Canvas Locations
        CanvasLeft = 0;
        CanvasTop = 0;
        
        // Click Command
        ServiceElementCommand = new ServiceElementAddCommand(this);
        ServiceElementCanvasDeleteCommand = new ServiceElementCanvasRemoveCommand(this);
        ServiceElementCanvasEditCommand = new ServiceElementCanvasEditCommand();
        ServiceElementCanvasPointerPressed = new ServiceElementCanvasPointerPressed(this);
        ServiceElementCanvasPointerReleased = new ServiceElementCanvasPointerReleased(this);
        ServiceElementCanvasPointerMoved = new ServiceElementCanvasPointerMoved(this);
    }
}