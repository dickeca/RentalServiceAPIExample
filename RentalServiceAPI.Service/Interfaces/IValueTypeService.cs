using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentalServiceAPI.Model;
using RentalServiceAPI.Service.Generics;
using ValueType = RentalServiceAPI.Model.ValueType;

namespace RentalServiceAPI.Service.Interfaces
{
    public interface IValueTypeService : IEntityService<ValueType>
    {
        ValueType GetById(int id);
    }
}
