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
using DocumentFormat.OpenXml.Office2010.Excel;
using Newtonsoft.Json.Linq;
using DocumentFormat.OpenXml.Wordprocessing;
using X.PagedList;

namespace New_DTAPP.Controllers;

[CustomAuthorize(Roles = Constants.Admin + "," + Constants.Supervisor)]
public class SpillsController : Controller
{
    private readonly ISpillRepository _spillRepository;
    private readonly IUserRepository _userRepository;
    private readonly ISystemRepository _systemRepository;
    private readonly ITransferRepository _transferRepository;
    private readonly ISpillStatusRepository _spillStatusRepository;

    [ActivatorUtilitiesConstructor]
    public SpillsController(ISpillRepository spillRepository, IUserRepository userRepository, ISystemRepository systemRepository, ITransferRepository transferRepository, ISpillStatusRepository spillStatusRepository)
    {
        _spillRepository = spillRepository;
        _userRepository = userRepository;
        _systemRepository = systemRepository;
        _transferRepository = transferRepository;
        _spillStatusRepository = spillStatusRepository;
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
    [CustomAuthorize(Roles = Constants.Admin)]
    public async Task<IActionResult> Index(int? page, string? sortOrder, string? selectedSpillStatus, string? raisedAfter, string? raisedBefore,
                                            string? selectedCompletedUser, string? selectedOrigSystem, string? selectedDestSystem, bool? filterCurrentYear,
                                            bool? filterCurrentMonth, bool? filterCurrentWeek, string? filtered, string? clearFilters)
    {
        string cookieName = "DTAPPFilterSpills" + GetCurrentUser().Result.Username;
        if (!string.IsNullOrEmpty(clearFilters))
        {
            Response.Cookies.Delete(cookieName);
        }
        var cookieOptions = new CookieOptions();
        cookieOptions.Expires = DateTime.Now.AddDays(1);
        cookieOptions.Path = "/";
        var cookie = Request.Cookies[cookieName];
        if (!string.IsNullOrEmpty(filtered))
        {
            Response.Cookies.Append(cookieName, "{SortOrder:'" + sortOrder + 
                                                    "', SelectedSpillStatus:'" + selectedSpillStatus + 
                                                    "', RaisedAfter:'" + raisedAfter + 
                                                    "', RaisedBefore:'" + raisedBefore +
                                                    "', SelectedCompletedUser:'" + selectedCompletedUser +
                                                    "', SelectedOrigSystem:'" + selectedOrigSystem +
                                                    "', SelectedDestSystem:'" + selectedDestSystem +
                                                    "', FilterCurrentYear:'" + filterCurrentYear +
                                                    "', FilterCurrentMonth:'" + filterCurrentMonth +
                                                    "', FilterCurrentWeek:'" + filterCurrentWeek +
                                                    "'}", cookieOptions);
        }
        if (cookie != null && string.IsNullOrEmpty(filtered) && string.IsNullOrEmpty(clearFilters))
        {
            JObject json = JObject.Parse(cookie);
            sortOrder = (string?)json["SortOrder"];
            selectedSpillStatus = (string?)json["SelectedSpillStatus"];
            raisedAfter = (string?)json["RaisedAfter"];
            raisedBefore = (string?)json["RaisedBefore"];
            selectedCompletedUser = (string?)json["SelectedCompletedUser"];
            selectedOrigSystem = (string?)json["SelectedOrigSystem"];
            selectedDestSystem = (string?)json["SelectedDestSystem"];

            var filterCurrentYearStr = (string?)json["FilterCurrentYear"];
            filterCurrentYear = !string.IsNullOrEmpty(filterCurrentYearStr) ? bool.Parse(filterCurrentYearStr) : null;

            var filterCurrentMonthStr = (string?)json["FilterCurrentMonth"];
            filterCurrentMonth = !string.IsNullOrEmpty(filterCurrentMonthStr) ? bool.Parse(filterCurrentMonthStr) : null;

            var filterCurrentWeekStr = (string?)json["FilterCurrentWeek"];
            filterCurrentWeek = !string.IsNullOrEmpty(filterCurrentWeekStr) ? bool.Parse(filterCurrentWeekStr) : null;
        }

        ViewBag.SortOrder = sortOrder;
        ViewBag.SelectedSpillStatus = selectedSpillStatus;
        ViewBag.RaisedAfter = raisedAfter;
        ViewBag.RaiseBefore = raisedBefore;
        ViewBag.SelectedCompletedUser = selectedCompletedUser;
        ViewBag.SelectedOrigSystem = selectedOrigSystem;
        ViewBag.SelectedDestSystem = selectedDestSystem;
        ViewBag.FilterCurrentYear = filterCurrentYear;
        ViewBag.FilterCurrentMonth = filterCurrentMonth;
        ViewBag.FilterCurrentWeek = filterCurrentWeek;

        ViewData["CompletedUserId"] = new SelectList(await _userRepository.GetAllUsersAsync(true), "UserId", "Username");
        ViewData["OrigSystemId"] = new SelectList(await _systemRepository.GetAllSystemsAsync(true), "SystemId", "SystemName");
        ViewData["DestSystemId"] = new SelectList(await _systemRepository.GetAllSystemsAsync(true), "SystemId", "SystemName");
        ViewData["SpillStatusId"] = new SelectList(await _spillStatusRepository.GetAllSpillStatusesAsync(true), "SpillStatusId", "SpillStatusDesc");

        var spills = await _spillRepository.GetAllSpillsAsync();
        var q = spills.AsQueryable();
        
        if (!string.IsNullOrEmpty(selectedSpillStatus))
        {
            q = q.Where(u => u.SpillStatusId == Int32.Parse(selectedSpillStatus));
        }
        if (!string.IsNullOrEmpty(raisedAfter))
        {
            q = q.Where(u => u.DateOfSpill >= DateTime.Parse(raisedAfter));
        }
        if (!string.IsNullOrEmpty(raisedBefore))
        {
            q = q.Where(u => u.DateOfSpill <= DateTime.Parse(raisedBefore));
        }
        if (!string.IsNullOrEmpty(selectedCompletedUser))
        {
            q = q.Where(u => u.SpecialistId == Int32.Parse(selectedCompletedUser));
        }
        if (!string.IsNullOrEmpty(selectedOrigSystem))
        {
            q = q.Where(u => u.OrigSystemId == Int32.Parse(selectedOrigSystem));
        }
        if (!string.IsNullOrEmpty(selectedDestSystem))
        {
            q = q.Where(u => u.DestSystemId == Int32.Parse(selectedDestSystem));
        }
        if (filterCurrentYear.HasValue)
        {
            if (filterCurrentYear.Value)
            {
                q = q.Where(u => u.DateOfSpill.Value.Year == DateTime.Now.Year);
            }
        }
        if (filterCurrentMonth.HasValue)
        {
            if (filterCurrentMonth.Value)
            {
                q = q.Where(u => u.DateOfSpill.Value.Month == DateTime.Now.Month);
            }
        }
        if (filterCurrentWeek.HasValue)
        {
            if (filterCurrentWeek.Value)
            {
                var dateRangeTo = DateTime.Today.AddDays(1);
                var dateRangeFrom = DateTime.Today.AddDays(-7);
                q = q.Where(u => u.DateOfSpill.Value.Date >= dateRangeFrom && u.DateOfSpill.Value.Date < dateRangeTo);
            }
        }
        switch (sortOrder)
        {
            case "DateOfSpillAsc":
                q = q.OrderBy(t => t.DateOfSpill);
                break;
            case "DateOfSpillDesc":
                q = q.OrderByDescending(t => t.DateOfSpill);
                break;
            case "ReviewedUserAsc":
                q = q.OrderBy(t => t.ReviewerId);
                break;
            case "ReviewedUserDesc":
                q = q.OrderByDescending(t => t.ReviewerId);
                break;
            case "CompletedUserAsc":
                q = q.OrderBy(t => t.SpecialistId);
                break;
            case "CompletedUserDesc":
                q = q.OrderByDescending(t => t.SpecialistId);
                break;
            case "OriginatingSystemAsc":
                q = q.OrderBy(t => t.OrigSystemId);
                break;
            case "OriginatingSystemDesc":
                q = q.OrderByDescending(t => t.OrigSystemId);
                break;
            case "DestinationSystemAsc":
                q = q.OrderBy(t => t.DestSystemId);
                break;
            case "DestinationSystemDesc":
                q = q.OrderByDescending(t => t.DestSystemId);
                break;
            default:
                q = q.OrderByDescending(t => t.DateOfSpill);
                break;
        }

        ViewBag.TotalCount = q.Count();
        int pageNumber = page ?? 1;
        int pageSize = 50;

        if (GetCurrentUser().Result.Role.RoleName == Constants.Admin)
        {
            HttpContext.Response.Headers.Add("refresh", "60; url=" + Url.Action("Index", new
            {
                page,
                sortOrder = ViewBag.SortOrder,
                selectedSpillStatus = ViewBag.SelectedSpillStatus,
                raisedAfter = ViewBag.RaisedAfter,
                raisedBefore = ViewBag.RaisedBefore,
                selectedCompletedUser = ViewBag.SelectedCompletedUser,
                selectedOrigSystem = @ViewBag.SelectedOrigSystem,
                selectedDestSystem = @ViewBag.SelectedDestSystem
            }));
        }

        return View(await q.ToPagedListAsync(pageNumber, pageSize));
    }

    // GET: Spills/Details/5
    [CustomAuthorize(Roles = Constants.Admin)]
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

        UserModel? user = await GetCurrentUser();
        ViewData["CurrentUser"] = user;

        if (transferId.HasValue)
        {
            //model = await _spillRepository.GetSpillByTransferId(transferId!.Value);

            //if (model != null)
            //{
            //    return await Edit(model.SpillId, transferId);
            //}
            //else
            //{
            //    model = new SpillModel();
            //}

            model.TransferId = transferId.Value;
            ViewData["TransferId"] = transferId.Value;

            var transfer = await _transferRepository.GetTransferByIdAsync(transferId!.Value);

            model.TransferRequestCompleted = transfer!.Urgent;
            ViewData["TransferRequestCompleted"] = transfer!.Urgent;

            model.DateOfSpill = transfer.SentTime;
            ViewData["DateOfSpill"] = transfer.SentTime;

            model.TimeIdentifiedSpill = DateTime.Parse(DateTime.Now.ToShortTimeString());
            ViewData["TimeIdentifiedSpill"] = DateTime.Parse(DateTime.Now.ToShortTimeString());

            model.OrigSystemId = transfer.OrigSystemId;
            ViewData["OrigSystemId"] = transfer.OrigSystemId;
            ViewData["OrigSystem"] = transfer.OrigSystem?.SystemName;

            model.DestSystemId = transfer.DestSystemId;
            ViewData["DestSystemId"] = transfer.DestSystemId;
            ViewData["DestSystem"] = transfer.DestSystem?.SystemName;

        }

        model.SpecialistId = user!.UserId;
        ViewData["SpecialistUserId"] = new SelectList(await _userRepository.GetAllUsersAsync(true), "UserId", "Username", user?.UserId);
        ViewData["SpecialistUserName"] = user?.Username;
        ViewData["ReviewerUserId"] = new SelectList(await _userRepository.GetAllUsersAsync(true), "UserId", "Username");
        ViewData["OrigSystemId"] = new SelectList(await _systemRepository.GetAllSystemsAsync(true), "SystemId", "SystemName");
        ViewData["DestSystemId"] = new SelectList(await _systemRepository.GetAllSystemsAsync(true), "SystemId", "SystemName");
        ViewData["SpillStatusId"] = new SelectList(await _spillStatusRepository.GetAllSpillStatusesAsync(true), "SpillStatusId", "SpillStatusDesc");

        return View(model);
    }

    // POST: Spills/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [CustomAuthorize(Roles = Constants.Admin)]
    public async Task<IActionResult> Create([Bind("SpillId,SpillStatusId,CFNOCIncidentNumber,DGDSSIMIncidentNumber,BurnedAndAnnotated,IssoInformed,ManagerInformed,NatureOfSpill,TransferRequestCompleted,EmailTripleDeleted,ClientInformed," +
        "ConsiderationPowerDown,CDSent,DateOfSpill,TimeIdentifiedSpill," +
        "WorkstationAffected,WorkstationAssetNumber,SpecialistId,ReviewerId,OrigSystemId,DestSystemId,TransferId")] Models.SpillModel spill)
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
    public async Task<IActionResult> Edit(int id, int? transferId)
    {
        var spill = await _spillRepository.GetSpillByIdAsync(id);

        if (spill == null)
        {
            return NotFound();
        }

        var user = await GetCurrentUser();
        ViewData["CurrentUser"] = user;

        ViewData["SpecialistUserId"] = new SelectList(await _userRepository.GetAllUsersAsync(true), "UserId", "Username", user?.UserId);
        ViewData["SpecialistUserName"] = user?.Username;
        ViewData["ReviewerUserId"] = new SelectList(await _userRepository.GetAllUsersAsync(true), "UserId", "Username");
        ViewData["OrigSystemId"] = new SelectList(await _systemRepository.GetAllSystemsAsync(true), "SystemId", "SystemName");
        ViewData["OrigSystem"] = spill.OrigSystem?.SystemName;
        ViewData["DestSystemId"] = new SelectList(await _systemRepository.GetAllSystemsAsync(true), "SystemId", "SystemName");
        ViewData["DestSystem"] = spill.DestSystem?.SystemName;
        ViewData["SpillStatusId"] = new SelectList(await _spillStatusRepository.GetAllSpillStatusesAsync(true), "SpillStatusId", "SpillStatusDesc");
        ViewData["SpillIdForLink"] = id;
        ViewData["TransferIdFromLink"] = transferId;

        return View(spill);
    }

    // POST: Spills/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [CustomAuthorize(Roles = Constants.Admin)]
    public async Task<IActionResult> Edit(int? id, int? transferId, [Bind("SpillId,SpillStatusId,CFNOCIncidentNumber,DGDSSIMIncidentNumber,BurnedAndAnnotated,IssoInformed,ManagerInformed,NatureOfSpill,TransferRequestCompleted,EmailTripleDeleted,ClientInformed," +
        "ConsiderationPowerDown,CDSent,DateOfSpill,TimeIdentifiedSpill," +
        "WorkstationAffected,WorkstationAssetNumber,SpecialistId,ReviewerId,OrigSystemId,DestSystemId,TransferId")] Models.SpillModel spill)
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

                if (transferId != null)
                {
                    await _transferRepository.UpdateSpillIdForTransferAsync(transferId, id);
                }
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
        ViewData["SpecialistUserId"] = new SelectList(await _userRepository.GetAllUsersAsync(true), "UserId", "Username");
        ViewData["ReviewerUserId"] = new SelectList(await _userRepository.GetAllUsersAsync(true), "UserId", "Username");
        ViewData["OrigSystemId"] = new SelectList(await _systemRepository.GetAllSystemsAsync(true), "SystemId", "SystemName");
        ViewData["DestSystemId"] = new SelectList(await _systemRepository.GetAllSystemsAsync(true), "SystemId", "SystemName");
        ViewData["SpillStatusId"] = new SelectList(await _spillStatusRepository.GetAllSpillStatusesAsync(true), "SpillStatusId", "SpillStatusDesc");
    }
}