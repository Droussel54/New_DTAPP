using System;
using System.Collections.Generic;

namespace New_DTAPP.Data;

public partial class Unit
{
    public int UnitId { get; set; }

    public string UnitName { get; set; } = null!;

    public bool Archived { get; set; }

    public virtual ICollection<Transfer> Transfers { get; set; } = new List<Transfer>();
}
