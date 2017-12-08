using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentalServiceAPI.Model;
using RentalServiceAPI.Repository.Generics;
using ValueType = RentalServiceAPI.Model.ValueType;

namespace RentalServiceAPI.Repository.Interfaces
{
    public interface IValueTypeRepository : IGenericRepository<ValueType>
    {
        ValueType GetById(int id);
    }
}
