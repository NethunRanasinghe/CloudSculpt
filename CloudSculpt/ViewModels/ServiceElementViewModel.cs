
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
    private double _canvasRight;
    
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

    public double CanvasRight
    {
        get => _canvasRight;
        set => SetField(ref _canvasRight, value);
    }
    
    public ICommand ServiceElementCommand { get; }
    public ICommand ServiceElementCanvasEditCommand { get; }
    public ICommand ServiceElementCanvasDeleteCommand { get; }

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
        CanvasRight = 0;
        
        // Click Command
        ServiceElementCommand = new ServiceElementCommand(this);
        ServiceElementCanvasDeleteCommand = new ServiceElementCanvasDeleteCommand();
        ServiceElementCanvasEditCommand = new ServiceElementCanvasEditCommand();
    }
}