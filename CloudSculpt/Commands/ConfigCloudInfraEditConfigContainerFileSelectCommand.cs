using System;
using System.IO;
using CloudSculpt.Interfaces;
using CloudSculpt.Services;
using CloudSculpt.ViewModels;

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
        var fileName = file.Path; //file:///C:/Users/nethu/Downloads/Test.txt

        
        Console.WriteLine(fileContent);
        Console.WriteLine(fileName);
    }
}