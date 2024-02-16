using System;
using System.Collections.Generic;
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
        var fileDirPath = Path.GetDirectoryName(filePath); // C:/Users/nethu/Downloads
        var dockerfileCopyFiles = new List<string>();

        if (!(fileContent.Contains("FROM")|| fileContent.Contains("ENTRYPOINT")))
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard("C003", "Invalid Dockerfile!",
                    ButtonEnum.Ok,Icon.Error);

            await box.ShowAsync();
            return;
        }

        
        if (fileContent.Contains("COPY"))
        {
            if(string.IsNullOrWhiteSpace(fileDirPath)) return;
            dockerfileCopyFiles = ExtractCopyCommands(fileContent, fileDirPath);
        }
        
        serviceElementViewModel.TempDockerFilePath = filePath;
        if(dockerfileCopyFiles.Count <= 0) return;
        serviceElementViewModel.TempDockerFileCopyDirs = dockerfileCopyFiles;
    }
    
    private static List<string> ExtractCopyCommands(string fileContent, string fileDirPath)
    {
        var copyCommands = new List<string>();

        using var reader = new StringReader(fileContent);
        while (reader.ReadLine() is { } line)
        {
            if (line.Contains("COPY"))
            {
                var dockerFileCopyDir = Path.Join(fileDirPath,line.Split(" ")[1]);
                copyCommands.Add(dockerFileCopyDir);
            }
        }

        return copyCommands;
    }
}