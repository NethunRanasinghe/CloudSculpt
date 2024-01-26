using System;
using System.Globalization;
using System.Windows.Input;
using Avalonia.Data.Converters;
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
    public ICommand ToggleWidthCommand { get; }
    
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
        
        // Commands
        ToggleWidthCommand = new ToggleDeployWidthCommand(this);
    }
}

public class HalfConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double width)
        {
            return width / 2;
        }

        return 100;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
