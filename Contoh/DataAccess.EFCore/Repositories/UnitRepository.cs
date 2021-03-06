

using DataAccess.EFCore;
using Domain.Entities;
using Domain.Interfaces;

namespace DataAccess.EFCore.Repositories
{
    public class UnitRepository : GenericRepository<Unit>, IUnitRepository
    {
        public UnitRepository(ApplicationContext context) : base(context)
        {
        }

        public IQueryable<Unit> GetQueryable()
        {
            var query = from a in _context.Units
                        select a;

            return query;
        }
    }
}
