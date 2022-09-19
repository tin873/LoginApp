using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data;

namespace login.EntiryFrameWorkCore.EntityFramWorkCore
{
    public interface IIdentityCtmDbContext
    {
        IDbConnection Connection { get; }
        DatabaseFacade Database { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        int SaveChanges();
    }
}
