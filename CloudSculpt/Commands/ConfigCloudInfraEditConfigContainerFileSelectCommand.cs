using System.IO;
using CloudSculpt.Interfaces;
using CloudSculpt.Services;
using CloudSculpt.ViewModels;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace CloudSculpt.Commands;

public class ConfigCloudInfraEditConfigContainerFileSelectCommand(ServiceElementViewModel serviceElementViewModel) : CommandBase
{
    public override async void Execute(object? parameter)
    {
        var fileDialogService = ServiceLocator.Resolve<IFileDialogService>();
        var file = await fileDialogService.OpenFilePickerAsync();
        if (file == null) return;


        await using var stream = await file.OpenReadAsync();
        using var streamReader = new StreamReader(stream);
        var fileContent = await streamReader.ReadToEndAsync();
        var fileLoc = file.Path; // file:///C:/Users/nethu/Downloads/Test.txt
        var filePath = fileLoc.AbsolutePath; // C:/Users/nethu/Downloads/Test.txt

        if (!(fileContent.Contains("FROM") && (fileContent.Contains("CMD") || fileContent.Contains("ENTRYPOINT"))))
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard("C003", "Invalid Dockerfile!",
                    ButtonEnum.Ok,Icon.Error);

            await box.ShowAsync();
            return;
        }

        if (fileContent.Contains("COPY"))
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard("C004", "COPY command in dockerfile is not supported!",
                    ButtonEnum.Ok,Icon.Error);

            await box.ShowAsync();
            return;
        }

        serviceElementViewModel.TempDockerFilePath = filePath;
    }
}