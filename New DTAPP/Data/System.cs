using System;
using System.Collections.Generic;

namespace New_DTAPP.Data;

public partial class System
{
    public int SystemId { get; set; }

    public string SystemName { get; set; } = null!;

    public bool Archived { get; set; }

    public virtual ICollection<Transfer> TransferDestSystems { get; set; } = new List<Transfer>();

    public virtual ICollection<Transfer> TransferOrigSystems { get; set; } = new List<Transfer>();
}
