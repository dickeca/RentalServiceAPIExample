using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentalServiceAPI.Model;
using RentalServiceAPI.Repository.Generics;
using RentalServiceAPI.Repository.Interfaces;
using ValueType = System.ValueType;

namespace RentalServiceAPI.Repository
{
    public class TitleMetaValueRepository : GenericRepository<TitleMetaValue>, ITitleMetaValueRepository
    {
        public TitleMetaValueRepository(DbContext context) : base(context)
        {
        }

        public TitleMetaValue GetById(Guid id)
        {
            return _dbset.Include(x=> x.Title).FirstOrDefault(x => x.Id == id);
        }
    }
}
