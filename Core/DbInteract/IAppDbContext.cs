using System.Data;

namespace Core.DbInteract
{
    public interface IAppDbContext
    {
        IDbConnection Connection { get; }
    }
}
