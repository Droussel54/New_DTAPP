using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using New_DTAPP.Data;
using New_DTAPP.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static New_DTAPP.Utility.ValidateRequiredDropDownsAndTextFieldsAttribute;

namespace New_DTAPP.Models;

public class SpillModel
{
    public SpillModel()
    { }

    public int SpillId { get; set; }

    [ValidateRequiredDropDownsAndTextFields(FieldValidationType.RequiredFieldString, ErrorMessage = "Spill Status is required.")]
    public int? SpillStatusId { get; set; }
    public SpillStatusModel? SpillStatus { get; set; }

    public string? CFNOCIncidentNumber { get; set; }

    public string? DGDSSIMIncidentNumber { get; set; }

    public bool BurnedAndAnnotated { get; set; }

    [DisplayFormat(DataFormatString = "{0:yyy/MM/dd HH:mm}")]
    public DateTime? IssoInformed { get; set; }

    [DisplayFormat(DataFormatString = "{0:yyy/MM/dd HH:mm}")]
    public DateTime? ManagerInformed { get; set; }

    [ValidateRequiredDropDownsAndTextFields(FieldValidationType.RequiredFieldString, ErrorMessage = "Nature of Spill is required.")]
    public string? NatureOfSpill { get; set; }

    public bool TransferRequestCompleted { get; set; }

    public bool EmailTripleDeleted { get; set; }

    public bool ClientInformed { get; set; }

    public bool ConsiderationPowerDown { get; set; }

    public bool CDSent { get; set; }

    [DisplayFormat(DataFormatString = "{0:yyy/MM/dd HH:mm}")]
    [Required(ErrorMessage = "Date of Spill is required.")]
    public DateTime? DateOfSpill { get; set; }

    [DisplayFormat(DataFormatString = "{0:yyy/MM/dd HH:mm}")]
    [Required(ErrorMessage = "Time Identified as Spill is required.")]
    public DateTime? TimeIdentifiedSpill { get; set; }

    public string? WorkstationAffected { get; set; }

    public string? WorkstationAssetNumber { get; set; }

    [ValidateRequiredDropDownsAndTextFields(FieldValidationType.RequiredFieldInt, ErrorMessage = "Specialist User Name is required.")]
    public int SpecialistId { get; set; }

    public UserModel? SpecialistUser { get; set; }

    [ValidateRequiredDropDownsAndTextFields(FieldValidationType.RequiredFieldInt, ErrorMessage = "Reviewer User Name is required.")]
    public int? ReviewerId { get; set; }

    public UserModel? ReviewerUser { get; set; }

    [ValidateRequiredDropDownsAndTextFields(FieldValidationType.RequiredFieldInt, ErrorMessage = "Originating System is required.")]
    public int? OrigSystemId { get; set; }

    public SystemModel? OrigSystem { get; set; } = null!;

    [ValidateRequiredDropDownsAndTextFields(FieldValidationType.RequiredFieldInt, ErrorMessage = "Destination System is required.")]
    public int? DestSystemId { get; set; }

    public SystemModel? DestSystem { get; set; } = null!;

    [Required(ErrorMessage = "TransferId is required.")]
    public int TransferId { get; set; }

    public TransferModel? Transfer { get; set; }
}
