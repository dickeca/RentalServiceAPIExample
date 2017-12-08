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
    public class SettingsValueService : EntityService<SettingsValue>, ISettingsValueService
    {
        IUnitOfWork _unitOfWork;
        ISettingsValueRepository _settingsValueRepository;

        public SettingsValueService(IUnitOfWork unitOfWork, ISettingsValueRepository settingsValueRepository)
            : base(unitOfWork, settingsValueRepository)
        {
            _unitOfWork = unitOfWork;
            _settingsValueRepository = settingsValueRepository;
        }
        public SettingsValue GetById(Guid id)
        {
            return _settingsValueRepository.GetById(id);
        }

        public bool ValidateUserPaymentId(Guid settingsValueId, string userId)
        {
            var settingsValue = _settingsValueRepository.GetById(settingsValueId);
            if (settingsValue == null) return false;
            //Make sure that the owner of this payment method is the user attempting to use it. 
            //And also make sure it's actually a payment method.
            return settingsValue.UserId == userId && settingsValue.ValueType.Id == 5;

        }
    }
}
