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
    public class SettingsValueTypeRepository : GenericRepository<SettingsValueType>, ISettingsValueTypeRepository
    {
        public SettingsValueTypeRepository(DbContext context) : base(context)
        {
        }

        public SettingsValueType GetById(int id)
        {
            return _dbset.FirstOrDefault(x => x.Id == id);
        }
    }
}
