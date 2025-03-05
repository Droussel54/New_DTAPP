using New_DTAPP.Models;

namespace New_DTAPP.Repository.Interfaces
{
    public interface ISystemRepository
    {
        Task<List<SystemModel>> GetAllSystemsAsync(bool activeOnly = false, bool addBlankItemForDropDown = false);
        Task<SystemModel?> GetSystemByIdAsync(int id);
        Task<SystemModel?> AddSystemAsync(SystemModel model);
        Task<SystemModel?> UpdateSystemAsync(SystemModel model);
        Task<SystemModel?> RemoveSystemAsync(SystemModel model);
        bool? SystemExistsAsync(int id);
    }
}
