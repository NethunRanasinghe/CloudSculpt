using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using CliWrap;
using CliWrap.Buffered;
using CloudSculpt.HelperClasses;
using CloudSculpt.ViewModels;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace CloudSculpt.Commands;

public class ConfigCloudInfraEditTerminalKeyDownCommand: CommandBase
{
    public override async void Execute(object? parameter)
    {
        if (parameter is not TextBox e) return;
        if (e.DataContext is not ServiceElementViewModel serviceElementViewModel)
            return;
        
        // Input
        var inputValue = e.Text;
        serviceElementViewModel.ConfigCloudInfraTerminalOutput += $"Command >> {inputValue}\n";

        if (!string.IsNullOrWhiteSpace(inputValue))
        {
            serviceElementViewModel.HasStarted = false;
            inputValue = inputValue.Trim();
            
            // Execute
            try
            {
                // Output
                var outputValue = await ExecuteCommand(serviceElementViewModel, inputValue);
                PrintToOutput(serviceElementViewModel, outputValue);
            }
            catch (CliWrap.Exceptions.CommandExecutionException exception)
            {
                if (exception.Message.Contains("not running"))
                {
                    var outputValue = await StartAndExecuteCommand(serviceElementViewModel, inputValue);
                    PrintToOutput(serviceElementViewModel, outputValue);
                }
                else
                {
                    var box = MessageBoxManager
                        .GetMessageBoxStandard("Command Error",
                            $"{exception.Message}\n" +
                            $"Exit Code :- {exception.ExitCode}\n" +
                            $"Details :- {exception.InnerException}",
                            ButtonEnum.Ok, Icon.Error);

                    await box.ShowAsync();
                }
            }
            catch (Exception otherExceptions)
            {
                var box = MessageBoxManager
                    .GetMessageBoxStandard("Error",
                        $"{otherExceptions.Message}\n" +
                        $"{otherExceptions.InnerException}",
                        ButtonEnum.Ok, Icon.Error);

                await box.ShowAsync();
            }
        }
        e.Text = string.Empty;
        serviceElementViewModel.HasStarted = true;
    }

    private async Task<string> ExecuteCommand(ServiceElementViewModel serviceElementViewModel, string inputValue)
    {
        var arguments = $"exec {serviceElementViewModel.ContainerId} {inputValue}";
        var outputValue = await Cli.Wrap("docker")
            .WithArguments(arguments)
            .ExecuteBufferedAsync();

        return outputValue.StandardOutput;
    }
    
    private async Task<string> StartAndExecuteCommand(ServiceElementViewModel serviceElementViewModel, string inputValue)
    {
        var containerId = serviceElementViewModel.ContainerId;
        var distro = serviceElementViewModel.Distro;
        var tag = serviceElementViewModel.Tag;
        
        // Delete the old container
        await DockerManage.RemoveContainer(containerId);
        
        // Create new container with CMD(tails -f /dev/null) and start it
        var newContainerId = await DockerManage.CreateContainerWithCommand($"{distro}:{tag}");
        await DockerManage.StartContainer(newContainerId);
        serviceElementViewModel.ContainerId = newContainerId;
        
        // Execute the Command
        var commandOutput = await ExecuteCommand(serviceElementViewModel, inputValue);
        return commandOutput;
    }

    private void PrintToOutput(ServiceElementViewModel serviceElementViewModel, string outputValue)
    {
        serviceElementViewModel.ConfigCloudInfraTerminalOutput += $"Output >> {outputValue}\n";
    }
}