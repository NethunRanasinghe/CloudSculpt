using System.Threading.Tasks;
using CloudSculpt.HelperClasses;
using CloudSculpt.ViewModels;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace CloudSculpt.Commands;

public class ConfigCloudInfraEditConfigApplyCommand (ServiceElementViewModel serviceElementViewModel) : CommandBase
{
    public override async void Execute(object? parameter)
    {
        // Apply Changes
        
        //Name
        serviceElementViewModel.Text = serviceElementViewModel.TempName;
        
        // Distro and Tag
        if (serviceElementViewModel.HasStarted)
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard("Error (C001)", "Can't Change Properties While A Process Is Already Running!\nClick YES to STOP and REMOVE the running process!",
                    ButtonEnum.YesNo, Icon.Error);

            var result = await box.ShowAsync();
            if (result != ButtonResult.Yes) return;
            await RemoveContainer();
            ApplyParameters();
        }
        else if (serviceElementViewModel is { HasStarted: false, HasGreeted: true })
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard("Error (C002)", "Can't Change Properties While There's an Un-Removed Stopped Process!\nClick YES to REMOVE the stopped process!",
                    ButtonEnum.YesNo);

            var result = await box.ShowAsync();
            if (result != ButtonResult.Yes) return;
            await RemoveContainer();
            ApplyParameters();
        }
        else
        {
            ApplyParameters();
        }
    }

    private void ApplyParameters()
    {
        var elementType = serviceElementViewModel.ElementType;

        if (elementType.Equals("v"))
        {
            serviceElementViewModel.OsType = serviceElementViewModel.TempOsType;

            if (!serviceElementViewModel.TempIsLinux)
            {
                serviceElementViewModel.Distro = ServiceElementViewModel.DefaultDistro;
                serviceElementViewModel.Tag = ServiceElementViewModel.DefaultTag;
                serviceElementViewModel.IsLinux = false;
                return;
            }
        
            var tempDistro = serviceElementViewModel.TempDistro;
            if(string.IsNullOrWhiteSpace(tempDistro)) return;
            tempDistro = tempDistro.Trim();
            var tempDistroSplit = tempDistro.Split(' ');
            serviceElementViewModel.Distro = tempDistroSplit[0];
            serviceElementViewModel.Tag = tempDistroSplit[1];
            serviceElementViewModel.IsLinux = true;
        }

        if (elementType.Equals("d"))
        {
            var tempFilePath = serviceElementViewModel.TempDockerFilePath;
            if(string.IsNullOrWhiteSpace(tempFilePath)) return;

            serviceElementViewModel.DockerFilePath = tempFilePath;
            serviceElementViewModel.ButtonState = true;
        }
        
        // Set Hardware config
        if (double.TryParse(serviceElementViewModel.TempRamAmount, out var tempRam))
        {
            serviceElementViewModel.RamAmount = tempRam;
        }
        
        if (double.TryParse(serviceElementViewModel.TempCoreCount, out var tempCpu))
        {
            serviceElementViewModel.CoreCount = tempCpu;
        }
        
        if (double.TryParse(serviceElementViewModel.TempStorageAmount, out var tempStorage))
        {
            serviceElementViewModel.StorageAmount = tempStorage;
        }
    }

    private async Task RemoveContainer()
    {
        var containerId = serviceElementViewModel.ContainerId;
        if (string.IsNullOrWhiteSpace(containerId)) return;
        await DockerManage.StopContainer(containerId);
        await DockerManage.RemoveContainer(containerId);
        serviceElementViewModel.ConfigCloudInfraTerminalOutput = string.Empty;
        serviceElementViewModel.HasStarted = false;
        serviceElementViewModel.ContainerId = string.Empty;
        serviceElementViewModel.HasGreeted = false;

    }
}