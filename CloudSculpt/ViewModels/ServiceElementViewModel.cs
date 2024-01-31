
namespace CloudSculpt.ViewModels;

public class ServiceElementViewModel : ViewModelBase
{
    private string _text;
    private string _imagePath;
    
    public string Text
    {
        get => _text;
        set => SetField(ref _text, value);
    }
    
    public string ImagePath
    {
        get => _imagePath;
        set => SetField(ref _imagePath, value);
    }
    
    public ServiceElementViewModel()
    {
        Text = "Default";
        ImagePath = "avares://CloudSculpt/Assets/dockerBlack.png";
    }
}