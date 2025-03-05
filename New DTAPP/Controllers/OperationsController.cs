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
    public class OperationsController : Controller
    {
        private readonly IOperationRepository _operationRepository;
        public OperationsController(IOperationRepository operationRepository)
        {
            _operationRepository = operationRepository;
        }

        // GET: Operations
        public async Task<IActionResult> Index()
        {
            var operations = await _operationRepository.GetAllOperationsAsync();

            return View(operations);
        }

        // GET: Operations/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var operation = await _operationRepository.GetOperationByIdAsync(id);

            if (operation == null)
            {
                return NotFound();
            }

            return View(operation);
        }

        // GET: Operations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Operations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OperationId,OperationName,Archived")] Models.OperationModel operation)
        {
            if (ModelState.IsValid)
            {
                await _operationRepository.AddOperationAsync(operation);

                return RedirectToAction(nameof(Index));
            }

            return View(operation);
        }

        // GET: Operations/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var operation = await _operationRepository.GetOperationByIdAsync(id);

            if (operation == null)
            {
                return NotFound();
            }

            return View(operation);
        }

        // POST: Operations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("OperationId,OperationName,Archived")] Models.OperationModel operation)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _operationRepository.UpdateOperationAsync(operation);
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
            return View(operation);
        }

        // GET: Operations/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var operation = await _operationRepository.GetOperationByIdAsync(id);

            if (operation == null)
            {
                return NotFound();
            }

            return View(operation);
        }

        // POST: Operations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _operationRepository.GetAllOperationsAsync() == null)
            {
                return Problem("Entity set 'Units' is null.");
            }

            var operation = await _operationRepository.GetOperationByIdAsync(id);

            if (operation != null)
            {
                await _operationRepository.RemoveOperationAsync(operation);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool? UnitExists(int id)
        {
            return _operationRepository.OperationExistsAsync(id);
        }
    }
}