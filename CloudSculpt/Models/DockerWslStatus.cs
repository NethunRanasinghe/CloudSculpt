using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MsBox.Avalonia;

namespace CloudSculpt.Models;

public static class DockerWslStatus
{

    private static string _os;
    private static string _status;
    private static string? _wsl;
    
    #region Get
    public static string Os
    {
        get
        {
            if (string.IsNullOrEmpty(_os))
            {
                SetOs();
            }

            return _os;
        }
    }

    public static string Status
    {
        get
        {
            if (string.IsNullOrEmpty(_status))
            {
                SetStatus();
            }

            return _status;
        }
    }

    public static string Wsl
    {
        get
        {
            if (string.IsNullOrEmpty(_wsl))
            {
                SetWsl();
            }

            return _wsl;
        }
    }
    #endregion
    
    #region Set
    private static void SetOs()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            _os = "Windows";
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            _os = "MacOS";
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            _os = "Linux";
        }
        else
        {
            _os = "Unknown";
        }
    }

    private static async Task SetStatus()
    {
        var dockerExecutable = "docker";

        // Windows
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            dockerExecutable += ".exe";
        }
        
        // Other
        string? localPath = Environment.GetEnvironmentVariable("PATH");
        if (!string.IsNullOrEmpty(localPath))
        {
            foreach (var path in localPath.Split(Path.PathSeparator))
            {
                var fullPath = Path.Combine(path, dockerExecutable);
                if (File.Exists(fullPath))
                {
                    _status = "Present";
                    return;
                }
            }
        }
        else
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard("ERROR (D005)", "Null System Path !",
                    MsBox.Avalonia.Enums.ButtonEnum.Ok,
                    MsBox.Avalonia.Enums.Icon.Error);

            await box.ShowAsync();
        }


        _status = "Absent";
    }

    private static void SetWsl()
    {
        const string wslPath = @"C:\Windows\system32\wsl.exe";
        if (File.Exists(wslPath))
        {
            _wsl = GetWslVersion();
        }
        else
        {
            // Set to Null
            _wsl = null;
        }
    }
    #endregion

    #region Other Methods

    private static string GetWslVersion()
    {
        try
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/c wsl -v",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(processStartInfo);
            if (process != null)
            {
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                // Parse the output to extract version information
                    
                /*WSL version: 2.0.9.0
                    Kernel version: 5.15.133.1-1
                    WSLg version: 1.0.59
                    MSRDC version: 1.2.4677
                    Direct3D version: 1.611.1-81528511
                    DXCore version: 10.0.25131.1002-220531-1700.rs-onecore-base2-hyp
                    Windows version: 10.0.22621.3007*/

                string wslVersion = output.Split('\n')[0].Split(':')[1][2..];
                return wslVersion;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return string.Empty;
    }

    #endregion
    
}