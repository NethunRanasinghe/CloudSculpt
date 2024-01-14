using System.Threading.Tasks;
using CloudSculpt.Models;
using CloudSculpt.ViewModels;

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
        
    }
    
    public void DockerInstallLinux()
    {
        
    }
    
    public void DockerInstallMacOs()
    {
        
    }
}