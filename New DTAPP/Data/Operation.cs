using System;
using System.Collections.Generic;

namespace New_DTAPP.Data;

public partial class Operation
{
    public int OperationId { get; set; }

    public string OperationName { get; set; } = null!;

    public bool Archived { get; set; }

    public virtual ICollection<Transfer> Transfers { get; set; } = new List<Transfer>();
}
