using System;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using CloudSculpt.HelperClasses;
using CloudSculpt.ViewModels;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace CloudSculpt.Commands;

public class ConfigCloudInfraEditWindowTerminalKeyDownCommand: CommandBase
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
            catch (Exception exception)
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
                            $"Details :- {exception.InnerException}",
                            ButtonEnum.Ok, Icon.Error);

                    await box.ShowAsync();
                }
            }
        }
        e.Text = string.Empty;
        serviceElementViewModel.HasStarted = true;
    }

    private static async Task<string> ExecuteCommand(ServiceElementViewModel serviceElementViewModel, string inputValue)
    {
        const string helpMessage = "\n\n----------Help----------" +
                                   "\n# These are some additional helper commands #\n\n" +
                                   "1. help     :: Show Help Screen\n" +
                                   "2. clear    :: Clear Terminal Output\n" +
                                   "3. files     :: Show a guide on installing a web based file manager\n"+
                                   "------------------------\n\n";

        const string fileManagerGuide = "\n\n----------Installing a File Manager (wfm)----------" +
                                        "\n# These are some additional helper commands #\n\n" +
                                        "1. Update\n" +
                                        "(Ex:- sudo apt update)\n\n" +
                                        "2. Upgrade (include -y to proceed)\n" +
                                        "(Ex:- sudo apt upgrade -y)\n\n" +
                                        "3. Install 'wget' package\n" +
                                        "(Ex:- sudo apt install wget -y)\n\n" +
                                        "4. Install 'screen' package\n" +
                                        "(Ex:- sudo apt install screen -y)\n\n" +
                                        "5. Get wfm\n" +
                                        "(Ex:- wget https://github.com/tenox7/wfm/releases/download/2.1.0/wfm-amd64-linux -O wfm)\n\n" +
                                        "6. Change Permission (Make the script executable)\n" +
                                        "(Ex:- chmod +x wfm)\n\n" +
                                        "7. Run\n" +
                                        "(Ex:- screen -dmS mysession ./wfm -allow_root -nopass_rw -robots -show_dot)\n\n"+
                                        "-------------------------------------------------------\n\n";
        
        if (inputValue.Equals("clear"))
        {
            serviceElementViewModel.ConfigCloudInfraTerminalOutput = string.Empty;
            return string.Empty;
        }

        if (inputValue.Equals("help"))
        {
            PrintToOutput(serviceElementViewModel,helpMessage);
            return string.Empty;
        }

        if (inputValue.Equals("files"))
        {
            PrintToOutput(serviceElementViewModel,fileManagerGuide);
            return string.Empty;
        }
        
        var inputValueSplit = inputValue.Split(" ").ToList();
        if (inputValueSplit.Count == 0) return string.Empty;
        inputValueSplit.Remove("sudo");
        var output = await DockerManage.ExecuteCommand(
            serviceElementViewModel.ContainerId, 
            inputValueSplit
            );

        return output.stdout;
    }
    
    private static async Task<string> StartAndExecuteCommand(ServiceElementViewModel serviceElementViewModel, string inputValue)
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

    private static void PrintToOutput(ServiceElementViewModel serviceElementViewModel, string outputValue)
    {
        if(string.IsNullOrWhiteSpace(outputValue)) return;
        serviceElementViewModel.ConfigCloudInfraTerminalOutput += $"Output >> {outputValue}\n";
    }
}