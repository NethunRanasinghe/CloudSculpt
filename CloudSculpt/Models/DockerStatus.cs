using System;
using System.IO;
using System.Runtime.InteropServices;

namespace CloudSculpt.Models;

public static class DockerStatus
{

    private static string _os;
    private static string _status;
    
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

    private static void SetStatus()
    {
        var dockerExecutable = "docker";

        // Windows
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            dockerExecutable += ".exe";
        }
        
        // Other
        foreach (var path in Environment.GetEnvironmentVariable("PATH").Split(Path.PathSeparator))
        {
            var fullPath = Path.Combine(path, dockerExecutable);
            if (File.Exists(fullPath))
            {
                _status = "Present";
            }
        }

        _status = "Absent";
    }
    
}