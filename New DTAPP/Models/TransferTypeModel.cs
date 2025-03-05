using New_DTAPP.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace New_DTAPP.Models
{
    public partial class TransferTypeModel
    {
        public TransferTypeModel()
        { }

        public int TransferTypeId { get; set; }

        [Required]
        [StringLength(64)]
        [Display(Name = "Transfer Type")]
        public string TransferTypeDesc { get; set; } = null!;

        public bool Archived { get; set; }

        public int Ordering { get; set; }

        public virtual ICollection<TransferModel> TransferTypeList{ get; set; } = new List<TransferModel>();
    }
}
