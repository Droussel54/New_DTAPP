using New_DTAPP.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace New_DTAPP.Models;

public partial class OperationModel
{
    public OperationModel()
    { }

    public int OperationId { get; set; }

    [Required]
    [StringLength(500)]
    [Display(Name = "Operation Name")]
    public string OperationName { get; set; } = null!;

    public bool Archived { get; set; }

}
