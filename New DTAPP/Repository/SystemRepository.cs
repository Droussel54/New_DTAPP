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
    public class SystemRepository : ISystemRepository
    {
        private readonly IConfiguration _configuration;
        private readonly NewDtappContext _context;

        public SystemRepository(NewDtappContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<SystemModel?> AddSystemAsync(SystemModel model)
        {

            Data.System system = new Data.System();

            if (system != null)
            {
                system.SystemName = model.SystemName;
                system.Archived = model.Archived;

                _context.Systems.Add(system);

                await _context.SaveChangesAsync();

                return system.EntityToModelSystem()!;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<SystemModel>> GetAllSystemsAsync(bool excludeArchive, bool addBlankItemForDropDown = false)
        {
            List<SystemModel> model = new();

            if (excludeArchive)
            {
                var r = await _context.Systems.Where(i => i.Archived != true).OrderBy(s => s.SystemName).ToListAsync();
                model = r.EntityToModelSystem()!;
            }
            else
            {
                var r = await _context.Systems.OrderBy(s => s.SystemName).ToListAsync();
                model = r.EntityToModelSystem()!;
              
            }

            if (addBlankItemForDropDown) model.Insert(0, new SystemModel() { SystemId = 0, SystemName = " " });

            return model;
        }

        public async Task<SystemModel?> GetSystemByIdAsync(int id)
        {
            SystemModel model = new();

            var system = await _context.Systems.FirstOrDefaultAsync(t => t.SystemId == id);

            model = system?.EntityToModelSystem()!;

            return model;
        }

        public async Task<SystemModel?> UpdateSystemAsync(SystemModel systemModel)
        {
            var system = await _context.Systems.FirstOrDefaultAsync(t => t.SystemId == systemModel.SystemId);

            if (system != null)
            {

                system.SystemName = systemModel.SystemName;
                system.Archived = systemModel.Archived;

                //TODO: Test this.
                //_context.Set<System>().Update(system);

                await _context.SaveChangesAsync();

                return system?.EntityToModelSystem()!;

            }
            else
            {
                return null;
            }
        }

        public async Task<SystemModel?> RemoveSystemAsync(SystemModel systemModel)
        {
            var system = await _context.Systems.FirstOrDefaultAsync(t => t.SystemId == systemModel.SystemId);

            if (system != null)
            {
                _context.Systems.Remove(system);

                await _context.SaveChangesAsync();

                return system?.EntityToModelSystem()!;
            }
            else
            {
                return null;
            }
        }

        public bool? SystemExistsAsync(int id)
        {
            return _context.Units.Any(u => u.UnitId == id);
        }
    }
}
