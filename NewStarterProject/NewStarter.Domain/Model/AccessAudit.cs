using System;
using System.Collections.Generic;

namespace NewStarterProject.NewStarter.Domain.Model;

public partial class AccessAudit
{
    public int AccessAuditId { get; set; }

    public int DoorId { get; set; }

    public int? UserId { get; set; }

    public bool AccessGranted { get; set; }

    public DateTime AuditDateTime { get; set; }

    public virtual Door Door { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
