namespace New_DTAPP.Data;

public class SpillStatus
{
    public int SpillStatusId { get; set; }
    public string SpillStatusDesc { get; set; }
    public bool Archived { get; set; }

    public ICollection<Spill> SpillStatuses { get; set; } = new List<Spill>();
}