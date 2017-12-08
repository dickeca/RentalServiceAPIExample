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
    public class TitleRepository : GenericRepository<Title>, ITitleRepository
    {
        public TitleRepository(DbContext context) : base(context)
        {
        }
        public override IEnumerable<Title> GetAll()
        {
            return _entities.Set<Title>().Include(x=> x.TitleMetaValues)
                .AsEnumerable();
        }

        public Title GetById(Guid id)
        {
            return _dbset.Include(x=> x.TitleMetaValues)
                    .FirstOrDefault(x => x.Id == id);
        }
    }
}
