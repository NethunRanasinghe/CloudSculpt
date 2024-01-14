using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using CloudSculpt.Models;
using CloudSculpt.ViewModels;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace CloudSculpt.HelperClasses;

public class DockerConfig(SettingsViewModel settingsViewModel)
{
    private static int _logIndex = 0;
    private async Task UpdateLog(string logMessage, bool clear)
    {
        _logIndex++;
        
        if (clear)
        {
            _logIndex = 0;
        }
        
        await settingsViewModel.UpdateLogAsync($"{_logIndex} : {logMessage}\n", clear);
    }

    #region Windows

    public async void DockerInstallWindows()
    {
        await UpdateLog(string.Empty, true);
        
        await UpdateLog("Starting Docker Install (Platform - Windows)",false);
        await UpdateLog("Checking WSL Availability...",false);
        
        // Check WSL2 Availability
        if (string.IsNullOrEmpty(DockerWslStatus.Wsl))
        {
            await UpdateLog("ERROR (D001): WSL Unavailable",false);
            await UpdateLog("ERROR (D001): Install WSL2 To Continue",false);
            return;
        }

        await UpdateLog("WSL Available", false);
        await UpdateLog("Checking WSL2 Availability...", false);

        string wslVersion = DockerWslStatus.Wsl;
        wslVersion = StringFormatExtra.GetFilteredString(wslVersion);
        await UpdateLog($"WSL Version : {wslVersion}",false);
        
        if (!wslVersion.Split('.')[0].Equals("2"))
        {
            await UpdateLog("ERROR (D003): WSL2 Unavailable",false);
            await UpdateLog("ERROR (D003): Update to WSL2 To Continue",false);
            return;
        }
        
        await UpdateLog("WSL2 Available", false);
        
        // Start Docker Install
        
        // Download Docker Exe
        await UpdateLog("Downloading Docker Desktop Installer...", false);
        bool dockerDownloadStatus = await DownloadDockerWindows();
        
        if (dockerDownloadStatus)
        {
            await UpdateLog("Downloading Docker Desktop Installer Complete", false);
        }
        else
        {
            await UpdateLog("Downloading Docker Desktop Installer Failed", false);
            return;
        }
        
        await UpdateLog("Starting Docker Installation...", false);
    }
    
    private static async Task<bool> DownloadDockerWindows()
    {
        const int maxRetries = 3;
        int retries = 0;

        using var client = new HttpClient();
        
        client.Timeout = TimeSpan.FromMinutes(5);

        bool downloadSuccessful = false;

        do
        {
            try
            {
                await using var stream = await client.GetStreamAsync("https://desktop.docker.com/win/main/amd64/Docker%20Desktop%20Installer.exe");

                await using var fs = new FileStream("DockerInstall.exe", FileMode.Create);

                // Await the completion of the copy operation
                await stream.CopyToAsync(fs);
                    
                downloadSuccessful = true;
                break;
            }
            catch (UnauthorizedAccessException)
            {
                var box = MessageBoxManager
                    .GetMessageBoxStandard("Error (D004)",
                        "Invalid Permissions, Does not have enough permissions to save the downloaded file!",
                        ButtonEnum.Ok, Icon.Error);

                await box.ShowAsync();
                return downloadSuccessful;
            }
            catch (Exception exception)
            {
                var box = MessageBoxManager
                    .GetMessageBoxStandard("Exception",
                        $"{exception}",
                        ButtonEnum.Ok, Icon.Error);

                await box.ShowAsync();
                return downloadSuccessful;
            }
            
            retries++;

            // Delay before the next retry
            await Task.Delay(1000);

        } while (retries < maxRetries);

        return downloadSuccessful;
    }

    
    #endregion

    
    public void DockerInstallLinux()
    {
        
    }
    
    public void DockerInstallMacOs()
    {
        
    }
}