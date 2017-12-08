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
    public class TitleService : EntityService<Title>, ITitleService
    {
        private IUnitOfWork _unitOfWork;
        private ITitleRepository _titleRepository;
        private IRentalHistoryRepository _rentalHistoryRepository;

        public TitleService(IUnitOfWork unitOfWork, ITitleRepository titleRepository, IRentalHistoryRepository rentalHistoryRepository)
            : base(unitOfWork, titleRepository)
        {
            _unitOfWork = unitOfWork;
            _titleRepository = titleRepository;
            _rentalHistoryRepository = rentalHistoryRepository;
        }

        public Title GetById(Guid id)
        {
            return _titleRepository.GetById(id);
        }


        public override void Create(Title entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entity.AvailableStock = entity.TotalStock; //no titles rented out, ergo stock is equal to total stock.
            _titleRepository.Add(entity);
            _unitOfWork.Commit();
        }

        public override void Update(Title entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            entity.AvailableStock = entity.TotalStock -
                                    _rentalHistoryRepository.FindBy(x => x.TitleId == entity.Id && !x.Returned).Count();

            _titleRepository.Edit(entity);
            _unitOfWork.Commit();
        }
    }
}
