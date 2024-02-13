using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using CloudSculpt.Interfaces;

namespace CloudSculpt.Services;

public class FileDialogService(TopLevel window) : IFileDialogService
{
    public async Task<IStorageFile?> OpenFilePickerAsync()
    {
        IReadOnlyList<IStorageFile?> files = await window.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Select Dockerfile",
            AllowMultiple = false
        });

        if (files.Count < 1) return null;
        return files[0];
        
        /*await using var stream = await files[0].OpenReadAsync();
        using var streamReader = new StreamReader(stream);
        var fileContent = await streamReader.ReadToEndAsync();

        return fileContent;*/
    }
}