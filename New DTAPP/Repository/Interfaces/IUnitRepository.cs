using New_DTAPP.Models;

namespace New_DTAPP.Repository.Interfaces
{
    public interface IUnitRepository 
    {
        Task<List<UnitModel>> GetAllUnitsAsync(bool activeOnly = false, bool addBlankItemForDropDown = false);
        Task<UnitModel?> GetUnitByIdAsync(int id);
        Task<UnitModel?> AddUnitAsync(UnitModel model);
        Task<UnitModel?> UpdateUnitAsync(UnitModel model);
        Task<UnitModel?> RemoveUnitAsync(UnitModel model);
        bool? UnitExistsAsync(int id);
    }
}
