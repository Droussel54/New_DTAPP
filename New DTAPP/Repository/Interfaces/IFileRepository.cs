using New_DTAPP.Models;

namespace New_DTAPP.Repository.Interfaces
{
    public interface IFileRepository 
    {
        Task<List<FileModel>> GetAllFilesAsync();
        Task<FileModel?> GetFileByIdAsync(int id);
        Task<List<FileModel>> GetFilesByTransferIdAsync(int id);
        Task<FileModel?> AddFileAsync(FileModel model);
        Task<FileModel?> UpdateFileAsync(FileModel model);
        Task<FileModel?> RemoveFileAsync(FileModel model);
        Task<bool?> RemoveFilesByTransferIdAsync(int id);
    }
}