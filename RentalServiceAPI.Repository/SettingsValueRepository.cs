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
    public class SettingsValueRepository : GenericRepository<SettingsValue>, ISettingsValueRepository
    {
        public SettingsValueRepository(DbContext context) : base(context)
        {
        }

        public override IEnumerable<SettingsValue> GetAll()
        {
            return _entities.Set<SettingsValue>()
                .Include(x => x.User)
                .Include(x => x.SettingsValueType)
                .AsEnumerable();
        }

        public SettingsValue GetById(Guid id)
        {
            return _dbset.Include(x => x.User)
                .Include(x => x.SettingsValueType)
                .FirstOrDefault(x => x.Id == id);         
        }
    }
}
