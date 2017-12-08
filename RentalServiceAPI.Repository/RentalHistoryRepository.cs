using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentalServiceAPI.Model;
using RentalServiceAPI.Repository.Generics;
using RentalServiceAPI.Repository.Interfaces;

namespace RentalServiceAPI.Repository
{
    public class RentalHistoryRepository : GenericRepository<RentalHistory>, IRentalHistoryRepository
    {
        public RentalHistoryRepository(DbContext context) : base(context)
        {
        }

        public override IEnumerable<RentalHistory> GetAll()
        {
            return _entities.Set<RentalHistory>()
                .Include(x => x.User)
                .Include(x => x.Title)
                .AsEnumerable();
        }

        public RentalHistory GetById(Guid id)
        {
            return _dbset.Include(x => x.User)
                .Include(x => x.Title)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
