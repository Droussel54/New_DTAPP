
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
    public class UnitRepository : IUnitRepository
    {
        private readonly IConfiguration _configuration;
        private readonly NewDtappContext _context;
        public UnitRepository(NewDtappContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<UnitModel?> AddUnitAsync(UnitModel unitModel)
        {

            Unit unit = new Unit();

            if (unit != null)
            {
                unit.UnitName = unitModel.UnitName;
                unit.Archived = unitModel.Archived;
                
                _context.Units.Add(unit);

                await _context.SaveChangesAsync();

                return unit?.EntityToModelUnit()!;
            }
            else
            {
                return null; 
            }
        }

        public async Task<List<UnitModel>> GetAllUnitsAsync(bool excludeArchive, bool addBlankItemForDropDown = false) 
        {
            List<UnitModel> model = new();

            if (excludeArchive)
            {
                var r = await _context.Units.Where(i => i.Archived != true).OrderBy(u => u.UnitName).ToListAsync();
                model = r.EntityToModelUnit()!;
            }
            else
            {
                var r = await _context.Units.OrderBy(u => u.UnitName).ToListAsync();
                model = r.EntityToModelUnit()!;
            }

            if (addBlankItemForDropDown) model.Insert(0, new UnitModel() { UnitId = 0, UnitName = " " });

            return model;

        }

        public async Task<UnitModel?> GetUnitByIdAsync(int id)
        {
            UnitModel model = new();

            var unit = await _context.Units.FirstOrDefaultAsync(t => t.UnitId == id);

            model = unit?.EntityToModelUnit()!;

            return model;
        }

        public async Task<UnitModel?> UpdateUnitAsync(UnitModel unitModel)
        {
            var unit = await _context.Units.FirstOrDefaultAsync(t => t.UnitId == unitModel.UnitId);

            if (unit != null)
            {
                
                unit.UnitName = unitModel.UnitName;
                unit.Archived = unitModel.Archived;

                //TODO: Test this.
                _context.Set<Unit>().Update(unit);

                await _context.SaveChangesAsync();

                return unit?.EntityToModelUnit()!;

            }
            else
            {
                return null;
            }
        }

        public async Task<UnitModel?> RemoveUnitAsync(UnitModel unitModel)
        {
            var unit = await _context.Units.FirstOrDefaultAsync(t => t.UnitId == unitModel.UnitId);

            if (unit != null)
            {
                _context.Units.Remove(unit);
                
                await _context.SaveChangesAsync();

                return unit?.EntityToModelUnit()!;

            }
            else
            {
                return null;
            }
        }

        public  bool? UnitExistsAsync(int id)
        {
          return _context.Units.Any(u=> u.UnitId == id);
        }
    }
}
