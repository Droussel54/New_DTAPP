using System;
using System.Collections.Generic;

namespace New_DTAPP.Data;

public partial class FileExtension
{
    public int FileExtensionId { get; set; }

    public string FileExtensionName { get; set; } = null!;

    public bool Archived { get; set; }
}
