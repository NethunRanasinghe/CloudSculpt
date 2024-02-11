using System.Threading.Tasks;
using Avalonia.Platform.Storage;

namespace CloudSculpt.Interfaces;

public interface IFileDialogService
{
    Task<IStorageFile?> OpenFilePickerAsync();

}