using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.Models;
using ICSharpCode.SharpZipLib.Tar;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace CloudSculpt.HelperClasses;

public static class DockerManage
{
    /*
     *                              Method  Test    Notes
     * 1. List Images           -   x       x
     * 2. List Containers       -   x       x
     * 3. Pull Image            -   x       x
     * 4. Create Containers     -   x       x       image should be available locally
     * 5. Build Dockerfile      -   x       x       (path, name:tag)
     * 6. Remove Image          -   x       x       name:tag
     * 7. Remove Container      -   x       x
     * 8. Start a Container     -   x       x
     * 9. Stop a Container      -   x       x
     * 10. Verify Docker Status -   x       x
     */

    // Uri :- Dev Only (ssh tunnel to remote docker server)
    // ssh -fNL localhost:23750:/var/run/docker.sock nethunranasingha@192.168.2.157

    private static readonly Uri? RemoteDockerUri = new("tcp://localhost:23750");
    //private static readonly Uri? RemoteDockerUri = null;

    private static readonly Uri DockerUri = RemoteDockerUri ?? 
                                            (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) 
                                                ? new Uri("npipe://./pipe/docker_engine") 
                                                : new Uri("unix:///var/run/docker.sock"));    
    #region Image Management

    private static async Task<IList<ImagesListResponse>> ListImages()
    {
        using var client = new DockerClientConfiguration(DockerUri).CreateClient();
        IList<ImagesListResponse> images = await client.Images.ListImagesAsync(
            new ImagesListParameters(){});

        return images;
    }
    
    public static async Task PullImage(string imageName, string imageTag)
    {
        using var client = new DockerClientConfiguration(DockerUri).CreateClient();
        await client.Images.CreateImageAsync(
            new ImagesCreateParameters
            {
                FromImage = imageName,
                Tag = imageTag,
            },
            null,
            new Progress<JSONMessage>());
        
    }
    
    public static async Task RemoveImage(string imageName)
    {
        using var client = new DockerClientConfiguration(DockerUri).CreateClient();
        await client.Images.DeleteImageAsync(imageName, new ImageDeleteParameters());
    }
    
    public static async Task<string> BuildDockerFile(string dockerFilePath, string imageTag, List<string> dockerFileCopyDirs)
    {
        var outputTarGzPath = $"{dockerFilePath}.tar.gz";
        
        CreateTarGzFile(dockerFilePath, outputTarGzPath, dockerFileCopyDirs);
        if (File.Exists(dockerFilePath))
        {
            using var client = new DockerClientConfiguration(DockerUri).CreateClient();

            await using var stream = File.OpenRead(outputTarGzPath);

            var imageBuildParameters = new ImageBuildParameters
            {
                Dockerfile = "Dockerfile",
                Tags = new List<string> { imageTag },
                NetworkMode = "host"
            };

            var progressMessages = new StringBuilder();
            var progress = new Progress<JSONMessage>(msg =>
            {
                progressMessages.AppendLine(msg.Stream);
            });

            try
            {
                await client.Images.BuildImageFromDockerfileAsync(
                    contents: stream,
                    parameters: imageBuildParameters,
                    authConfigs: null,
                    headers: null,
                    progress: progress,
                    cancellationToken: default);

                return progressMessages.ToString();
            }

            catch (DockerApiException ex)
            {
                Console.WriteLine();

                var box = MessageBoxManager
                    .GetMessageBoxStandard("Error (D100)",$"Docker API Exception: {ex.StatusCode}, {ex.Message}",
                        ButtonEnum.Ok, Icon.Error);

                await box.ShowAsync();
                return string.Empty;
            }

        }
        
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard("Error (D006)",
                    "tar.gz compress error, Failed to create a tar.gz file !",
                    ButtonEnum.Ok, Icon.Error);

            await box.ShowAsync();
            return string.Empty;
        }
    }

    #endregion

    #region Container Management

    private static readonly string[] DnsParameters = ["8.8.8.8", "1.1.1.1"];
    private static readonly string[] CommandParameters = ["/bin/bash", "-c", "tail -f /dev/null"];
    
    public static async Task<IList<ContainerListResponse>> ListContainers()
    {
        using var client = new DockerClientConfiguration(DockerUri).CreateClient();
        IList<ContainerListResponse> containers = await client.Containers.ListContainersAsync(
            new ContainersListParameters(){});

        return containers;
    }

    public static async Task<string> CreateContainer(string imageName)
    {
        using var client = new DockerClientConfiguration(DockerUri).CreateClient();
        var createdContainer = await client.Containers.CreateContainerAsync(new CreateContainerParameters()
        {
            Image = imageName,
            HostConfig = new HostConfig()
            {
                DNS = DnsParameters,
                NetworkMode = "host"
            }
        });

        var containerId = createdContainer.ID;
        return containerId;
    }

    public static async Task<string> CreateContainerWithCommand(string imageName)
    {
        using var client = new DockerClientConfiguration(DockerUri).CreateClient();
        var createdContainer = await client.Containers.CreateContainerAsync(new CreateContainerParameters()
        {
            Image = imageName,
            Cmd = CommandParameters,
            HostConfig = new HostConfig()
            {
                DNS = DnsParameters,
                NetworkMode = "host"
            }
        });

        var containerId = createdContainer.ID;
        return containerId;
    }

    public static async Task RemoveContainer(string containerId)
    {
        using var client = new DockerClientConfiguration(DockerUri).CreateClient();
        await StopContainer(containerId);
        await client.Containers.RemoveContainerAsync(containerId, new ContainerRemoveParameters());
    }

    public static async Task StartContainer(string containerId)
    {
        using var client = new DockerClientConfiguration(DockerUri).CreateClient();
        await client.Containers.StartContainerAsync(
            containerId,
            new ContainerStartParameters()
        );
    }

    public static async Task StopContainer(string containerId)
    {
        using var client = new DockerClientConfiguration(DockerUri).CreateClient();
        await client.Containers.StopContainerAsync(
            containerId,
            new ContainerStopParameters()
        );
    }

    public static async Task<(string stdout, string stderr)> ExecuteCommand(string containerId, IList<string> command)
    {
        using var client = new DockerClientConfiguration(RemoteDockerUri)
            .CreateClient();

        var execParams = new ContainerExecCreateParameters()
        {
            AttachStderr = true,
            AttachStdout = true,
            Cmd = command,
        };

        var exec = await client.Exec.ExecCreateContainerAsync(containerId, execParams);
        var stream = await client.Exec.StartAndAttachContainerExecAsync(exec.ID, false);

        using var stdoutStream = new MemoryStream();
        using var stderrStream = new MemoryStream();

        await stream.CopyOutputToAsync(
            null,
            stdoutStream,
            stderrStream,
            CancellationToken.None);

        var stdout = Encoding.UTF8.GetString(stdoutStream.ToArray());
        var stderr = Encoding.UTF8.GetString(stderrStream.ToArray());

        return (stdout, stderr);
    }
    
    #endregion

    #region Networks

    private static async Task CreateIpVlanNetwork()
    {
        var networkData = NetworkingHelper.GetNetworkData();
        using var client = new DockerClientConfiguration(DockerUri).CreateClient();
        var networks = await client.Networks.ListNetworksAsync();
        var networkExists = networks.Any(n => n.Name == "cloudsculptnetwork");
        
        if (!networkExists)
        {
            await client.Networks.CreateNetworkAsync(new NetworksCreateParameters
            {
                Name = "cloudsculptnetwork",
                Driver = "ipvlan",
                IPAM = new IPAM
                {
                    Config = new List<IPAMConfig>
                    {
                        new()
                        {
                            Subnet = $"{networkData.NetworkAddress}/{networkData.Cidr}",
                            Gateway = networkData.GatewayAddress.ToString(),
                            IPRange = $"{networkData.NetworkAddress}/{networkData.Cidr}",
                        }
                    }
                },
                Options = new Dictionary<string, string>
                {
                    { "parent", $"{networkData.InterfaceName}" }
                }
            });
        }
    }
    
    /*private static async Task CreateIpVlanNetwork()
    {
        using var client = new DockerClientConfiguration(DockerUri).CreateClient();
        var networks = await client.Networks.ListNetworksAsync();
        var networkExists = networks.Any(n => n.Name == "cloudsculptnetwork");

        if (!networkExists)
        {
            await client.Networks.CreateNetworkAsync(new NetworksCreateParameters
            {
                Name = "cloudsculptnetwork",
                Driver = "ipvlan",
                IPAM = new IPAM
                {
                    Config = new List<IPAMConfig>
                    {
                        new()
                        {
                            Subnet = "192.168.1.0/24",
                            Gateway = "192.168.1.1",
                            IPRange = "192.168.1.200/30"
                        }
                    }
                },
                Options = new Dictionary<string, string>
                {
                    { "parent", "eth0" }
                }
            });
        }
    }*/

    #endregion
    
    #region Other

    public static async Task<string> VerifyDockerStatus()
    {
        string dockerStatus;
        
        try
        {
            await ListImages();
            //await CreateIpVlanNetwork();
            dockerStatus = string.Empty;
        }
        catch (Exception ex)
        {
            dockerStatus = ex.Message;
            
            var box = MessageBoxManager
                .GetMessageBoxStandard("Error (D007)",
                    $"Docker Connection Failed, Connection to docker engine failed, Verify docker status !\n\nError:- {dockerStatus}",
                    ButtonEnum.Ok, Icon.Error);

            await box.ShowAsync();
        }

        return dockerStatus;
    }

    private static void CreateTarGzFile(string sourceFilePath, string tarGzFilePath, List<string> dockerFileCopyCmdDirs)
    {
        // Delete tar file if exist
        if (File.Exists(tarGzFilePath))
        {
            File.Delete(tarGzFilePath);
        }

        Encoding utf8Encoding = new UTF8Encoding(false);

        using var fs = new FileStream(tarGzFilePath, FileMode.Create);
        using var gzipStream = new GZipStream(fs, CompressionMode.Compress);
        using var tarStream = new TarOutputStream(gzipStream, utf8Encoding);

        // Add the source file to the tar.gz file
        AddFileOrDirectoryToTarGz(tarStream, sourceFilePath, Path.GetDirectoryName(sourceFilePath));

        // Add the files and directories from the dockerFileCopyCmdDirs list to the tar.gz file
        foreach (var fileOrDirPath in dockerFileCopyCmdDirs)
        {
            if (File.Exists(fileOrDirPath) || Directory.Exists(fileOrDirPath))
            {
                AddFileOrDirectoryToTarGz(tarStream, fileOrDirPath, Path.GetDirectoryName(fileOrDirPath));
            }
        }
    }



    private static void AddFileOrDirectoryToTarGz(TarOutputStream tarStream, string fileOrDirPath, string baseDirectoryPath)
    {
        var tarEntry = TarEntry.CreateEntryFromFile(fileOrDirPath);
        // Use a relative path from the base directory as the tar entry name
        tarEntry.Name = Path.GetRelativePath(baseDirectoryPath, fileOrDirPath);
        tarStream.PutNextEntry(tarEntry);

        if (File.Exists(fileOrDirPath))
        {
            // If it's a file, copy its content to the tar.gz file
            using var fileStream = new FileStream(fileOrDirPath, FileMode.Open, FileAccess.Read);
            fileStream.CopyTo(tarStream);
        }

        tarStream.CloseEntry();
    }
    #endregion
}