using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using New_DTAPP.Models;
using New_DTAPP.Data;
using New_DTAPP.Repository.Interfaces;
using New_DTAPP.Repository;
using New_DTAPP.Security;

namespace New_DTAPP.Controllers
{
    [CustomAuthorize(Roles = Constants.Admin + "," + Constants.Supervisor)]
    public class FileExtensionsController : Controller
    {
        private readonly IFileExtensionRepository _fileExtensionRepository;
        public FileExtensionsController(IFileExtensionRepository fileExtensionRepository)
        {
            _fileExtensionRepository = fileExtensionRepository;
        }

        // GET: FileExtensions
        public async Task<IActionResult> Index()
        {
            var fileExt = await _fileExtensionRepository.GetAllFileExtensionsAsync();

            return View(fileExt);
        }

        // GET: FileExtensions/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var fileExt = await _fileExtensionRepository.GetFileExtensionByIdAsync(id);

            if (fileExt == null)
            {
                return NotFound();
            }

            return View(fileExt);
        }

        // GET: FileExtensions/Create
        [CustomAuthorize(Roles = Constants.Admin)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: FileExtensions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = Constants.Admin)]
        public async Task<IActionResult> Create([Bind("FileExtensionId,FileExtensionName,Archived")] Models.FileExtensionModel fileExt)
        {
            if (ModelState.IsValid)
            {
                await _fileExtensionRepository.AddFileExtensionAsync(fileExt);

                return RedirectToAction(nameof(Index));
            }

            return View(fileExt);
        }

        // GET: FileExtensions/Edit/5
        [CustomAuthorize(Roles = Constants.Admin)]
        public async Task<IActionResult> Edit(int id)
        {
            var fileExt = await _fileExtensionRepository.GetFileExtensionByIdAsync(id);

            if (fileExt == null)
            {
                return NotFound();
            }

            return View(fileExt);
        }

        // POST: FileExtensions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = Constants.Admin)]
        public async Task<IActionResult> Edit(int? id, [Bind("FileExtensionId,FileExtensionName,Archived")] Models.FileExtensionModel fileExt)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _fileExtensionRepository.UpdateFileExtensionAsync(fileExt);
                }
                catch (DbUpdateConcurrencyException)
                {
                    //TODO: Complete processing this concurrency exception.
                    //if ( = null)
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    throw;
                    //}
                }
                return RedirectToAction(nameof(Index));
            }
            return View(fileExt);
        }

        // GET: FileExtensions/Delete/5
        [CustomAuthorize(Roles = Constants.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            var fileExt = await _fileExtensionRepository.GetFileExtensionByIdAsync(id);

            if (fileExt == null)
            {
                return NotFound();
            }

            return View(fileExt);
        }

        // POST: FileExtentions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = Constants.Admin)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _fileExtensionRepository.GetAllFileExtensionsAsync() == null)
            {
                return Problem("Entity set 'FileExtensions' is null.");
            }

            var fileExt = await _fileExtensionRepository.GetFileExtensionByIdAsync(id);

            if (fileExt != null)
            {
                await _fileExtensionRepository.RemoveFileExtensionAsync(fileExt);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool? FileExtensionExists(int id)
        {
            return _fileExtensionRepository.FileExtensionExistsAsync(id);
        }
    }
}