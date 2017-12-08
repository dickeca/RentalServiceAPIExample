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
    public class TitleMetaValueService : EntityService<TitleMetaValue>, ITitleMetaValueService
    {
        private IUnitOfWork _unitOfWork;
        private ITitleMetaValueRepository _titleMetaValueRepository;

        public TitleMetaValueService(IUnitOfWork unitOfWork, ITitleMetaValueRepository titleMetaValueRepository)
            : base(unitOfWork, titleMetaValueRepository)
        {
            _unitOfWork = unitOfWork;
            _titleMetaValueRepository = titleMetaValueRepository;
        }

        public TitleMetaValue GetById(Guid id)
        {
            return _titleMetaValueRepository.GetById(id);
        }
    }
}