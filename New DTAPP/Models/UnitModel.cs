using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using New_DTAPP.Utility;

namespace New_DTAPP.Models;

public class UnitModel
{
    public UnitModel()
    { }

    public int UnitId { get; set; }
    
    [Required]
    [StringLength(500)]
    [Display(Name = "Unit Name")]
    public string UnitName { get; set; } = null!;
    
    public bool Archived { get; set; }
    public virtual ICollection<TransferModel> Transfers { get; set; } = new List<TransferModel>();
}
