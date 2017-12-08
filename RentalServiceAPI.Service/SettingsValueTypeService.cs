using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentalServiceAPI.Model;
using RentalServiceAPI.Repository.Interfaces;
using RentalServiceAPI.Service.Generics;
using RentalServiceAPI.Service.Interfaces;

namespace RentalServiceAPI.Service
{
    public class SettingsValueTypeService : EntityService<SettingsValueType>, ISettingsValueTypeService
    {
        IUnitOfWork _unitOfWork;
        ISettingsValueTypeRepository _settingsValueTypeRepository;

        public SettingsValueTypeService(IUnitOfWork unitOfWork, ISettingsValueTypeRepository settingsValueTypeRepository)
            : base(unitOfWork, settingsValueTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _settingsValueTypeRepository = settingsValueTypeRepository;
        }
        public SettingsValueType GetById(int id)
        {
            return _settingsValueTypeRepository.GetById(id);
        }
    }
}
