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

    public bool BurnedAndAnnotated { get; set; }

    [DisplayFormat(DataFormatString = "{0:yyy/MM/dd HH:mm}")]
    public DateTime? IssoInformed { get; set; }

    [DisplayFormat(DataFormatString = "{0:yyy/MM/dd HH:mm}")]
    public DateTime? ManagerInformed { get; set; }

    [ValidateRequiredDropDownsAndTextFields(FieldValidationType.RequiredFieldString, ErrorMessage = "Nature of the spill is required.")]
    public string? NatureOfSpill { get; set; }

    public bool TransferRequestCompleted { get; set; }

    public bool EmailTripleDeleted { get; set; }

    public bool ClientInformed { get; set; }

    public bool ConsiderationPowerDown { get; set; }

    public bool CDSent { get; set; }

    [DisplayFormat(DataFormatString = "{0:yyy/MM/dd HH:mm}")]
    public DateTime? DateOfSpill { get; set; }

    [DisplayFormat(DataFormatString = "{0:yyy/MM/dd HH:mm}")]
    public DateTime? TimeOfSpill { get; set; }

    [DisplayFormat(DataFormatString = "{0:yyy/MM/dd HH:mm}")]
    public DateTime? TimeIdentifiedSpill { get; set; }

    [DisplayFormat(DataFormatString = "{0:yyy/MM/dd HH:mm}")]
    public DateTime? TimeReported { get; set; }

    public string? WorkstationAffected { get; set; }

    public string? WorkstationAssetNumber { get; set; }

    public int SpecialistId { get; set; }

    public UserModel? SpecialistUser { get; set; }

    public int? ReviewerId { get; set; }

    public UserModel? ReviewerUser { get; set; }

    public string? SystemsInvolved { get; set; }

    public int TransferId { get; set; }

    public TransferModel? Transfer { get; set; }
}
