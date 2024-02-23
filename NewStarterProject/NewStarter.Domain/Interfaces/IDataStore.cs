using Microsoft.EntityFrameworkCore;
using NewStarterProject.NewStarter.Domain.Model;

namespace NewStarterProject.NewStarter.Domain.Interfaces
{
    public interface IDataStore : IDisposable
    {
        DbSet<AccessAudit> AccessAudits { get; set; }
        DbSet<AccessControlDoorsToUser> AccessControlDoorsToUsers { get; set; }
        DbSet<Door> Doors { get; set; }
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync();
    }
}
