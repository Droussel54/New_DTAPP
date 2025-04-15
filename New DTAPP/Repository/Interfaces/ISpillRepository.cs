using New_DTAPP.Models;

namespace New_DTAPP.Repository.Interfaces;

public interface ISpillRepository
{
    Task<List<SpillModel>> GetAllSpillsAsync();
    Task<SpillModel?> GetSpillByIdAsync(int id);
    Task<SpillModel?> AddSpillAsync(SpillModel model);
    SpillModel UpdateSpillAsync(SpillModel model);
    Task<SpillModel> RemoveSpillAsync(SpillModel model);
    Task<bool> SpillExistsAsync(int id);
    Task<SpillModel?> GetSpillByTransferId(int id);
}