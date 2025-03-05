using System;
using System.Collections.Generic;

namespace New_DTAPP.Data;

public partial class File
{
    public int FileId { get; set; }

    public string FileName { get; set; } = null!;

    public string FileSize { get; set; } = null!;

    public int? TransferId { get; set; }

    public string? Comment { get; set; }

    public bool FileSent { get; set; }
    public string FileExt { get; set; }

    public virtual Transfer? Transfer { get; set; }
}
