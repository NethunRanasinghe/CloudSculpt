using CloudSculpt.HelperClasses;

namespace CloudSculpt.Commands;

public class ProjectSelectionMainViewExitCommand : CommandBase
{
    public override void Execute(object? parameter)
    {
        ApplicationControl.ExitApplication();
    }
}