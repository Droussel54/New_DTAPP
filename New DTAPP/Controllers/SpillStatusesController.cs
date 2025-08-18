using Microsoft.AspNetCore.Mvc;
using New_DTAPP.Repository.Interfaces;
using New_DTAPP.Security;

namespace New_DTAPP.Controllers;

[CustomAuthorize(Roles = Constants.Admin + "," + Constants.Supervisor)]
public class SpillStatusesController : Controller
{
    private readonly ISpillStatusRepository _spillStatusRepository;

    [ActivatorUtilitiesConstructor]
    public SpillStatusesController(ISpillStatusRepository spillStatusRepository)
    {
        _spillStatusRepository = spillStatusRepository;
    }

    // GET: SpillStatuses
    [CustomAuthorize(Roles = Constants.Admin)]
    public async Task<IActionResult> Index()
    {
        var spillStatuses = await _spillStatusRepository.GetAllSpillStatusesAsync();

        return View(spillStatuses);
    }

    // GET: SpillStatuses/Details/5
    [CustomAuthorize(Roles = Constants.Admin)]
    public async Task<IActionResult> Details(int id)
    {
        var spillStatus = await _spillStatusRepository.GetSpillStatusByIdAsync(id);

        if (spillStatus == null)
        {
            return NotFound();
        }

        return View(spillStatus);
    }

    // GET: SpillStatuses/Create
    [CustomAuthorize(Roles = Constants.Admin)]
    public async Task<IActionResult> Create()
    {
        return View();
    }

    //// POST: SpillStatuses/Create
    //// To protect from overposting attacks, enable the specific properties you want to bind to.
    //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [CustomAuthorize(Roles = Constants.Admin)]
    public async Task<IActionResult> Create([Bind("SpillStatusId,SpillStatusDesc,Archived")] Models.SpillStatusModel spillStatus)
    {
        if (ModelState.IsValid)
        {
            await _spillStatusRepository.AddSpillStatusAsync(spillStatus);

            return RedirectToAction(nameof(Index));
        }

        return View(spillStatus);
    }

    //// GET: SpillStatuses/Edit/5
    [CustomAuthorize(Roles = Constants.Admin)]
    public async Task<IActionResult> Edit(int id)
    {
        var spillStatus = await _spillStatusRepository.GetSpillStatusByIdAsync(id);

        if (spillStatus == null)
        {
            return NotFound();
        }

        return View(spillStatus);
    }

    //// POST: SpillStatuses/Edit/5
    //// To protect from overposting attacks, enable the specific properties you want to bind to.
    //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [CustomAuthorize(Roles = Constants.Admin)]
    public async Task<IActionResult> Edit(int? id, [Bind("SpillStatusId,SpillStatusDesc,Archived")] Models.SpillStatusModel spillStatus)
    {
        if (id == null)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _spillStatusRepository.UpdateSpillStatusAsync(spillStatus);

            return RedirectToAction(nameof(Index));
        }

        return View(spillStatus);
    }

    // GET: SpillStatuses/Delete/5
    [CustomAuthorize(Roles = Constants.Admin)]
    public async Task<IActionResult> Delete(int id)
    {
        var spillStatus = await _spillStatusRepository.GetSpillStatusByIdAsync(id);

        if (spillStatus == null)
        {
            return NotFound();
        }

        return View(spillStatus);
    }

    // POST: SpillStatuses/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [CustomAuthorize(Roles = Constants.Admin)]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var spillStatus = await _spillStatusRepository.GetSpillStatusByIdAsync(id);

        if (spillStatus != null)
        {
            await _spillStatusRepository.RemoveSpillStatusAsync(spillStatus);
        }

        return RedirectToAction(nameof(Index));
    }
}