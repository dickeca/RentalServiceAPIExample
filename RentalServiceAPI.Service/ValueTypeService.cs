using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentalServiceAPI.Model;
using RentalServiceAPI.Repository.Interfaces;
using RentalServiceAPI.Service.Generics;
using RentalServiceAPI.Service.Interfaces;
using ValueType = RentalServiceAPI.Model.ValueType;

namespace RentalServiceAPI.Service
{
    public class ValueTypeService : EntityService<ValueType>, IValueTypeService
    {
        IUnitOfWork _unitOfWork;
        IValueTypeRepository _valueTypeRepository;

        public ValueTypeService(IUnitOfWork unitOfWork, IValueTypeRepository valueTypeRepository)
            : base(unitOfWork, valueTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _valueTypeRepository = valueTypeRepository;
        }
        public ValueType GetById(int id)
        {
            return _valueTypeRepository.GetById(id);
        }
    }
}
