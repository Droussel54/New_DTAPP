using Microsoft.EntityFrameworkCore;
using New_DTAPP.Data;
using New_DTAPP.Extensions;
using New_DTAPP.Models;
using New_DTAPP.Repository.Interfaces;
using New_DTAPP.Security;

namespace New_DTAPP.Repository
{
    public class TransferRepository : ITransferRepository
    {
        private readonly IConfiguration _configuration;
        private readonly NewDtappContext _context;
        public TransferRepository(NewDtappContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<TransferModel?> GetTransferByIdAsync(int id)
        {
            TransferModel? transferModel = null;

            var transfer = await _context.Transfers
               .Include(t => t.ClientUnit)
               .Include(t => t.CompletedUser)
               .Include(t => t.DestSystem)
               .Include(t => t.Operation)
               .Include(t => t.OrigSystem)
               .Include(t => t.ReviewedUser)
               .Include(t => t.Files)
               .Include(t => t.TransferType)
               .FirstOrDefaultAsync(t => t.TransferId == id); 

            transferModel = transfer?.EntityToModelTransfer();

            return transferModel;
        }

        public async Task<List<TransferModel>> GetAllTransfersAsync()
        {
            List<TransferModel> transferModel = new();

            var transfers = await _context.Transfers
               .Include(t => t.ClientUnit)
               .Include(t => t.CompletedUser)
               .Include(t => t.DestSystem)
               .Include(t => t.Operation)
               .Include(t => t.OrigSystem)
               .Include(t => t.ReviewedUser)
               .Include(t => t.Files)
               .Include(t => t.TransferType)
               .Include(t => t.Spill)
               .AsNoTracking().ToListAsync();

            transferModel = transfers?.EntityToModelTransfer()!;

            return transferModel.ToList();
        }

        public TransferModel? AddTransfer(Models.TransferModel transferModel) 
        {
            Transfer entity = new Transfer();
            List<Data.File> files = new List<Data.File>();

            if (transferModel != null)
            {
                entity = transferModel.ModelToEntityTransfer()!;

                _context.Transfers.Add(entity);
                _context.SaveChanges();

                    foreach (FileModel f in transferModel.Files)
                    {
                        Data.File file = new Data.File() {
                            TransferId = entity.TransferId,
                            Comment = f.FileComment,
                            FileName = f.FileName,
                            FileSent = f.FileSent,
                            FileSize = f.FileSize,
                            FileExt = f.FileExt,
                        };
                        
                        _context.Files.Add(file);
                        _context.SaveChanges();
                    }
            }

            return transferModel;

        }

        public TransferModel UpdateTransfer(Models.TransferModel transferModel)
        {
            Transfer entity = new Transfer();
            List<Data.File> files = new List<Data.File>();

            if (transferModel != null)
            {
                entity = transferModel.ModelToEntityTransfer()!;

                _context.Transfers.Update(entity);
                _context.SaveChanges();

                foreach (FileModel f in transferModel.Files)
                {
                    Data.File file = new Data.File()
                    {
                        TransferId = entity.TransferId,
                        Comment = f.FileComment,
                        FileName = f.FileName,
                        FileSent = f.FileSent,
                        FileSize = f.FileSize,
                        FileExt = f.FileExt,
                    };

                    _context.Files.Update(file);
                    _context.SaveChanges();
                }
            }

            return transferModel;

        }
        [CustomAuthorize(Roles = Constants.Admin)]
        public async Task<TransferModel> RemoveTransferAsync(Models.TransferModel transferModel)
        {
            var transfer = _context.Transfers.Where(t=> t.TransferId == transferModel.TransferId).FirstOrDefault();    

            if (transfer != null)
            {
                _context.Transfers.Remove(transfer);
                
            }
            await _context.SaveChangesAsync();

            return transferModel;
        }

        private void SaveEntity()
        {
            _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Transfers.AnyAsync(t => t.TransferId == id);
        }

        public async Task UpdateSpillIdForTransferAsync(int transferId, int spillId)
        {
            var transfer = await _context.Transfers.FirstOrDefaultAsync(t => t.TransferId == transferId);

            if (transfer != null)
            {
                if (spillId == 0)
                {
                    transfer.SpillId = null;
                }
                else
                {
                    transfer.SpillId = spillId;
                }
                
                await _context.SaveChangesAsync();
            }
        }
    }
}
