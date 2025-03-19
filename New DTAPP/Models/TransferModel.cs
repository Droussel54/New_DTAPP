using New_DTAPP.Utility;
using System.Collections.Generic;

using System;

//using System.ComponentModel.DataAnnotations;
using static New_DTAPP.Utility.ValidateRequiredDropDownsAndTextFieldsAttribute;
using System.ComponentModel.DataAnnotations;

namespace New_DTAPP.Models;

public class TransferModel //: IValidatableObject
{

    public TransferModel()
    { }

    public int TransferId { get; set; }

    [ValidateTransfersDates(
        DateValidationType.RequestCreatedAt,
        "RequestCreatedAt", "SentTime",
        "ReviewedAt", "CompletedAt")]
    [DisplayFormat(DataFormatString = "{0:yyy/MM/dd HH:mm}")]
    public DateTime RequestCreatedAt { get; set; } = DateTime.Now;

    //[ValidateTransfersDates(
    //    DateValidationType.SentTime,
    //    "RequestCreatedAt", "SentTime",
    //    "ReviewedAt", "CompletedAt")] 
    [DisplayFormat(DataFormatString = "{0:yyy/MM/dd HH:mm}")]
    public DateTime? SentTime { get; set; }

    [ValidateRequiredDropDownsAndTextFields(FieldValidationType.RequiredFieldString, ErrorMessage = "Client Name is a required.")]
    public string? ClientName { get; set; }

    [ValidateRequiredDropDownsAndTextFields(FieldValidationType.RequiredFieldInt, ErrorMessage = "Client Unit is required.")]
    public int? ClientUnitId { get; set; }

    public int? OperationId { get; set; }

    //[ValidateRequiredDropDownsAndTextFields(FieldValidationType.OrigAndDestSystemCannotBeTheSame, "DestSystemId")]
    public int OrigSystemId { get; set; }

    //[ValidateRequiredDropDownsAndTextFields(FieldValidationType.OrigAndDestSystemCannotBeTheSame, "OrigSystemId")]
    public int DestSystemId { get; set; }

    public bool IssoApproval { get; set; }

    public bool IssueReported { get; set; }

    public bool SpillPrevented { get; set; }

    public bool SpillOccurred { get; set; }

    public string? Comments { get; set; }

    public bool Urgent { get; set; }

    //[ValidateTransfersDates(
    //   DateValidationType.ReviewedAtDate,
    //   "RequestCreatedAt", "SentTime",
    //   "ReviewedAt", "CompletedAt")]
    [DisplayFormat(DataFormatString = "{0:yyy/MM/dd HH:mm}")]
    public DateTime? ReviewedAt { get; set; }

    //[ValidateTransfersDates(
    // DateValidationType.CompletedAtDate,
    // "RequestCreatedAt", "SentTime",
    // "ReviewedAt", "CompletedAt")]
    [DisplayFormat(DataFormatString = "{0:yyy/MM/dd HH:mm}")]
    public DateTime? CompletedAt { get; set; }

    public bool Completed { get; set; }

    public UnitModel? ClientUnit { get; set; }

    //[ValidateRequiredDropDownsAndTextFields(FieldValidationType.RequiredField, ErrorMessage = "Completed User is required.")] 
    //[ValidateRequiredDropDownsAndTextFields(FieldValidationType.ReviewedAndCompletedUsersCannotBeTheSame, "ReviewedUserId")]
    public int? CompletedUserId { get; set; }

   
    public UserModel? CompletedUser { get; set; }

    //[System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Destination System is required.")]
    public SystemModel DestSystem { get; set; } = null!;

    //[FileCountValidation]
    public ICollection<FileModel> Files { get; set; } = new List<FileModel>();

    public OperationModel? Operation { get; set; }

    public SystemModel OrigSystem { get; set; } = null!;


    public bool Reviewed { get; set; }

    //[ValidateRequiredDropDownsAndTextFields(FieldValidationType.ReviewedAndCompletedUsersCannotBeTheSame, "CompletedUserId")]
    public int? ReviewedUserId { get; set; }

    //[ValidateRequiredDropDownsAndTextFields(FieldValidationType.ReviewedAndCompletedUsersCannotBeTheSame, "CompletedUser", ErrorMessage = "Completed User is required.")]
    public UserModel? ReviewedUser { get; set; }

    public int TransferTypeId { get; set; }

    public TransferTypeModel? TransferTypeModel { get; set; }

    public FileExtensionModel? FileExtensionModel { get; set; }
}
