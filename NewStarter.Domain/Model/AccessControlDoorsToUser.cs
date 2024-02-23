namespace NewStarter.Domain.Model;

public partial class AccessControlDoorsToUser
{
    public int AccessControlDoorsToUsersId { get; set; }

    public int DoorId { get; set; }

    public int UserId { get; set; }

    public virtual Door Door { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
