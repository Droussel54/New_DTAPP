using Microsoft.EntityFrameworkCore;
using New_DTAPP.Controllers;
using New_DTAPP.Data;
using New_DTAPP.Extensions;
using New_DTAPP.Models;
using New_DTAPP.Repository.Interfaces;
using System.Security.Cryptography.Xml;
using System.Xml.Linq;

namespace New_DTAPP.Repository
{
    public class TransferTypeRepository : ITransferTypeRepository
    {
        private readonly IConfiguration _configuration;
        private readonly NewDtappContext _context;

        public TransferTypeRepository(NewDtappContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<TransferTypeModel?> AddTransferTypeAsync(TransferTypeModel model)
        {

            Data.TransferType transferType = new Data.TransferType();

            if (transferType != null)
            {
                transferType.TransferTypeId = model.TransferTypeId;
                transferType.TransferTypeDesc = model.TransferTypeDesc;
                transferType.Archived = model.Archived;
                transferType.Ordering = model.Ordering;

                _context.TransferType.Add(transferType);

                await _context.SaveChangesAsync();

                return transferType.EntityToModelTransferType()!;
            }
            else
            {
                return null;
            }
        }


        public async Task<List<TransferTypeModel>> GetAllTransferTypeAsync(bool? excludeArchive)
        {
            List<TransferTypeModel> model = new();
            var transferType = await _context.TransferType.Where(x => x.Archived != true).OrderBy(o => o.Ordering).ToListAsync();
            if (excludeArchive == null || excludeArchive.Value == false)
            {
                transferType = await _context.TransferType.OrderBy(o => o.Ordering).ToListAsync();
            }


            model = transferType.EntityToModelTransferType()!;

            return model;
        }

        public async Task<TransferTypeModel?> GetTransferTypeByIdAsync(int id)
        {
            TransferTypeModel model = new();

            var transferType = await _context.TransferType.FirstOrDefaultAsync(t => t.TransferTypeId == id);

            model = transferType?.EntityToModelTransferType()!;

            return model;
        }

        public async Task<TransferTypeModel?> UpdateTransferTypeAsync(TransferTypeModel transferTypeModel)
        {
            var transferType = await _context.TransferType.FirstOrDefaultAsync(t => t.TransferTypeId == transferTypeModel.TransferTypeId);

            if (transferType != null)
            {

                transferType.TransferTypeDesc = transferTypeModel.TransferTypeDesc;
                transferType.Ordering = transferTypeModel.Ordering;
                transferType.Archived = transferTypeModel.Archived;

                //TODO: Test this.
                //_context.Set<TransferType>().Update(transferType);
                
                await _context.SaveChangesAsync();

                return transferType?.EntityToModelTransferType()!;

            }
            else
            {
                return null;
            }
        }

        public async Task<TransferTypeModel?> RemoveTransferTypeAsync(TransferTypeModel transferTypeModel)
        {
            var transferType = await _context.TransferType.FirstOrDefaultAsync(t => t.TransferTypeId == transferTypeModel.TransferTypeId);

            if (transferType != null)
            {
                _context.TransferType.Remove(transferType);

                await _context.SaveChangesAsync();

                return transferType?.EntityToModelTransferType()!;

            }
            else
            {
                return null;
            }
        }

        public bool? TransferTypeExistsAsync(int id)
        {
            return _context.TransferType.Any(t => t.TransferTypeId == id);
        }

    }
}
