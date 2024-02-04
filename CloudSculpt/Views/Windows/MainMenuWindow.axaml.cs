using Avalonia.Controls;
using ProjectSelectionMainView = CloudSculpt.Views.UserControls.ProjectSelectionMainView;

namespace CloudSculpt.Views.Windows;

public partial class MainMenuWindow : Window
{
    private UserControl _currentUserControl;
    
    public MainMenuWindow()
    {
        InitializeComponent();
        
        UserControl projectSelectionMainView = new ProjectSelectionMainView(this);
        
        SetMainMenuWindowContentControl(projectSelectionMainView);
    }
    
    // Set View
    public UserControl CurrentUserControl
    {
        get { return _currentUserControl; }
        set
        {
            if (_currentUserControl != value)
            {
                _currentUserControl = value;
                SetMainMenuWindowContentControl(_currentUserControl);
            }
        }
    }

    private void SetMainMenuWindowContentControl(UserControl toBeSetUserControl)
    {
        MainMenuWindowContentControl.Content = toBeSetUserControl;
    }
}