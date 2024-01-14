using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using CloudSculpt.Commands;
using CloudSculpt.Models;

namespace CloudSculpt.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    // Stack : Last In First Out
    static Stack<string> _logData = new Stack<string>();
    
    private string _os;
    private string _status;
    private string _log;

    public string Os
    {
        get => _os;
        set => SetField(ref _os, value);
    }
    
    public string Status
    {
        get => _status;
        set => SetField(ref _status, value);
    }
    
    public string Log
    {
        get => _log;
        set => SetField(ref _log, value);
    }
    
    public event EventHandler<string> LogUpdated;
    
    public ICommand SettingsDockerInstallCommand { get; }
    public ICommand SettingsDockerUninstallCommand { get; }
    
    public SettingsViewModel()
    {
        // Initialize the property with the value from DockerWslStatus
        Os = DockerWslStatus.Os;
        Status = DockerWslStatus.Status;
        
        // Commands
        SettingsDockerInstallCommand = new DockerInstallCommand(this);
        SettingsDockerUninstallCommand = new DockerUninstallCommand();
    }
    
    // Method to update log and raise the LogUpdated event
    public async Task UpdateLogAsync(string newLog, bool clear)
    {
        await Task.Run(() =>
        {
            if (clear)
            {
                _logData.Clear();
                return;
            }
            
            // Add New Logs to the stack
            _logData.Push(newLog);
            
            if (_logData.Count > 0)
            {
                string formattedLog = string.Empty;
                
                foreach (var log in _logData)
                {
                    formattedLog += log;
                }
        
                Log  = formattedLog;
            }
        
            LogUpdated?.Invoke(this, newLog);
        });
    }
}