using New_DTAPP.Models;

namespace New_DTAPP.Repository.Interfaces
{
    public interface ITransferTypeRepository
    {
        Task<List<TransferTypeModel>> GetAllTransferTypeAsync(bool? excludeArchive);
        Task<TransferTypeModel?> GetTransferTypeByIdAsync(int id);
        Task<TransferTypeModel?> AddTransferTypeAsync(TransferTypeModel model);
        Task<TransferTypeModel?> UpdateTransferTypeAsync(TransferTypeModel model);
        Task<TransferTypeModel?> RemoveTransferTypeAsync(TransferTypeModel model);
        bool? TransferTypeExistsAsync(int id);
    }
}
