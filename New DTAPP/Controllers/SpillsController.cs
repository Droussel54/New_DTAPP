using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using New_DTAPP.Models;
using New_DTAPP.Data;
using New_DTAPP.Repository.Interfaces;
using New_DTAPP.Repository;
using New_DTAPP.Security;
using static New_DTAPP.Security.UserRoleProvider;
using Microsoft.AspNetCore.Mvc.Rendering;
using DocumentFormat.OpenXml.Spreadsheet;

namespace New_DTAPP.Controllers
{
    [CustomAuthorize(Roles = Constants.Admin + "," + Constants.Supervisor)]
    public class SpillsController : Controller
    {
        private readonly ISpillRepository _spillRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISystemRepository _systemRepository;
        private readonly ITransferRepository _transferRepository;

        [ActivatorUtilitiesConstructor]
        public SpillsController(ISpillRepository spillRepository, IUserRepository userRepository, ISystemRepository systemRepository, ITransferRepository transferRepository)
        {
            _spillRepository = spillRepository;
            _userRepository = userRepository;
            _systemRepository = systemRepository;
            _transferRepository = transferRepository;
        }

        private async Task<Models.UserModel?> GetCurrentUser()
        {

            if (User.Identity == null || User.Identity.Name == null) return null;
            string username = RemoveDomainFromUsername(User.Identity.Name);
            Models.UserModel? user = null;
            if (string.IsNullOrEmpty(username)) return null;
            user = await _userRepository.GetUserByUsernameAsync(username);
            return user;

        }

        // GET: Spills
        public async Task<IActionResult> Index()
        {
            var spills = await _spillRepository.GetAllSpillsAsync();

            return View(spills);
        }

        // GET: Spills/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var spill = await _spillRepository.GetSpillByIdAsync(id);

            if (spill == null)
            {
                return NotFound();
            }

            return View(spill);
        }

        // GET: Spills/Create
        [HttpGet]
        [CustomAuthorize(Roles = Constants.Admin)]
        public async Task<IActionResult> Create(int? transferId)
        {
            var model = new SpillModel();

            if (transferId.HasValue)
            {
                model.TransferId = transferId.Value;
                ViewData["TransferId"] = transferId.Value;

                var transfer = await _transferRepository.GetTransferByIdAsync(transferId!.Value);

                model.TransferRequestCompleted = transfer!.Urgent;
                ViewData["TransferRequestCompleted"] = transfer!.Urgent;

                model.DateOfSpill = transfer.RequestCreatedAt;
                ViewData["DateOfSpill"] = transfer.RequestCreatedAt;

                model.TimeOfSpill = transfer.SentTime;
                ViewData["TimeOfSpill"] = transfer.SentTime;

                model.TimeIdentifiedSpill = transfer.RequestCreatedAt;
                ViewData["TimeIdentifiedSpill"] = transfer.RequestCreatedAt;
            }

            UserModel? user = await GetCurrentUser();
            ViewData["CurrentUser"] = user;
            model.SpecialistId = user!.UserId;

            ViewData["SpecialistUserId"] = new SelectList(await _userRepository.GetAllUsersAsync(true), "UserId", "Username", user?.UserId);
            ViewData["ReviewerUserId"] = new SelectList(await _userRepository.GetAllUsersAsync(true), "UserId", "Username");
            ViewData["SystemsInvolved"] = new SelectList(await _systemRepository.GetAllSystemsAsync(), "SystemId", "SystemName");

            return View(model);
        }

        // POST: Spills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = Constants.Admin)]
        public async Task<IActionResult> Create([Bind("SpillId,BurnedAndAnnotated,IssoInformed,ManagerInformed,NatureOfSpill,TransferRequestCompleted,EmailTripleDeleted,ClientInformed," +
            "ConsiderationPowerDown,CDSent,DateOfSpill,TimeOfSpill,TimeIdentifiedSpill,TimeReported," +
            "WorkstationAffected,WorkstationAssetNumber,SpecialistId,ReviewerId,SystemsInvolved,TransferId")] Models.SpillModel spill)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _spillRepository.AddSpillAsync(spill);

                    var spills = await _spillRepository.GetAllSpillsAsync();

                    foreach (var s in spills)
                    {
                        if (s.TransferId == spill.TransferId)
                        {
                            await _transferRepository.UpdateSpillIdForTransferAsync(spill.TransferId, s.SpillId);
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpillExists(spill.SpillId).Result)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            await LoadCommonViewData();

            return View(spill);
        }

        // GET: Spills/Edit/5
        [CustomAuthorize(Roles = Constants.Admin)]
        public async Task<IActionResult> Edit(int id)
        {
            var spill = await _spillRepository.GetSpillByIdAsync(id);

            if (spill == null)
            {
                return NotFound();
            }

            var user = await GetCurrentUser();
            ViewData["CurrentUser"] = user;

            ViewData["SpecialistUserId"] = new SelectList(await _userRepository.GetAllUsersAsync(true), "UserId", "Username", user?.UserId);
            ViewData["ReviewerUserId"] = new SelectList(await _userRepository.GetAllUsersAsync(true), "UserId", "Username");
            ViewData["SystemsInvolved"] = new SelectList(await _systemRepository.GetAllSystemsAsync(), "SystemId", "SystemName");

            return View(spill);
        }

        // POST: Spills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = Constants.Admin)]
        public async Task<IActionResult> Edit(int? id, [Bind("SpillId,BurnedAndAnnotated,IssoInformed,ManagerInformed,NatureOfSpill,TransferRequestCompleted,EmailTripleDeleted,ClientInformed," +
            "ConsiderationPowerDown,CDSent,DateOfSpill,TimeOfSpill,TimeIdentifiedSpill,TimeReported," +
            "WorkstationAffected,WorkstationAssetNumber,SpecialistId,ReviewerId,SystemsInvolved,TransferId")] Models.SpillModel spill)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _spillRepository.UpdateSpillAsync(spill);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await SpillExists(spill.SpillId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            await LoadCommonViewData();

            return View(spill);
        }

        // GET: Spills/Delete/5
        [CustomAuthorize(Roles = Constants.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            var spill = await _spillRepository.GetSpillByIdAsync(id);

            if (spill == null)
            {
                return NotFound();
            }

            await _transferRepository.UpdateSpillIdForTransferAsync(spill.TransferId, 0);

            return View(spill);
        }

        // POST: FileExtentions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = Constants.Admin)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var spill = await _spillRepository.GetSpillByIdAsync(id);

            if (spill != null)
            {
                await _spillRepository.RemoveSpillAsync(spill);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<bool> SpillExists(int id)
        {
            return await _spillRepository.SpillExistsAsync(id);
        }

        private async Task LoadCommonViewData()
        {
            ViewData["CurrentUser"] = await GetCurrentUser();
            ViewData["SpecialistUserId"] = new SelectList(await _userRepository.GetAllUsersAsync(), "UserId", "Username");
            ViewData["ReviewerUserId"] = new SelectList(await _userRepository.GetAllUsersAsync(), "UserId", "Username");
            ViewData["SystemsInvolved"] = new SelectList(await _systemRepository.GetAllSystemsAsync(), "SystemId", "SystemName");
        }
    }
}