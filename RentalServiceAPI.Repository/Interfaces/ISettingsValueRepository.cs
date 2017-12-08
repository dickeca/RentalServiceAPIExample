﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentalServiceAPI.Model;
using RentalServiceAPI.Repository.Generics;

namespace RentalServiceAPI.Repository.Interfaces
{
    public interface ISettingsValueRepository : IGenericRepository<SettingsValue>
    {
        SettingsValue GetById(Guid id);
    }
}
