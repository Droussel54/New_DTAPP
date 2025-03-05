using New_DTAPP.Models;

namespace New_DTAPP.Repository.Interfaces
{
    public interface ITransferRepository
    {
        Task<List<TransferModel>> GetAllTransfersAsync();
        Task<TransferModel?> GetTransferByIdAsync(int id);
        TransferModel? AddTransfer(TransferModel model);
        TransferModel UpdateTransfer(TransferModel model);
        Task<TransferModel> RemoveTransferAsync(TransferModel model);
        Task<bool> ExistsAsync(int id);
    }
}