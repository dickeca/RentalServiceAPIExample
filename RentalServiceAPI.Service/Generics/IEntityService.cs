using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using RentalServiceAPI.Model;

namespace RentalServiceAPI.Service.Generics
{
    public interface IEntityService<T> : IService
    where T : BaseEntity
    {
        void Create(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll();
        void Update(T entity);
    }

    public interface IIdentityService<T> : IService
        where T : IdentityUser, IEntity<string>
    {
        void Create(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll();
        void Update(T entity);
    }
}
