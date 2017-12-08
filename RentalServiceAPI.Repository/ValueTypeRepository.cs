using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentalServiceAPI.Model;
using RentalServiceAPI.Repository.Generics;
using RentalServiceAPI.Repository.Interfaces;
using ValueType = RentalServiceAPI.Model.ValueType;

namespace RentalServiceAPI.Repository
{
    public class ValueTypeRepository : GenericRepository<ValueType>, IValueTypeRepository
    {
        public ValueTypeRepository(DbContext context) : base(context)
        {
        }

        public ValueType GetById(int id)
        {
            return _dbset.FirstOrDefault(x => x.Id == id);
        }
    }
}
