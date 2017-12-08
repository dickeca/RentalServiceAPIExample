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
    public class RentalHistoryService : EntityService<RentalHistory>, IRentalHistoryService
    {
        IUnitOfWork _unitOfWork;
        IRentalHistoryRepository _rentalHistoryRepository;

        public RentalHistoryService(IUnitOfWork unitOfWork, IRentalHistoryRepository rentalHistoryRepository)
            : base(unitOfWork, rentalHistoryRepository)
        {
            _unitOfWork = unitOfWork;
            _rentalHistoryRepository = rentalHistoryRepository;
        }
        public RentalHistory GetById(Guid id)
        {
            return _rentalHistoryRepository.GetById(id);
        }

        public IEnumerable<RentalHistory> GetByUserId(string id)
        {
            return _rentalHistoryRepository.FindBy(x => x.UserId == id);
        }
    }
}
