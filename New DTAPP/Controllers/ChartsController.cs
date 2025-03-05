using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using New_DTAPP.Helpers;
using New_DTAPP.Models;
using New_DTAPP.Repository.Interfaces;
using New_DTAPP.Security;
using System.Data.Common;
using System.Text.Json;

namespace New_DTAPP.Controllers
{
    [CustomAuthorize(Roles = Constants.Admin)]
    public class ChartsController : Controller
    {
        private readonly ITransferRepository _transferRepository;
        private readonly IUnitRepository _unitRepository;
        private readonly IUserRepository _userRepository;

        public ChartsController(ITransferRepository transferRepository, IUnitRepository unitRepository, IUserRepository userRepository)
        {
            _transferRepository = transferRepository;
            _unitRepository = unitRepository;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> DataSizeByDestination(string? selectedUnit, string? raisedAfter, string? raisedBefore,
                                               string? selectedCompletedUser, bool? filterSpillOccurred, bool? filterTransferDenied, string? filtered, string? clearFilters)
        {
            var q = GetFilteredList(selectedUnit, raisedAfter, raisedBefore, selectedCompletedUser, filterSpillOccurred, filterTransferDenied).Result;

            FileSizeHelper fileSizeHelper = new FileSizeHelper();
            var files = q.Select(x => new { dest = x.DestSystem.SystemName, totalFiles = x.Files });
            List<Tuple<string, decimal>> group = new List<Tuple<string, decimal>>();
            foreach (var f in files)
            {
                
                decimal bytes = 0;
                bytes = Math.Round(fileSizeHelper.TotalFileSizeInBytes(f.totalFiles));
                group.Add(Tuple.Create(f.dest, bytes));
            }
            var filter = group.AsQueryable().GroupBy(d => d.Item1).Select(x => new {destSys = x.Key, totalSize = x.Sum(s=>s.Item2)});

            var json = JsonSerializer.Serialize(filter);
            ViewBag.jsonData = json;
            return View();
        }

        public async Task<IActionResult> RequestsByNetwork(string? selectedUnit, string? raisedAfter, string? raisedBefore,
                                               string? selectedCompletedUser, bool? filterSpillOccurred, bool? filterTransferDenied, string? filtered, string? clearFilters)
        {

            var q = GetFilteredList(selectedUnit, raisedAfter, raisedBefore, selectedCompletedUser, filterSpillOccurred, filterTransferDenied).Result;

            var group = q.Select(x => new { orig = x.OrigSystem.SystemName, dest = x.DestSystem.SystemName })
                         .GroupBy(x => new { x.orig, x.dest })
                         .Select(g => new { origSys = g.Key.orig, destSys = g.Key.dest, count = g.Count() });

            var json = JsonSerializer.Serialize(group);
            ViewBag.jsonData = json;
            return View();
        }

        //Adding controller for chart showing data from-data to named "Routes"

        public async Task<IActionResult> Routes(string? selectedUnit, string? raisedAfter, string? raisedBefore,
                                              string? selectedCompletedUser, bool? filterSpillOccurred, bool? filterTransferDenied, string? filtered, string? clearFilters)
        {
            
            var q = GetFilteredList(selectedUnit, raisedAfter, raisedBefore, selectedCompletedUser, filterSpillOccurred, filterTransferDenied).Result;

            var group = q.Select(x => new { orig = x.OrigSystem.SystemName, dest = x.DestSystem.SystemName })
                        .GroupBy(x => x.orig)
                        .Select(g => new
                        {
                            name = g.Key,
                            children = g.GroupBy(x => x.dest)
                                .Select(dg => new
                                {
                                    name = dg.Key,
                                    value = dg.Count()
                                })
                                .ToList()
                        }).ToList();

            var json = JsonSerializer.Serialize(group);
            ViewBag.jsonData = json;
            return View();
        }


        public async Task<IActionResult> RequestsByHour(string? selectedUnit, string? raisedAfter, string? raisedBefore,
                                       string? selectedCompletedUser, bool? filterSpillOccurred, bool? filterTransferDenied, string? filtered, string? clearFilters)
        {

            var q = GetFilteredList(selectedUnit, raisedAfter, raisedBefore, selectedCompletedUser, filterSpillOccurred, filterTransferDenied).Result;

            var group = q.Select(x => new { time = x.RequestCreatedAt.Hour})
                         .GroupBy(x => new { x.time})
                         .Select(g => new { hour = g.Key.time, count = g.Count() })
                         .OrderBy(o => o.hour);
            var json = JsonSerializer.Serialize(group);

            ViewBag.jsonData = json;
            return View();
        }

        public async Task<IActionResult> TopClientsByRequests(string? typeOfReport,string? raisedAfter, string? raisedBefore,
                                       string? selectedCompletedUser, bool? filterSpillOccurred, bool? filterTransferDenied, string? filtered, string? clearFilters)
        {
            if (typeOfReport == null) {
                ViewBag.NumOfTransfers = true;
            }
            else
            {
                switch(typeOfReport)
                {
                    case "numOfTransfers":
                        ViewBag.NumOfTransfers = true;
                        break;
                    case "numOfFiles":
                        ViewBag.NumOfFiles = true;
                        break;
                    case "fileSize":
                        ViewBag.FileSize = true;
                        break;
                    default:
                        ViewBag.NumOfTransfers = true;
                        break;
                }
            }
           
            var q = GetFilteredList(null, raisedAfter, raisedBefore, selectedCompletedUser, filterSpillOccurred, filterTransferDenied).Result;

            if (typeOfReport == "numOfTransfers" || typeOfReport == null)
            {
                var group = q.GroupBy(x => new { x.ClientUnit.UnitName })
                    .Select(s => new { clientUnit = s.Key.UnitName, count = s.Count() })
                    .OrderByDescending(o => o.count);
                var json = JsonSerializer.Serialize(group);
                ViewBag.jsonData = json;
            }
            else if (typeOfReport == "fileSize")
            {
                FileSizeHelper fileSizeHelper = new FileSizeHelper();
                var files = q.Select(x => new { unit = x.ClientUnit.UnitName, totalFiles = x.Files });
                List<Tuple<string, decimal>> tup = new List<Tuple<string, decimal>>();
                foreach (var f in files)
                {
                    decimal bytes = 0;
                    bytes = Math.Round(fileSizeHelper.TotalFileSizeInBytes(f.totalFiles));
                    tup.Add(Tuple.Create(f.unit, bytes));
                }
                var group = tup.AsQueryable().GroupBy(d => d.Item1).Select(x => new { clientUnit = x.Key, count = x.Sum(s => s.Item2) }).OrderByDescending(o => o.count);
                var json = JsonSerializer.Serialize(group);
                ViewBag.jsonData = json;
            }
            else if (typeOfReport == "numOfFiles")
            {
                var group = q.GroupBy(x => new { x.ClientUnit.UnitName }).Select(s => new { clientUnit = s.Key.UnitName, count = s.Sum(a=>a.Files.Count) }).OrderByDescending(o => o.count);
                var json = JsonSerializer.Serialize(group);
                ViewBag.jsonData = json;
            }
            
            
            return View();
        }

        private async Task<IQueryable<TransferModel>> GetFilteredList(string? selectedUnit, string? raisedAfter, string? raisedBefore,
                                               string? selectedCompletedUser, bool? filterSpillOccurred, bool? filterTransferDenied)
        {
            ViewData["ClientUnitId"] = new SelectList(await _unitRepository.GetAllUnitsAsync(true, false), "UnitId", "UnitName");
            ViewBag.SelectedUnit = selectedUnit;
            ViewBag.RaisedAfter = raisedAfter;
            ViewBag.RaisedBefore = raisedBefore;
            ViewBag.SelectedCompletedUser = selectedCompletedUser;
            ViewBag.FilterSpillOccurred = filterSpillOccurred;
            ViewBag.FilterTransferDenied = filterTransferDenied;
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
            return q;
        }
    }
}
