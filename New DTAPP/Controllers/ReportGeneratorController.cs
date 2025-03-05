using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ClosedXML.Excel;
using New_DTAPP.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using New_DTAPP.Security;
using New_DTAPP.Repository.Interfaces;
using New_DTAPP.Helpers;
using static ClosedXML.Excel.XLPredefinedFormat;
using Tuple = System.Tuple;

namespace New_DTAPP.Controllers
{
    [CustomAuthorize(Roles = Constants.Admin + "," + Constants.Supervisor)]
    public class ReportGeneratorController : Controller
    {
        private readonly ITransferRepository _transferRepository;

        public ReportGeneratorController(ITransferRepository transferRepository)
        {
            _transferRepository = transferRepository;
        }

        public async Task<IActionResult> CreateExportReportstring(string? sortOrder, string? selectedUnit, string? raisedAfter, string? raisedBefore,
                                                string? selectedCompletedUser, bool? filterSpillOccurred, bool? filterTransferDenied, string? filterClientName)
        {
            FileSizeHelper fileSizeHelper = new FileSizeHelper();
            int row = 1;
            int column = 19;
            XLWorkbook workbook = new();
            IXLWorksheet? filteredList = workbook.Worksheets.Add("Transfers");

            var transferModel = await _transferRepository.GetAllTransfersAsync();
            var q = transferModel.AsQueryable();
            if (!string.IsNullOrEmpty(selectedUnit))
            {
                q = q.Where(u => u.ClientUnitId == Int32.Parse(selectedUnit));
            }
            if (!string.IsNullOrEmpty(raisedAfter))
            {
                q = q.Where(u => u.RequestCreatedAt >= System.DateTime.Parse(raisedAfter));
            }
            if (!string.IsNullOrEmpty(raisedBefore))
            {
                q = q.Where(u => u.RequestCreatedAt <= System.DateTime.Parse(raisedBefore));
            }
            if (!string.IsNullOrEmpty(selectedCompletedUser))
            {
                q = q.Where(u => u.CompletedUserId == Int32.Parse(selectedCompletedUser));
            }
            if(!string.IsNullOrEmpty(filterClientName))
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

            List<Tuple<string, int>> fileExt = fileSizeHelper.GetFileExtensions(q, column);

            filteredList.Cell(row, 1).Value = "Time of Request";
            filteredList.Cell(row, 2).Value = "Sent Time";
            filteredList.Cell(row, 3).Value = "Client Name";
            filteredList.Cell(row, 4).Value = "Client Unit";
            filteredList.Cell(row, 5).Value = "Operation Name";
            filteredList.Cell(row, 6).Value = "# of Files";
            filteredList.Cell(row, 7).Value = "File Size Total";
            filteredList.Cell(row, 8).Value = "Originating System";
            filteredList.Cell(row, 9).Value = "Destination System";
            filteredList.Cell(row, 10).Value = "Transfer Type";
            filteredList.Cell(row, 11).Value = "ISSO Approval";
            filteredList.Cell(row, 12).Value = "Spill Prevented";
            filteredList.Cell(row, 13).Value = "Spill Occurred";
            filteredList.Cell(row, 14).Value = "Transfer Denied";
            filteredList.Cell(row, 15).Value = "Comments";
            filteredList.Cell(row, 16).Value = "Completed User";
            filteredList.Cell(row, 17).Value = "Reviewed User";
            filteredList.Cell(row, 18).Value = "Entry Created At";
            foreach (Tuple<string,int> ext in fileExt)
            {
                filteredList.Cell(row, ext.Item2).Value = ext.Item1;
            }
            
            foreach (TransferModel eachTransfer in q)
            {
                row++;
                filteredList.Cell(row, 1).Value = eachTransfer.RequestCreatedAt;
                filteredList.Cell(row, 2).Value = eachTransfer.SentTime;
                filteredList.Cell(row, 3).Value = eachTransfer.ClientName;
                filteredList.Cell(row, 4).Value = eachTransfer.ClientUnit.UnitName;
                if (eachTransfer.Operation != null)
                {
                    filteredList.Cell(row, 5).Value = eachTransfer.Operation.OperationName;
                }
                filteredList.Cell(row, 6).Value = fileSizeHelper.TotalFileCount(eachTransfer.Files);
                filteredList.Cell(row, 7).Value = fileSizeHelper.TotalFileSize(eachTransfer.Files);
                filteredList.Cell(row, 8).Value = eachTransfer.OrigSystem.SystemName;
                filteredList.Cell(row, 9).Value = eachTransfer.DestSystem.SystemName;
                filteredList.Cell(row, 10).Value = eachTransfer.TransferTypeModel.TransferTypeDesc;
                filteredList.Cell(row, 11).Value = eachTransfer.IssoApproval;
                filteredList.Cell(row, 12).Value = eachTransfer.SpillPrevented;
                filteredList.Cell(row, 13).Value = eachTransfer.SpillOccurred;
                filteredList.Cell(row, 14).Value = eachTransfer.Urgent;
                filteredList.Cell(row, 15).Value = eachTransfer.Comments;
                filteredList.Cell(row, 16).Value = eachTransfer.CompletedUser.Username;
                if (eachTransfer.ReviewedUser != null)
                {
                    filteredList.Cell(row, 17).Value = eachTransfer.ReviewedUser.Username;
                }
                filteredList.Cell(row, 18).Value = eachTransfer.CompletedAt;
                foreach (Tuple<string, int> ext in fileExt)
                {
                    filteredList.Cell(row, ext.Item2).Value = fileSizeHelper.FileCountByType(eachTransfer.Files,ext.Item1);
                }

            }
            
            var range = filteredList.Range(1, 1, row, 18+fileExt.Count());
            var table = range.CreateTable();
            table.Theme = XLTableTheme.TableStyleLight9;

            using MemoryStream? stream = new();
            workbook.SaveAs(stream);

            return File(stream.ToArray(), Constants.applicationTypeSpreadsheet, "Transfers.xlsx");
        }

       
    }
}
