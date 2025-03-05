using System;
using System.Collections.Generic;

namespace New_DTAPP.Data;

public partial class Transfer
{
    public int TransferId { get; set; }

    public DateTime RequestCreatedAt { get; set; }

    public DateTime? SentTime { get; set; }

    public string? ClientName { get; set; }

    public int? ClientUnitId { get; set; }

    public int? OperationId { get; set; }

    public int OrigSystemId { get; set; }

    public int DestSystemId { get; set; }

    public bool IssoApproval { get; set; }

    public bool IssueReported { get; set; }

    public bool SpillPrevented { get; set; }

    public bool SpillOccurred { get; set; }

    public string? Comments { get; set; }

    public bool Urgent { get; set; }

    public DateTime? VirusDefinitionDate { get; set; }

    public string? MachineName { get; set; }

    public bool Reviewed { get; set; }

    public int? ReviewedUserId { get; set; }

    public DateTime? ReviewedAt { get; set; }

    public int? CompletedUserId { get; set; }

    public DateTime? CompletedAt { get; set; }

    public bool Completed { get; set; }
    public int TransferTypeId {  get; set; }

    public virtual Unit? ClientUnit { get; set; }

    public virtual User? CompletedUser { get; set; }

    public virtual System DestSystem { get; set; } = null!;

    public virtual ICollection<File> Files { get; set; } = new List<File>();

    public virtual Operation? Operation { get; set; }

    public virtual System OrigSystem { get; set; } = null!;

    public virtual User? ReviewedUser { get; set; }

    public virtual TransferType TransferType { get; set; }
}
