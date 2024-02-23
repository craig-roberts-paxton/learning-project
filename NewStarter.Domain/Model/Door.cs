namespace NewStarter.Domain.Model;

public partial class Door
{
    public int DoorId { get; set; }

    public string DoorName { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<AccessAudit> AccessAudits { get; set; } = new List<AccessAudit>();

    public virtual ICollection<AccessControlDoorsToUser> AccessControlDoorsToUsers { get; set; } = new List<AccessControlDoorsToUser>();
}
