using New_DTAPP.Models;

namespace New_DTAPP.Repository.Interfaces
{
    public interface IFileExtensionRepository
    {
        Task<List<FileExtensionModel>> GetAllFileExtensionsAsync(bool activeOnly = false, bool addBlankItemForDropDown = false);
        Task<FileExtensionModel?> GetFileExtensionByIdAsync(int id);
        Task<FileExtensionModel?> AddFileExtensionAsync(FileExtensionModel model);
        Task<FileExtensionModel?> UpdateFileExtensionAsync(FileExtensionModel model);
        Task<FileExtensionModel?> RemoveFileExtensionAsync(FileExtensionModel model);
        bool? FileExtensionExistsAsync(int id);
    }
}
