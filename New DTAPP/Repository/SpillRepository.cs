using Microsoft.EntityFrameworkCore;
using New_DTAPP.Data;
using New_DTAPP.Extensions;
using New_DTAPP.Models;
using New_DTAPP.Repository.Interfaces;
using New_DTAPP.Security;

namespace New_DTAPP.Repository;

public class SpillRepository : ISpillRepository
{
    private readonly IConfiguration _configuration;
    private readonly NewDtappContext _context;
    public SpillRepository(NewDtappContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<SpillModel?> AddSpillAsync(SpillModel model)
    {
        Spill entity = new Spill();

        if (model != null)
        {
            entity = model.ModelToEntitySpill()!;

            _context.Spills.Add(entity);
            await _context.SaveChangesAsync();
        }

        return model;
    }

    public async Task<List<SpillModel>> GetAllSpillsAsync()
    {
        List<SpillModel> spillModel = new();

        var spills = await _context.Spills
            .Include(t => t.SpecialistUser)
            .Include(t => t.ReviewerUser)
            .Include(t => t.Transfer)
            .AsNoTracking().ToListAsync();

        spillModel = spills?.EntityToModelSpill()!;

        return spillModel.ToList();
    }

    public async Task<SpillModel?> GetSpillByIdAsync(int id)
    {
        SpillModel? spillModel = null;

        var spill = await _context.Spills
            .Include(t => t.SpecialistUser)
            .Include(t => t.ReviewerUser)
            .FirstOrDefaultAsync(t => t.SpillId == id);

        spillModel = spill?.EntityToModelSpill();

        return spillModel;
    }

    public async Task<SpillModel?> GetSpillByTransferId(int transferId)
    {
        SpillModel? spillModel = null;

        var spill = await _context.Spills.Where(t => t.TransferId == transferId).FirstOrDefaultAsync();

        spillModel = spill?.EntityToModelSpill();

        return spillModel;
    }

    [CustomAuthorize(Roles = Constants.Admin)]
    public async Task<SpillModel> RemoveSpillAsync(SpillModel model)
    {
        var spill = _context.Spills.Where(t => t.SpillId == model.SpillId).FirstOrDefault();

        if (spill != null)
        {
            _context.Spills.Remove(spill);
        }

        await _context.SaveChangesAsync();

        return model;
    }

    public async Task<bool> SpillExistsAsync(int id)
    {
        return await _context.Spills.AnyAsync(t => t.SpillId == id);
    }

    public SpillModel UpdateSpillAsync(SpillModel model)
    {
        Spill entity = new Spill();

        if (model != null)
        {
            entity = model.ModelToEntitySpill();

            _context.Spills.Update(entity);
            _context.SaveChanges();
        }

        return model;
    }
}
