using Microsoft.EntityFrameworkCore;
using New_DTAPP.Data;
using New_DTAPP.Extensions;
using New_DTAPP.Models;
using New_DTAPP.Repository.Interfaces;
using New_DTAPP.Security;

namespace New_DTAPP.Repository;

public class SpillStatusRepository : ISpillStatusRepository
{
    private readonly IConfiguration _configuration;
    private readonly NewDtappContext _context;

    public SpillStatusRepository(NewDtappContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<SpillStatusModel?> AddSpillStatusAsync(SpillStatusModel model)
    {
        SpillStatus entity = new SpillStatus();

        if (model != null)
        {
            entity = model.ModelToEntitySpillStatus()!;

            _context.SpillStatuses.Add(entity);
            await _context.SaveChangesAsync();
        }

        return model;
    }

    public async Task<List<SpillStatusModel>> GetAllSpillStatusesAsync(bool excludeArchived)
    {
        List<SpillStatusModel> spillStatusModel = new();

        if (excludeArchived)
        {
            var spillStatuses = await _context.SpillStatuses
                .Where(i => i.Archived != true)
                .OrderBy(o => o.SpillStatusId)
                .ToListAsync();
            spillStatusModel = spillStatuses?.EntityToModelSpillStatus()!;
        }
        else
        {
            var spillStatuses = await _context.SpillStatuses
                .OrderBy(o => o.SpillStatusId)
                .ToListAsync();
            spillStatusModel = spillStatuses?.EntityToModelSpillStatus()!;
        }

        return spillStatusModel;
    }

    public async Task<SpillStatusModel?> GetSpillStatusByIdAsync(int id)
    {
        SpillStatusModel? spillStatusModel = null;

        var spillStatuses = await _context.SpillStatuses
            .FirstOrDefaultAsync(t => t.SpillStatusId == id);

        spillStatusModel = spillStatuses?.EntityToModelSpillStatus();

        return spillStatusModel;
    }

    [CustomAuthorize(Roles = Constants.Admin)]
    public async Task<SpillStatusModel> RemoveSpillStatusAsync(SpillStatusModel model)
    {
        var spillStatus = _context.SpillStatuses.Where(t => t.SpillStatusId == model.SpillStatusId).FirstOrDefault();

        if (spillStatus != null)
        {
            _context.SpillStatuses.Remove(spillStatus);
        }

        await _context.SaveChangesAsync();

        return model;
    }

    public async Task<bool> SpillStatusExistsAsync(int id)
    {
        return await _context.SpillStatuses.AnyAsync(t => t.SpillStatusId == id);
    }

    public SpillStatusModel UpdateSpillStatusAsync(SpillStatusModel model)
    {
        SpillStatus entity = new SpillStatus();

        if (model != null)
        {
            entity = model.ModelToEntitySpillStatus();

            _context.SpillStatuses.Update(entity);
            _context.SaveChanges();
        }

        return model;
    }
}
