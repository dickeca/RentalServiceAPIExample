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
    }
}
