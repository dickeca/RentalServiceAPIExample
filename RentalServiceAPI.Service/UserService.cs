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
    public class UserService : IdentityService<User>, IUserService
    {
        IUnitOfWork _unitOfWork;
        IUserRepository _userRepository;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(unitOfWork, userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;       
        }
        public User GetById(string id)
        {
            return _userRepository.GetById(id);
        }
    }
}
