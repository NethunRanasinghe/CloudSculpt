using CloudSculpt.HelperClasses;

namespace CloudSculpt.Commands;

public class MainWindowLoadedCommand : CommandBase
{
    public override async void Execute(object? parameter)
    {
        // Get All VM Data
        await DatabaseManage.GetAllVmData();
    }
}