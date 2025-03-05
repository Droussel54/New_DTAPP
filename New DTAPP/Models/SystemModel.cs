using New_DTAPP.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace New_DTAPP.Models;

public class SystemModel
{
    public SystemModel()
    { }

    public int SystemId { get; set; }

    [Required]
    [StringLength(500)]
    [Display(Name = "System Name")]
    public string SystemName { get; set; } = null!;

    public bool Archived { get; set; }

    public virtual ICollection<TransferModel> TransferDestSystems { get; set; } = new List<TransferModel>();

    public virtual ICollection<TransferModel> TransferOrigSystems { get; set; } = new List<TransferModel>();
}
