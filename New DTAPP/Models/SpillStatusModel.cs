using System.ComponentModel.DataAnnotations;

namespace New_DTAPP.Models;

public class SpillStatusModel
{
    public SpillStatusModel() { }

    public int SpillStatusId { get; set; }
    [Display(Name = "Status")]
    public string SpillStatusDesc { get; set; }
    public bool Archived { get; set; }

}