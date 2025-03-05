using System;
using System.Collections.Generic;

namespace New_DTAPP.Models;

public class FileModel
{
    public FileModel()
    { }
     public int FileId { get; set; }   
    
    public string FileName { get; set; } = null!;

    public string FileSize { get; set; } = null!;

    public int? TransferId { get; set; }

    public bool FileSent { get; set; } = false;
    
    public string? FileComment { get; set; } = string.Empty!;

    public string FileExt { get; set; }

    public virtual TransferModel? Transfer { get; set; }
}
