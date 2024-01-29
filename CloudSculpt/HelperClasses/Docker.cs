using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace CloudSculpt.HelperClasses;

public class Docker
{
    /*
     *                              Method  Test
     * 1. List Images           -   x
     * 2. List Containers       -   x
     * 3. Pull Image            -   x
     * 4. Create Containers     -   x
     * 5. Build Dockerfile      -   x
     * 6. Remove Image          -   x
     * 7. Remove Container      -   x
     * 8. Start a Container     -   x
     * 9. Stop a Container      -   x
     */

    #region Image Management

    private async Task<IList<ImagesListResponse>> ListImages()
    {
        DockerClient client = new DockerClientConfiguration().CreateClient();
        IList<ImagesListResponse> images = await client.Images.ListImagesAsync(
            new ImagesListParameters(){});

        return images;
    }
    
    private async Task PullImage(string imageName, string imageTag)
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
    
    private async Task RemoveImage(string imageName)
    {
        DockerClient client = new DockerClientConfiguration().CreateClient();
        await client.Images.DeleteImageAsync(imageName, new ImageDeleteParameters());
    }

    #endregion

    #region Container Management

    private async Task<IList<ContainerListResponse>> ListContainers()
    {
        DockerClient client = new DockerClientConfiguration().CreateClient();
        IList<ContainerListResponse> containers = await client.Containers.ListContainersAsync(
            new ContainersListParameters(){});

        return containers;
    }

    private async Task CreateContainer(string imageName)
    {
        DockerClient client = new DockerClientConfiguration().CreateClient();
        await client.Containers.CreateContainerAsync(new CreateContainerParameters()
        {
            Image = imageName,
            HostConfig = new HostConfig()
            {
                DNS = new[] { "8.8.8.8", "8.8.4.4" }
            }
        });
    }
    
    private async Task BuildDockerFile(string dockerFilePath, string imageTag)
    {
        DockerClient client = new DockerClientConfiguration().CreateClient();

        await using var stream = File.OpenRead(Path.Combine(dockerFilePath, "Dockerfile"));
        var imageBuildParameters = new ImageBuildParameters
        {
            Dockerfile = "Dockerfile",
            Tags = new List<string> { imageTag },
        };

        var progress = new Progress<JSONMessage>(msg => Console.WriteLine(msg.Stream));
        
        await client.Images.BuildImageFromDockerfileAsync(
            contents: stream,
            parameters: imageBuildParameters,
            authConfigs: null,
            headers: null,
            progress: progress,
            cancellationToken: default);
    }


    private async Task RemoveContainer(string containerId)
    {
        DockerClient client = new DockerClientConfiguration().CreateClient();
        await client.Containers.StopContainerAsync(containerId, new ContainerStopParameters());
        await client.Containers.RemoveContainerAsync(containerId, new ContainerRemoveParameters());
    }

    private async Task StartContainer(string containerId)
    {
        DockerClient client = new DockerClientConfiguration().CreateClient();
        await client.Containers.StartContainerAsync(
            containerId,
            new ContainerStartParameters()
        );
    }

    private async Task StopContainer(string containerId)
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

    #endregion
}