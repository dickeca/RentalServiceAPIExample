using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using RentalServiceAPI.Model;
using RentalServiceAPI.Repository.Generics;
using RentalServiceAPI.Repository.Interfaces;

namespace RentalServiceAPI.Repository
{
    public class UserRepository : IdentityRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public override IEnumerable<User> GetAll()
        {
            return _entities.Set<User>()
                .Include(x => x.RentalHistories)
                .Include(x => x.SettingsValues)
                .AsEnumerable();
        }
        public User GetById(string id)
        {
            return
                _dbset
                    .Include(x => x.RentalHistories)
                    .Include(x => x.SettingsValues)
                    .FirstOrDefault(x => x.Id == id);
            ;
        }

        public override User Add(User entity)
        {
            if (!Thread.CurrentPrincipal.IsInRole("Admin"))
            {
                throw new AuthenticationException("Invalid User Role");
            }
            return _dbset.Add(entity);
        }

        public override User Delete(User entity)
        {
            if (!Thread.CurrentPrincipal.IsInRole("Admin"))
            {
                throw new AuthenticationException("Invalid User Role");
            }
            return _dbset.Remove(entity);
        }

        public override void Edit(User entity)
        {
            var admin = Thread.CurrentPrincipal.IsInRole("Admin");
            if (admin)
            {
                _entities.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            }
            else if (Thread.CurrentPrincipal.Identity.GetUserId() == entity.Id)
            {
                _entities.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                throw new AuthenticationException("Invalid User Role");
            }
        }
    }
}
