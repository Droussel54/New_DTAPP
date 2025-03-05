using System;
using System.Collections.Generic;

namespace New_DTAPP.Data
{
    public partial class TransferType
    {
        public int TransferTypeId { get; set; }

        public string TransferTypeDesc { get; set; } = null!;

        public bool Archived { get; set; }

        public int Ordering {  get; set; }

        public virtual ICollection<Transfer> TransferTypeList { get; set; } = new List<Transfer>();
    }
}
