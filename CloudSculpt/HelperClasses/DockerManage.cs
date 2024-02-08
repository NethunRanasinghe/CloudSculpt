using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.Models;
using ICSharpCode.SharpZipLib.Tar;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace CloudSculpt.HelperClasses;

public class DockerManage
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

    #region Image Management

    public static async Task<IList<ImagesListResponse>> ListImages()
    {
        var client = new DockerClientConfiguration().CreateClient();
        IList<ImagesListResponse> images = await client.Images.ListImagesAsync(
            new ImagesListParameters(){});

        return images;
    }
    
    public static async Task PullImage(string imageName, string imageTag)
    {
        var client = new DockerClientConfiguration().CreateClient();
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
        var client = new DockerClientConfiguration().CreateClient();
        await client.Images.DeleteImageAsync(imageName, new ImageDeleteParameters());
    }
    
    public static async Task BuildDockerFile(string dockerFilePath, string imageTag)
    {
        var inputDockerfile = Path.Combine(dockerFilePath, "Dockerfile");
        var outputTarGzPath = Path.Combine(dockerFilePath, "dockerfile.tar.gz");
        
        CreateTarGzFile(inputDockerfile, outputTarGzPath);

        if (File.Exists(inputDockerfile))
        {
            var client = new DockerClientConfiguration().CreateClient();

            await using var stream = File.OpenRead(outputTarGzPath);
        
            var imageBuildParameters = new ImageBuildParameters
            {
                Dockerfile = "Dockerfile",
                Tags = new List<string> { imageTag },
            };

            var progress = new Progress<JSONMessage>(msg => Console.WriteLine(msg.Stream));

            try
            {
                await client.Images.BuildImageFromDockerfileAsync(
                    contents: stream,
                    parameters: imageBuildParameters,
                    authConfigs: null,
                    headers: null,
                    progress: progress,
                    cancellationToken: default);
            }
        
            catch (DockerApiException ex)
            {
                Console.WriteLine();
                
                var box = MessageBoxManager
                    .GetMessageBoxStandard("Error (D100)",$"Docker API Exception: {ex.StatusCode}, {ex.Message}",
                        ButtonEnum.Ok, Icon.Error);

                await box.ShowAsync();
            }   
        }
        else
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard("Error (D006)",
                    "tar.gz compress error, Failed to create a tar.gz file !",
                    ButtonEnum.Ok, Icon.Error);

            await box.ShowAsync();
        }
    }
    #endregion

    #region Container Management

    private static readonly string[] DnsParameters = ["8.8.8.8", "1.1.1.1"];
    private static readonly string[] CommandParameters = ["/bin/bash", "-c", "tail -f /dev/null"];
    
    public static async Task<IList<ContainerListResponse>> ListContainers()
    {
        var client = new DockerClientConfiguration().CreateClient();
        IList<ContainerListResponse> containers = await client.Containers.ListContainersAsync(
            new ContainersListParameters(){});

        return containers;
    }

    public static async Task<string> CreateContainer(string imageName)
    {
        var client = new DockerClientConfiguration().CreateClient();
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
        var client = new DockerClientConfiguration().CreateClient();
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
        var client = new DockerClientConfiguration().CreateClient();
        await client.Containers.StopContainerAsync(containerId, new ContainerStopParameters());
        await client.Containers.RemoveContainerAsync(containerId, new ContainerRemoveParameters());
    }

    public static async Task StartContainer(string containerId)
    {
        var client = new DockerClientConfiguration().CreateClient();
        await client.Containers.StartContainerAsync(
            containerId,
            new ContainerStartParameters()
        );
    }

    public static async Task StopContainer(string containerId)
    {
        var client = new DockerClientConfiguration().CreateClient();
        await client.Containers.StopContainerAsync(
            containerId,
            new ContainerStopParameters()
        );
    }
    
    #endregion

    #region Other

    public static async Task<string> VerifyDockerStatus()
    {
        string dockerStatus;
        
        try
        {
            await ListImages();
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

    private static void CreateTarGzFile(string sourceFilePath, string tarGzFilePath)
    {
        Encoding utf8Encoding = new UTF8Encoding(false);

        using var fs = new FileStream(tarGzFilePath, FileMode.Create);
        using var gzipStream = new GZipStream(fs, CompressionMode.Compress);
        using var tarStream = new TarOutputStream(gzipStream, utf8Encoding);
        var fileName = Path.GetFileName(sourceFilePath);
            
        var tarEntry = TarEntry.CreateEntryFromFile(sourceFilePath);
        tarEntry.Name = fileName;
        tarStream.PutNextEntry(tarEntry);

        using (var fileStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read))
        {
            fileStream.CopyTo(tarStream);
        }
        tarStream.CloseEntry();
    }

    #endregion
}