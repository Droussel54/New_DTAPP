using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using New_DTAPP.Data;
using New_DTAPP.Repository;
using New_DTAPP.Repository.Interfaces;
using New_DTAPP.Security;

namespace New_DTAPP.Controllers
{
    [CustomAuthorize(Roles = Constants.Admin)]
    public class TransferTypeController : Controller
    {

        private readonly ITransferTypeRepository _transferTypeRepository;
        public TransferTypeController(ITransferTypeRepository transferTypeRepository)
        {
            _transferTypeRepository = transferTypeRepository;
        }
        // GET: Transfer Types
        public async Task<IActionResult> Index()
        {
            var transferTypes = await _transferTypeRepository.GetAllTransferTypeAsync(false);

            return View(transferTypes);
        }
        public async Task<IActionResult> Details(int id)
        {
            var transferType = await _transferTypeRepository.GetTransferTypeByIdAsync(id);

            if (transferType == null)
            {
                return NotFound();
            }

            return View(transferType);
        }

        // GET: TransferType/Create
        public IActionResult Create()
        {
            return View();
        }

        //// POST: TransferType/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransferTypeId,TransferTypeDesc,Archived,Ordering")] Models.TransferTypeModel transferType)
        {
            if (ModelState.IsValid)
            {
                await _transferTypeRepository.AddTransferTypeAsync(transferType);

                return RedirectToAction(nameof(Index));
            }

            return View(transferType);
        }

        //// GET: TransferType/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var transferType = await _transferTypeRepository.GetTransferTypeByIdAsync(id);

            if (transferType == null)
            {
                return NotFound();
            }

            return View(transferType);
        }

        //// POST: TransferType/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("TransferTypeId,TransferTypeDesc,Archived,Ordering")] Models.TransferTypeModel transferType)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _transferTypeRepository.UpdateTransferTypeAsync(transferType);
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
            return View(transferType);
        }

        // GET: Units/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await _transferTypeRepository.GetAllTransferTypeAsync(false) == null)
            {
                return NotFound();
            }

            var transferType = await _transferTypeRepository.GetTransferTypeByIdAsync(id.Value);
            if (transferType == null)
            {
                return NotFound();
            }

            return View(transferType);
        }

        // POST: Units/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _transferTypeRepository.GetAllTransferTypeAsync(false) == null)
            {
                return Problem("Entity set 'NewDtappContext.TransferType'  is null.");
            }

            var transferType = await _transferTypeRepository.GetTransferTypeByIdAsync(id);

            if (transferType != null)
            {
                await _transferTypeRepository.RemoveTransferTypeAsync(transferType);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool? TransferTypeExists(int id)
        {
            return _transferTypeRepository.TransferTypeExistsAsync(id);
        }
    }
}

