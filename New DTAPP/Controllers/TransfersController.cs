using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Net.Http.Headers;
using New_DTAPP.Models;
using New_DTAPP.Repository;
using New_DTAPP.Repository.Interfaces;
using New_DTAPP.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using X.PagedList;
using static New_DTAPP.Security.UserRoleProvider;

//using FileModel = New_DTAPP.Models.FileModel;
//using Azure.Identity;

namespace New_DTAPP.Controllers
{
    [CustomAuthorize(Roles = Constants.Admin +","+Constants.User+","+Constants.Supervisor)]
    public class TransfersController : Controller
    {
        private readonly IFileRepository _fileRepository;
        private readonly IOperationRepository _operationRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ISystemRepository _systemRepository;
        private readonly ITransferRepository _transferRepository;
        private readonly IUnitRepository _unitRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITransferTypeRepository _transferTypeRepository;
        private readonly IFileExtensionRepository _fileExtensionRepository;

        public TransfersController(IFileRepository fileRepository,
                                   IOperationRepository operationRepository, IRoleRepository roleRepository,
                                   ISystemRepository systemRepository, ITransferRepository transferRepository, 
                                   IUnitRepository unitRepository, IUserRepository userRepository, 
                                   ITransferTypeRepository transferTypeRepository, IFileExtensionRepository fileExtensionRepository)
        {
            _fileRepository = fileRepository;
            _operationRepository = operationRepository;
            _roleRepository = roleRepository;
            _systemRepository = systemRepository;
            _transferRepository = transferRepository;
            _unitRepository = unitRepository;
            _userRepository = userRepository;
            _transferTypeRepository = transferTypeRepository;
            _fileExtensionRepository = fileExtensionRepository;
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

        // GET: Transfers
        public async Task<IActionResult> Index(int? page, string? sortOrder, string? selectedUnit, string? raisedAfter, string? raisedBefore, 
                                                string? selectedCompletedUser, bool? filterSpillOccurred, bool? filterTransferDenied, string? filtered, string? clearFilters, 
                                                string? filterClientName, string? selectedOrigSystem, string? selectedDestSystem)
        {
            string cookieName = "DTAPPFilter"+ GetCurrentUser().Result.Username;
            if (!string.IsNullOrEmpty(clearFilters))
            {
                Response.Cookies.Delete(cookieName);
            }
            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(1);
            cookieOptions.Path = "/";
            var cookie = Request.Cookies[cookieName];
            if (!string.IsNullOrEmpty(filtered)){
                Response.Cookies.Append(cookieName  , "{SortOrder:'"+sortOrder+"', SelectedUnit:'"+selectedUnit+ "', RaisedAfter:'"+raisedAfter+ "', RaisedBefore:'"+raisedBefore+
                                                        "', SelectedCompletedUser:'"+selectedCompletedUser+ 
                                                        "', filterClientName:'"+filterClientName+
                                                        "', FilterSpillOccurred:'"+filterSpillOccurred+
                                                        "', FilterTransferDenied:'"+filterTransferDenied+
                                                        "', SelectedOrigSystem:'"+selectedOrigSystem+
                                                        "', SelectedDestSystem:'"+selectedDestSystem+"'}", cookieOptions);
            }
            if (cookie != null && string.IsNullOrEmpty(filtered) && string.IsNullOrEmpty(clearFilters))
            {
                JObject json = JObject.Parse(cookie);
                sortOrder = (string?)json["SortOrder"];
                selectedUnit = (string?)json["SelectedUnit"];
                raisedAfter = (string?)json["RaisedAfter"];
                raisedBefore = (string?)json["RaisedBefore"];
                selectedCompletedUser = (string?)json["SelectedCompletedUser"];
                filterSpillOccurred = (bool?)json["FilterSpillOccurred"];
                filterTransferDenied = (bool?)json["FilterTransferDenied"];
                filterClientName = (string?)json["filterClientName"];
                selectedOrigSystem = (string?)json["SelectedOrigSystem"];
                selectedDestSystem = (string?)json["SelectedDestSystem"];
            }
            ViewBag.UsernameSortParm = String.IsNullOrEmpty(sortOrder) ? "RequestCreatedAt" : "";
            ViewData["ClientUnitId"] = new SelectList(await _unitRepository.GetAllUnitsAsync(true, false), "UnitId", "UnitName");
            ViewBag.SortOrder = sortOrder;
            ViewBag.SelectedUnit = selectedUnit;
            ViewBag.RaisedAfter = raisedAfter;
            ViewBag.RaisedBefore = raisedBefore;
            ViewBag.SelectedCompletedUser = selectedCompletedUser;
            ViewBag.FilterSpillOccurred = filterSpillOccurred;
            ViewBag.FilterTransferDenied = filterTransferDenied;
            ViewBag.FilterClientName = filterClientName;
            ViewData["OrigSystemId"] = new SelectList(await _systemRepository.GetAllSystemsAsync(true, false), "SystemId", "SystemName");
            ViewData["DestSystemId"] = new SelectList(await _systemRepository.GetAllSystemsAsync(true, false), "SystemId", "SystemName");
            ViewBag.SelectedOrigSystem = selectedOrigSystem;
            ViewBag.SelectedDestSystem = selectedDestSystem;
            ViewData["CompletedUserId"] = new SelectList(await _userRepository.GetAllUsersAsync(), "UserId", "Username");          

            var transferModel = await _transferRepository.GetAllTransfersAsync();
            var q = transferModel.AsQueryable();
            if (!string.IsNullOrEmpty(selectedUnit))
            {
                q = q.Where(u => u.ClientUnitId == Int32.Parse(selectedUnit));
            }
            if (!string.IsNullOrEmpty(raisedAfter))
            {
                q = q.Where(u => u.RequestCreatedAt >= DateTime.Parse(raisedAfter));
            }
            if (!string.IsNullOrEmpty(raisedBefore))
            {
                q = q.Where(u => u.RequestCreatedAt <= DateTime.Parse(raisedBefore));
            }
            if (!string.IsNullOrEmpty(selectedCompletedUser))
            {
                q = q.Where(u => u.CompletedUserId == Int32.Parse(selectedCompletedUser));
            }
            if (!string.IsNullOrEmpty(filterClientName))
            {
                q = q.Where(u => u.ClientName.ToLower().Contains(filterClientName.ToLower()));
            }
            if (filterSpillOccurred.HasValue)
            {
                if (filterSpillOccurred.Value)
                {
                    q = q.Where(u => u.SpillOccurred == filterSpillOccurred.Value);
                }

            }
            if (filterTransferDenied.HasValue)
            {
                if (filterTransferDenied.Value)
                {
                    q = q.Where(u => u.Urgent == filterTransferDenied.Value);
                }

            }
            if (!string.IsNullOrEmpty(selectedOrigSystem))
            {
                q = q.Where(u => u.OrigSystemId == Int32.Parse(selectedOrigSystem));
            }
            if (!string.IsNullOrEmpty(selectedDestSystem))
            {
                q = q.Where(u => u.DestSystemId == Int32.Parse(selectedDestSystem));
            }
            switch (sortOrder)
            {
                case "RequestCreatedAtAsc":
                    q = q.OrderBy(t => t.RequestCreatedAt);
                    break;
                case "RequestCreatedAtDesc":
                    q = q.OrderByDescending(t => t.RequestCreatedAt);
                    break;
                case "SentTimeAsc":
                    q = q.OrderBy(t => t.SentTime);
                    break;
                case "SentTimeDesc":
                    q = q.OrderByDescending(t => t.SentTime);
                    break;
                case "ClientNameAsc":
                    q = q.OrderBy(t => t.ClientName);
                    break;
                case "ClientNameDesc":
                    q = q.OrderByDescending(t => t.ClientName);
                    break;
                case "ReviewedUserAsc":
                    q = q.OrderBy(t => t.ReviewedUserId);
                    break;
                case "ReviewedUserDesc":
                    q = q.OrderByDescending(t => t.ReviewedUserId);
                    break;
                case "CompletedUserAsc":
                    q = q.OrderBy(t => t.CompletedUserId);
                    break;
                case "CompletedUserDesc":
                    q = q.OrderByDescending(t => t.CompletedUserId);
                    break;
                case "IssueReportedAsc":
                    q = q.OrderBy(t => t.IssueReported);
                    break;
                case "IssueReportedDesc":
                    q = q.OrderByDescending(t => t.IssueReported);
                    break;
                case "SpillPreventedAsc":
                    q = q.OrderBy(t => t.SpillPrevented);
                    break;
                case "SpillPreventedDesc":
                    q = q.OrderByDescending(t => t.SpillPrevented);
                    break;
                case "SpillOccurredAsc":
                    q = q.OrderBy(t => t.SpillOccurred);
                    break;
                case "SpillOccurredDesc":
                    q = q.OrderByDescending(t => t.SpillOccurred);
                    break;
                case "CompletedAtAsc":
                    q = q.OrderBy(t => t.CompletedAt);
                    break;
                case "CompletedAtDesc":
                    q = q.OrderByDescending(t => t.CompletedAt);
                    break;
                case "UrgentAsc":
                    q = q.OrderBy(t => t.Urgent);
                    break;
                case "UrgentDesc":
                    q = q.OrderByDescending(t => t.Urgent);
                    break;
                case "ClientUnitAsc":
                    q = q.OrderBy(t => t.ClientUnitId);
                    break;
                case "ClientUnitDesc":
                    q = q.OrderByDescending(t => t.ClientUnitId);
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
                case "OperationAsc":
                    q = q.OrderBy(t => t.OperationId);
                    break;
                case "OperationDesc":
                    q = q.OrderByDescending(t => t.OperationId);
                    break;
                default:
                    q = q.OrderByDescending(t => t.RequestCreatedAt);
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
                    selectedUnit = ViewBag.SelectedUnit,
                    raisedAfter = ViewBag.RaisedAfter,
                    raisedBefore = ViewBag.RaisedBefore,
                    selectedCompletedUser = ViewBag.SelectedCompletedUser,
                    filterSpillOccurred = @ViewBag.FilterSpillOccurred,
                    filterTransferDenied = @ViewBag.FilterTransferDenied,
                    filterClientName = @ViewBag.FilterClientName,
                    selectedOrigSystem = @ViewBag.SelectedOrigSystem,
                    selectedDestSystem = @ViewBag.SelectedDestSystem
                }));
            }
            


            return View(await q.ToPagedListAsync(pageNumber, pageSize));
        }

        //// GET: Transfers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            Models.TransferModel? transfer;

            if (id == null)
            {
                return NotFound();
            }
            else
            {
                transfer = await _transferRepository.GetTransferByIdAsync(id!.Value);
            }

            if (transfer == null)
            {
                return NotFound();
            }

            ViewData["FileList"] = SetFileListViewData(transfer);

            return View(transfer);
        }

        //// GET: Transfers/Create
        public async Task<IActionResult> Create()
        {
            var model = new Models.TransferModel();
            model.Files = new List<FileModel>();

            Models.UserModel? user = GetCurrentUser().Result;

            ViewData["CurrentUser"] = user;

            model.CompletedUserId = user?.UserId;

            ViewData["ClientUnitId"] = new SelectList(await _unitRepository.GetAllUnitsAsync(true,true), "UnitId", "UnitName");
            ViewData["OperationId"] = new SelectList(await _operationRepository.GetAllOperationsAsync(true, true), "OperationId", "OperationName");
            ViewData["DestSystemId"] = new SelectList(await _systemRepository.GetAllSystemsAsync(true), "SystemId", "SystemName");
            ViewData["OrigSystemId"] = new SelectList(await _systemRepository.GetAllSystemsAsync(true), "SystemId", "SystemName");
            ViewData["ReviewedUserId"] = new SelectList(await _userRepository.GetAllUsersAsync(true), "UserId", "Username");
            ViewData["CompletedUserId"] = new SelectList(await _userRepository.GetAllUsersAsync(true), "UserId", "Username", user?.UserId);
            ViewData["TransferTypeId"] = new SelectList(await _transferTypeRepository.GetAllTransferTypeAsync(true), "TransferTypeId","TransferTypeDesc");
            ViewData["FileExtensionId"] = new SelectList(await _fileExtensionRepository.GetAllFileExtensionsAsync(true, false), "FileExtensionId", "FileExtensionName");

            return View();
        }


        //// POST: Transfers/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransferId,RequestCreatedAt,SentTime,ClientName,ClientUnitId,OperationId,OrigSystemId,DestSystemId,IssoApproval,IssueReported" +
            ",SpillPrevented,SpillOccurred,Comments,CompletedUserId,ReviewedUserId,ReviewedAt,CompletedAt,Urgent,Reviewed,Completed,TransferTypeId")] Models.TransferModel transfer, string fileList)
        {

            ICollection<Models.FileModel> fileListCollection;
            List<FileModel> filesToRemove = new();
            //if (transfer.Completed == true)
            //{
                transfer.CompletedAt = DateTime.Now;
            //}
            if(transfer.OperationId == 0)
            {
                transfer.OperationId = null;
            }

            if (fileList != null)
            {
                fileListCollection = JsonConvert.DeserializeObject<ICollection<FileModel>>(fileList)!;

                foreach (FileModel fm in fileListCollection)
                {
                    if (fm == null) continue;
                    fm.FileName = "";
                    transfer.Files.Add(fm);
                }
            }
            TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            if (transfer.SentTime.HasValue)
            {
                transfer.SentTime = TimeZoneInfo.ConvertTimeToUtc(transfer.SentTime.Value, est);
            }
            if (transfer.RequestCreatedAt != null)
            {
                transfer.RequestCreatedAt = TimeZoneInfo.ConvertTimeToUtc(transfer.RequestCreatedAt, est);
            }

            ModelState.Remove("DestSystem");
            ModelState.Remove("OrigSystem");

            //transfer.RequestCreatedAt = DateTime.Now;

            if (transfer.Files.Count < 1)
            {
                //ModelState.AddModelError("Files", "You must select at least one file or folder to create a transfer request.");

                if (ModelState.ContainsKey("fileList"))
                {
                    string key = "fileList";

                    if (ModelState[key] != null)
                    {
                        ModelState[key]!.ValidationState = ModelValidationState.Valid;
                        ModelState[key]!.Errors.Clear();
                    }
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _transferRepository.AddTransfer(transfer);

                }
                catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException)
                {
                    if (!TransferExists(transfer.TransferId).Result)
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

            ViewData["FileList"] = SetFileListViewData(transfer);

            await LoadCommonViewData();

            return View(transfer);
        }

        private static string SetFileListViewData(TransferModel transfer)
        {
            
            return JsonConvert.SerializeObject(transfer.Files.Select(f => new { fileID = f.FileId, fileName = "", 
                                                                                fileExt = f.FileExt, // <---! This is needed or else if a file does not have an extention it throws an exception. 
                                                                                fileSize = f.FileSize, fileSent = f.FileSent, fileComment = f.FileComment }).ToList());
            //Don't use this - it fails - see above. 
            //return JsonConvert.SerializeObject(transfer.Files.Select(f => new { fileID = f.FileId, fileName = f.FileName, fileExt = f.FileName.Substring(f.FileName.LastIndexOf(".")), fileSize = f.FileSize, fileSent = f.FileSent, fileComment = f.FileComment }).ToList());

        }


        // GET: Transfers/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var transfer = await _transferRepository.GetTransferByIdAsync(id);
            TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            if (transfer.SentTime.HasValue)
            {
                transfer.SentTime = TimeZoneInfo.ConvertTimeFromUtc(transfer.SentTime.Value, est);
            }
            if (transfer.RequestCreatedAt != null)
            {
                transfer.RequestCreatedAt = TimeZoneInfo.ConvertTimeFromUtc(transfer.RequestCreatedAt, est);
            }
            if (transfer == null)
            {
                return NotFound();
            }

            ViewData["FileList"] = SetFileListViewData(transfer);
            ViewData["CurrentUser"] = GetCurrentUser();
            ViewData["ClientUnitId"] = new SelectList(await _unitRepository.GetAllUnitsAsync(true), "UnitId", "UnitName");
            ViewData["CompletedUserId"] = new SelectList(await _userRepository.GetAllUsersAsync(), "UserId", "Username");
            ViewData["DestSystemId"] = new SelectList(await _systemRepository.GetAllSystemsAsync(true), "SystemId", "SystemName");
            ViewData["OperationId"] = new SelectList(await _operationRepository.GetAllOperationsAsync(true, true), "OperationId", "OperationName");
            ViewData["OrigSystemId"] = new SelectList(await _systemRepository.GetAllSystemsAsync(true), "SystemId", "SystemName");
            ViewData["ReviewedUserId"] = new SelectList(await _userRepository.GetAllUsersAsync(), "UserId", "Username");
            ViewData["TransferTypeId"] = new SelectList(await _transferTypeRepository.GetAllTransferTypeAsync(false), "TransferTypeId", "TransferTypeDesc");

            return View(transfer);
        }

        //// POST: Transfers/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransferId,RequestCreatedAt,SentTime,ClientName,ClientUnitId,OperationId,OrigSystemId,DestSystemId,IssoApproval,IssueReported" +
            ",SpillPrevented,SpillOccurred,Comments,CompletedUserId,ReviewedUserId,ReviewedAt,CompletedAt,Urgent,Reviewed,Completed,TransferTypeId")] Models.TransferModel transfer, string fileList)
        {
            ICollection<Models.FileModel> fileListCollection;
            List<FileModel> filesToRemove = new();

            if (id != transfer.TransferId)
            {
                return NotFound();
            }
            if (transfer.OperationId == 0)
            {
                transfer.OperationId = null;
            }

            fileListCollection = JsonConvert.DeserializeObject<ICollection<FileModel>>(fileList)!;
            filesToRemove = await _fileRepository.GetFilesByTransferIdAsync(id);
            
            foreach (FileModel file in filesToRemove)
            {
                await _fileRepository.RemoveFileAsync(file);
            }

            foreach (FileModel fm in fileListCollection)
            {
                if (fm == null) continue;
                transfer.Files.Add(fm);
            }

            ModelState.Remove("DestSystem");
            ModelState.Remove("OrigSystem");


            TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            if (transfer.SentTime.HasValue)
            {
                transfer.SentTime = TimeZoneInfo.ConvertTimeToUtc(transfer.SentTime.Value, est);
            }
            if (transfer.RequestCreatedAt != null)
            {
                transfer.RequestCreatedAt = TimeZoneInfo.ConvertTimeToUtc(transfer.RequestCreatedAt, est);
            }


            if (ModelState.IsValid)
            {
                try
                {
                   _transferRepository.UpdateTransfer(transfer);

                }
                catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException)
                {
                    if (!TransferExists(transfer.TransferId).Result)
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

            ViewData["FileList"] = SetFileListViewData(transfer);
            await LoadCommonViewData();

            return View(transfer);
        }

        private async Task LoadCommonViewData()
        {
            ViewData["CurrentUser"] = GetCurrentUser();

            ViewData["ClientUnitId"] = new SelectList(await _unitRepository.GetAllUnitsAsync(true, true), "UnitId", "UnitName"); ;
            ViewData["OperationId"] = new SelectList(await _operationRepository.GetAllOperationsAsync(true, true), "OperationId", "OperationName");
            ViewData["DestSystemId"] = new SelectList(await _systemRepository.GetAllSystemsAsync(true), "SystemId", "SystemName");
            ViewData["OrigSystemId"] = new SelectList(await _systemRepository.GetAllSystemsAsync(true), "SystemId", "SystemName");
            ViewData["ReviewedUserId"] = new SelectList(await _userRepository.GetAllUsersAsync(), "UserId", "Username");
            ViewData["CompletedUserId"] = new SelectList(await _userRepository.GetAllUsersAsync(), "UserId", "Username");
            ViewData["TransferTypeId"] = new SelectList(await _transferTypeRepository.GetAllTransferTypeAsync(false), "TransferTypeId", "TransferTypeDesc");
        }

        // GET: Transfers/Delete/5
        [CustomAuthorize(Roles = Constants.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            var transfer = await _transferRepository.GetTransferByIdAsync(id);

            if (transfer == null)
            {
                return NotFound();
            }

            ViewData["FileList"] = SetFileListViewData(transfer);

            return View(transfer);
        }

        // POST: Transfers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = Constants.Admin)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transfer = await _transferRepository.GetTransferByIdAsync(id);

            if (transfer != null)
            { 
                await _fileRepository.RemoveFilesByTransferIdAsync(id);
                await _transferRepository.RemoveTransferAsync(transfer);

            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<bool> TransferExists(int id)
        {
            return await _transferRepository.ExistsAsync(id);
        }
    }

}