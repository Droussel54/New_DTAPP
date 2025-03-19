using System.ComponentModel.DataAnnotations;

namespace New_DTAPP.Models;

public class FileExtensionModel
{
    public FileExtensionModel()
    { }

    public int FileExtensionId { get; set; }

    [Required]
    [StringLength(500)]
    [Display(Name = "File Extension Name")]
    public string FileExtensionName { get; set; } = null!;

    public bool Archived { get; set; }
}
