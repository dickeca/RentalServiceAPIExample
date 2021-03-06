﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentalServiceAPI.Model;
using RentalServiceAPI.Service.Generics;

namespace RentalServiceAPI.Service.Interfaces
{
    public interface IUserService : IIdentityService<User>
    {
        User GetById(string id);
    }
}
