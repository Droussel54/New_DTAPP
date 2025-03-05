using Microsoft.EntityFrameworkCore;
using New_DTAPP.Controllers;
using New_DTAPP.Data;
using New_DTAPP.Extensions;
using New_DTAPP.Models;
using New_DTAPP.Repository.Interfaces;


namespace New_DTAPP.Repository
{
    public class FileRepository : IFileRepository
    {
        private readonly IConfiguration _configuration;
        private readonly NewDtappContext _context;

        public FileRepository(NewDtappContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<FileModel?> AddFileAsync(FileModel fileModel)
        {
            var file = await _context.Files.FirstOrDefaultAsync(t => t.FileId == fileModel.FileId);

            if (file != null)
            {
                file.FileName = fileModel.FileName;
                

                await _context.SaveChangesAsync();

                return file?.EntityToModelFile()!;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<FileModel>> GetAllFilesAsync()
        {
            List<FileModel> model = new();

            var files = await _context.Files.ToListAsync();

            model = files.EntityToModelFile()!;

            return model;
        }

        public async Task<List<FileModel>> GetFilesByTransferIdAsync(int id)
        {
            List<FileModel> model = new();

            var files = await _context.Files.Where(f => f.TransferId == id).ToListAsync();

            model = files.EntityToModelFile()!;

            return model;
        }

        public async Task<FileModel?> GetFileByIdAsync(int id)
        {
            FileModel model = new();

            var file = await _context.Files.FirstOrDefaultAsync(t => t.FileId == id);

            model = file?.EntityToModelFile()!;

            return model;
        }

        public async Task<FileModel?> UpdateFileAsync(FileModel fileModel)
        {
            var file = await _context.Files.FirstOrDefaultAsync(t => t.FileId == fileModel.FileId);

            if (file != null)
            {

                file.FileName = fileModel.FileName;

                //TODO: Test this.
                _context.Set<Data.File>().Update(file);

                await _context.SaveChangesAsync();

                return file?.EntityToModelFile()!;

            }
            else
            {
                return null;
            }
        }

        public async Task<FileModel?> RemoveFileAsync(FileModel fileModel)
        {
            var file = await _context.Files.FirstOrDefaultAsync(t => t.FileId == fileModel.FileId);

            if (file != null)
            {
                _context.Files.Remove(file);

                await _context.SaveChangesAsync();

                return file?.EntityToModelFile()!;

            }
            else
            {
                return null;
            }
        }

        public async Task<bool?> RemoveFilesByTransferIdAsync(int id)
        {
            var files = await _context.Files.Where(f => f.TransferId == id).ToListAsync();

            if (files != null)
            {
                foreach (var file in files)
                {
                    _context.Files.Remove(file);
                }
                await _context.SaveChangesAsync();

                return true;

            }
            else
            {
                return null;
            }
        }
    }
}
