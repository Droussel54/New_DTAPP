using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using New_DTAPP.Models;
using New_DTAPP.Data;
using New_DTAPP.Repository.Interfaces;
using New_DTAPP.Security;

namespace New_DTAPP.Controllers
{
    [CustomAuthorize(Roles = Constants.Admin + "," + Constants.Supervisor)]
    public class UnitsController : Controller
    {
        private readonly IUnitRepository _unitRepository;
        public UnitsController(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }

        // GET: Units
        public async Task<IActionResult> Index()
        {
            var units = await _unitRepository.GetAllUnitsAsync();

            return View(units);
        }

        // GET: Units/Details/5
        public async Task<IActionResult> Details(int id)
        {
         var unit = await _unitRepository.GetUnitByIdAsync(id);

            if (unit == null)
            {
                return NotFound();
            }

            return View(unit);
        }

        // GET: Units/Create
        public IActionResult Create()
        {
            return View();
        }

        //// POST: Units/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UnitId,UnitName,Archived")] Models.UnitModel unit)
        {
            if (ModelState.IsValid)
            {
                await _unitRepository.AddUnitAsync(unit);

                return RedirectToAction(nameof(Index));
            }

            return View(unit);
        }

        //// GET: Units/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var unit = await _unitRepository.GetUnitByIdAsync(id);

            if (unit == null)
            {
                return NotFound();
            }

            return View(unit);
        }

        //// POST: Units/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("UnitId,UnitName,Archived")] Models.UnitModel unit)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                  await _unitRepository.UpdateUnitAsync(unit);
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
            return View(unit);
        }

        // GET: Units/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await _unitRepository.GetAllUnitsAsync() == null)
            {
                return NotFound();
            }

            var unit = await _unitRepository.GetUnitByIdAsync(id.Value);
            if (unit == null)
            {
                return NotFound();
            }

            return View(unit);
        }

        // POST: Units/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _unitRepository.GetAllUnitsAsync() == null)
            {
                return Problem("Entity set 'NewDtappContext.Units'  is null.");
            }

            var unit = await _unitRepository.GetUnitByIdAsync(id);

            if (unit != null)
            {
                await _unitRepository.RemoveUnitAsync(unit);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool? UnitExists(int id)
        {
            return _unitRepository.UnitExistsAsync(id);
        }
    }
}
