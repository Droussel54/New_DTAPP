using Microsoft.EntityFrameworkCore;
using New_DTAPP.Data;
using New_DTAPP.Extensions;
using New_DTAPP.Models;
using New_DTAPP.Repository.Interfaces;

namespace New_DTAPP.Repository
{
    public class OperationRepository : IOperationRepository
    {
        private readonly IConfiguration _configuration;
        private readonly NewDtappContext _context;

        public OperationRepository(NewDtappContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
             
        public async Task<OperationModel?> AddOperationAsync(OperationModel model)
        {

            Operation operation = new Operation();

            if (operation != null)
            {
                operation.OperationName = model.OperationName;
                operation.Archived = model.Archived;

                _context.Operations.Add(operation);

                await _context.SaveChangesAsync();

                return operation?.EntityToModelOperation()!;
            }
            else
            {
                return null;
            }
        }
        
        public async Task<List<OperationModel>> GetAllOperationsAsync(bool excludeArchive, bool addBlankItemForDropDown = false)
        {
            List<OperationModel> model = new();

            if (excludeArchive)
            {
                var r = await _context.Operations.Where(i => i.Archived != true).OrderBy(o => o.OperationName).ToListAsync();
                model = r.EntityToModelOperation()!;
                
            }
            else
            {
                var r = await _context.Operations.OrderBy(o => o.OperationName).ToListAsync();
                model = r.EntityToModelOperation()!;
            }

            if (addBlankItemForDropDown) model.Insert(0, new OperationModel() { OperationId = 0, OperationName = " " });

            return model;
        }

        public async Task<OperationModel?> GetOperationByIdAsync(int id)
        {
            OperationModel model = new();
            
            var operation = await _context.Operations.FirstOrDefaultAsync(t => t.OperationId == id);

            model = operation?.EntityToModelOperation()!;

            return model;
        }

        public async Task<OperationModel?> RemoveOperationAsync(OperationModel model)
        {
            var operation = await _context.Operations.FirstOrDefaultAsync(t => t.OperationId == model.OperationId);

            if (operation != null)
            {
                _context.Operations.Remove(operation);

                await _context.SaveChangesAsync();

                return operation?.EntityToModelOperation()!;
            }
            else
            {
                return null;
            }
        }

        public async Task<OperationModel?> UpdateOperationAsync(OperationModel model)
        {
            var operation = await _context.Operations.FirstOrDefaultAsync(t => t.OperationId == model.OperationId);

            if (operation != null)
            {

                operation.OperationName = model.OperationName;
                operation.Archived = model.Archived;

                _context.Operations.Update(operation);

                await _context.SaveChangesAsync();

                return operation?.EntityToModelOperation()!;

            }
            else
            {
                return null;
            }
        }

        public bool? OperationExistsAsync(int id)
        {
            return _context.Operations.Any(u => u.OperationId == id);
        }


    }
}
