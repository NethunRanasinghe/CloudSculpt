using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.IO;
using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace CloudSculpt.HelperClasses;

public class Docker
{
    /*
     *                              Method  Test    Notes
     * 1. List Images           -   x       x
     * 2. List Containers       -   x       x
     * 3. Pull Image            -   x       x
     * 4. Create Containers     -   x       x       image should be available locally
     * 5. Build Dockerfile      -   x
     * 6. Remove Image          -   x       x       name:tag
     * 7. Remove Container      -   x       x
     * 8. Start a Container     -   x       x
     * 9. Stop a Container      -   x       x
     */

    #region Image Management

    public async Task<IList<ImagesListResponse>> ListImages()
    {
        DockerClient client = new DockerClientConfiguration().CreateClient();
        IList<ImagesListResponse> images = await client.Images.ListImagesAsync(
            new ImagesListParameters(){});

        return images;
    }
    
    public async Task PullImage(string imageName, string imageTag)
    {
        DockerClient client = new DockerClientConfiguration().CreateClient();
        await client.Images.CreateImageAsync(
            new ImagesCreateParameters
            {
                FromImage = imageName,
                Tag = imageTag,
            },
            null,
            new Progress<JSONMessage>());
        
    }
    
    public async Task RemoveImage(string imageName)
    {
        DockerClient client = new DockerClientConfiguration().CreateClient();
        await client.Images.DeleteImageAsync(imageName, new ImageDeleteParameters());
    }

    #endregion

    #region Container Management

    public async Task<IList<ContainerListResponse>> ListContainers()
    {
        DockerClient client = new DockerClientConfiguration().CreateClient();
        IList<ContainerListResponse> containers = await client.Containers.ListContainersAsync(
            new ContainersListParameters(){});

        return containers;
    }

    public async Task CreateContainer(string imageName)
    {
        DockerClient client = new DockerClientConfiguration().CreateClient();
        await client.Containers.CreateContainerAsync(new CreateContainerParameters()
        {
            Image = imageName,
            HostConfig = new HostConfig()
            {
                DNS = new[] { "8.8.8.8", "1.1.1.1" }
            }
        });
    }
    
    public async Task BuildDockerFile(string dockerFilePath, string imageTag)
    {
        DockerClient client = new DockerClientConfiguration().CreateClient();

        await using var stream = File.OpenRead(Path.Combine(dockerFilePath, "Dockerfile"));
        
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
            Console.WriteLine($"Docker API Exception: {ex.StatusCode}, {ex.Message}");
        }
    }


    public async Task RemoveContainer(string containerId)
    {
        DockerClient client = new DockerClientConfiguration().CreateClient();
        await client.Containers.StopContainerAsync(containerId, new ContainerStopParameters());
        await client.Containers.RemoveContainerAsync(containerId, new ContainerRemoveParameters());
    }

    public async Task StartContainer(string containerId)
    {
        DockerClient client = new DockerClientConfiguration().CreateClient();
        await client.Containers.StartContainerAsync(
            containerId,
            new ContainerStartParameters()
        );
    }

    public async Task StopContainer(string containerId)
    {
        DockerClient client = new DockerClientConfiguration().CreateClient();
        await client.Containers.StopContainerAsync(
            containerId,
            new ContainerStopParameters()
        );
    }
    
    #endregion

    #region Other

    private void VerifyDockerStatus()
    {
        
    }

    private void CreateTarFile(string filePath)
    {
        
    }

    #endregion
}