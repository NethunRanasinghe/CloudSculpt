using System.Threading.Tasks;
using Avalonia.Controls;
using CliWrap;
using CliWrap.Buffered;
using CloudSculpt.ViewModels;

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
        serviceElementViewModel.ConfigCloudInfraTerminalOutput += $"{inputValue}\n";

        if (!string.IsNullOrWhiteSpace(inputValue))
        {
            inputValue = inputValue.Trim();
            
            // Execute
            try
            {
                // Output
                var outputValue = await ExecuteCommand(serviceElementViewModel, inputValue);
                serviceElementViewModel.ConfigCloudInfraTerminalOutput += $"{outputValue}\n";
            }
            catch (CliWrap.Exceptions.CommandExecutionException exception)
            {
                if (exception.Message.Contains("not running"))
                {
                    var outputValue = await RunAndExecuteCommand(serviceElementViewModel, inputValue);
                    serviceElementViewModel.ConfigCloudInfraTerminalOutput += $"{outputValue}\n";
                }
            }
            
        
            
        }
        e.Text = string.Empty;
    }

    private async Task<string> ExecuteCommand(ServiceElementViewModel serviceElementViewModel, string inputValue)
    {
        var outputValue = await Cli.Wrap("docker")
            .WithArguments(args => args
                .Add("exec")
                .Add("-d")
                .Add(serviceElementViewModel.ContainerId)
                .Add(inputValue))
            .ExecuteBufferedAsync();

        return outputValue.StandardOutput;
    }
    
    private async Task<string> RunAndExecuteCommand(ServiceElementViewModel serviceElementViewModel, string inputValue)
    {
        var outputValue = await Cli.Wrap("docker")
            .WithArguments(args => args
                .Add("run")
                .Add("-d")
                .Add(serviceElementViewModel.ContainerId)
                .Add(inputValue))
            .ExecuteBufferedAsync();

        return outputValue.StandardOutput;
    }
}