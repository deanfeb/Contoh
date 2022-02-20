using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUnitRepository : IGenericRepository<Unit>
    {
        IQueryable<Unit> GetQueryable();
    }
}
