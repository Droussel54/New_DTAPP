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
    public class SystemsController : Controller
    {
        private readonly ISystemRepository _systemRepository;
        public SystemsController(ISystemRepository systemRepository)
        {
            _systemRepository = systemRepository;
        }

        // GET: Systems
        public async Task<IActionResult> Index()
        {
            var systems = await _systemRepository.GetAllSystemsAsync();

            return View(systems);
        }

        // GET: Systems/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var system = await _systemRepository.GetSystemByIdAsync(id);

            if (system == null)
            {
                return NotFound();
            }

            return View(system);
        }

        // GET: Systems/Create
        [CustomAuthorize(Roles = Constants.Admin)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Systems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = Constants.Admin)]
        public async Task<IActionResult> Create([Bind("SystemId,SystemName,Archived")] Models.SystemModel system)
        {
            if (ModelState.IsValid)
            {
                await _systemRepository.AddSystemAsync(system);

                return RedirectToAction(nameof(Index));
            }

            return View(system);
        }

        // GET: Systems/Edit/5
        [CustomAuthorize(Roles = Constants.Admin)]
        public async Task<IActionResult> Edit(int id)
        {
            var system = await _systemRepository.GetSystemByIdAsync(id);

            if (system == null)
            {
                return NotFound();
            }

            return View(system);
        }

        // POST: Systems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = Constants.Admin)]
        public async Task<IActionResult> Edit(int? id, [Bind("SystemId,SystemName,Archived")] Models.SystemModel system)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _systemRepository.UpdateSystemAsync(system);
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
            return View(system);
        }

        // GET: Systems/Delete/5
        [CustomAuthorize(Roles = Constants.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            var system = await _systemRepository.GetSystemByIdAsync(id);

            if (system == null)
            {
                return NotFound();
            }

            return View(system);
        }

        // POST: Systems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = Constants.Admin)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _systemRepository.GetAllSystemsAsync() == null)
            {
                return Problem("Entity set 'Units' is null.");
            }

            var system = await _systemRepository.GetSystemByIdAsync(id);

            if (system != null)
            {
                await _systemRepository.RemoveSystemAsync(system);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool? UnitExists(int id)
        {
            return _systemRepository.SystemExistsAsync(id);
        }
    }
}