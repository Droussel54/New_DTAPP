using New_DTAPP.Models;

namespace New_DTAPP.Repository.Interfaces
{
    public interface IOperationRepository 
    {
        Task<List<OperationModel>> GetAllOperationsAsync(bool activeOnly = false,bool addBlankItemForDropDown = false);
        Task<OperationModel?> GetOperationByIdAsync(int id);
        Task<OperationModel?> AddOperationAsync(OperationModel model);
        Task<OperationModel?> UpdateOperationAsync(OperationModel model);
        Task<OperationModel?> RemoveOperationAsync(OperationModel model);
        bool? OperationExistsAsync(int id);

    }
}
