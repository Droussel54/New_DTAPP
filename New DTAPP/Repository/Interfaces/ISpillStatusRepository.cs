using New_DTAPP.Models;

namespace New_DTAPP.Repository.Interfaces;

public interface ISpillStatusRepository
{
    Task<List<SpillStatusModel>> GetAllSpillStatusesAsync(bool excludeArchived = false);
    Task<SpillStatusModel?> GetSpillStatusByIdAsync(int id);
    Task<SpillStatusModel?> AddSpillStatusAsync(SpillStatusModel model);
    SpillStatusModel UpdateSpillStatusAsync(SpillStatusModel model);
    Task<SpillStatusModel> RemoveSpillStatusAsync(SpillStatusModel model);
    Task<bool> SpillStatusExistsAsync(int id);
}
