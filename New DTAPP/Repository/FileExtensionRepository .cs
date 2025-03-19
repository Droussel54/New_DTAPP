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
    public class FileExtensionRepository : IFileExtensionRepository
    {
        private readonly IConfiguration _configuration;
        private readonly NewDtappContext _context;

        public FileExtensionRepository(NewDtappContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<FileExtensionModel?> AddFileExtensionAsync(FileExtensionModel model)
        {

            Data.FileExtension fileExt = new Data.FileExtension();

            if (fileExt != null)
            {
                fileExt.FileExtensionName = model.FileExtensionName;
                fileExt.Archived = model.Archived;

                _context.FileExtensions.Add(fileExt);

                await _context.SaveChangesAsync();

                return fileExt.EntityToModelFileExtension()!;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<FileExtensionModel>> GetAllFileExtensionsAsync(bool excludeArchive, bool addBlankItemForDropDown = false)
        {
            List<FileExtensionModel> model = new();

            if (excludeArchive)
            {
                var r = await _context.FileExtensions.Where(i => i.Archived != true).OrderBy(s => s.FileExtensionName).ToListAsync();
                model = r.EntityToModelFileExtension()!;
            }
            else
            {
                var r = await _context.FileExtensions.OrderBy(s => s.FileExtensionName).ToListAsync();
                model = r.EntityToModelFileExtension()!;

            }

            if (addBlankItemForDropDown) model.Insert(0, new FileExtensionModel() { FileExtensionId = 0, FileExtensionName = " " });

            return model;
        }

        public async Task<FileExtensionModel?> GetFileExtensionByIdAsync(int id)
        {
            FileExtensionModel model = new();

            var fileExt = await _context.FileExtensions.FirstOrDefaultAsync(t => t.FileExtensionId == id);

            model = fileExt?.EntityToModelFileExtension()!;

            return model;
        }

        public async Task<FileExtensionModel?> UpdateFileExtensionAsync(FileExtensionModel fileExtModel)
        {
            var fileExt = await _context.FileExtensions.FirstOrDefaultAsync(t => t.FileExtensionId == fileExtModel.FileExtensionId);

            if (fileExt != null)
            {

                fileExt.FileExtensionName = fileExtModel.FileExtensionName;
                fileExt.Archived = fileExtModel.Archived;

                //TODO: Test this.
                //_context.Set<System>().Update(system);

                await _context.SaveChangesAsync();

                return fileExt?.EntityToModelFileExtension()!;

            }
            else
            {
                return null;
            }
        }

        public async Task<FileExtensionModel?> RemoveFileExtensionAsync(FileExtensionModel fileExtModel)
        {
            var fileExt = await _context.FileExtensions.FirstOrDefaultAsync(t => t.FileExtensionId == fileExtModel.FileExtensionId);

            if (fileExt != null)
            {
                _context.FileExtensions.Remove(fileExt);

                await _context.SaveChangesAsync();

                return fileExt?.EntityToModelFileExtension()!;
            }
            else
            {
                return null;
            }
        }

        public bool? FileExtensionExistsAsync(int id)
        {
            return _context.FileExtensions.Any(u => u.FileExtensionId == id);
        }
    }
}
