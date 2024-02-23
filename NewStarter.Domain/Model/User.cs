namespace NewStarter.Domain.Model;

public partial class User
{
    public int UserId { get; set; }

    public string LastName { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? UserName { get; set; }

    public string? PinCode { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<AccessAudit> AccessAudits { get; set; } = new List<AccessAudit>();

    public virtual ICollection<AccessControlDoorsToUser> AccessControlDoorsToUsers { get; set; } = new List<AccessControlDoorsToUser>();
}
