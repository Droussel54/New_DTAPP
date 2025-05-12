namespace New_DTAPP.Data;

public class Spill
{
    public int SpillId { get; set; }
    public int? SpillStatusId { get; set; }
    public string? CFNOCIncidentNumber { get; set; }
    public string? DGDSSIMIncidentNumber { get; set; }
    public bool BurnedAndAnnotated { get; set; }
    public DateTime? IssoInformed { get; set; }
    public DateTime? ManagerInformed { get; set; }
    public string? NatureOfSpill { get; set; }
    public bool TransferRequestCompleted { get; set; }
    public bool EmailTripleDeleted { get; set; }
    public bool ClientInformed { get; set; }
    public bool ConsiderationPowerDown { get; set; }
    public bool CDSent { get; set; }
    public DateTime? DateOfSpill { get; set; }
    public DateTime? TimeOfSpill { get; set; }
    public DateTime? TimeIdentifiedSpill { get; set; }
    public DateTime? TimeReported { get; set; }
    public string? WorkstationAffected { get; set; }
    public string? WorkstationAssetNumber { get; set; }
    public int SpecialistId { get; set; }
    public int? ReviewerId { get; set; }
    public int? OrigSystemId { get; set; }
    public int? DestSystemId { get; set; }
    public int TransferId { get; set; }

    public virtual SpillStatus? SpillStatus { get; set; }
    public virtual User? SpecialistUser { get; set; }
    public virtual User? ReviewerUser { get; set; }
    public virtual Transfer? Transfer { get; set; }
    public virtual System? OrigSystem { get; set; } = null!;
    public virtual System? DestSystem { get; set; } = null!;

    public virtual ICollection<Transfer> TransferSpills { get; set; } = new List<Transfer>();
}
