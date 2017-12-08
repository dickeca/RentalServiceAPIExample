using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNet.Identity.EntityFramework;
using RentalServiceAPI.Model;

namespace RentalServiceAPI.Repository.Generics
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        T Add(T entity);
        T Delete(T entity);
        void Edit(T entity);
        void Save();
    }

    public interface IIdentityRepository<T> where T : IdentityUser, IEntity<string>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        T Add(T entity);
        T Delete(T entity);
        void Edit(T entity);
        void Save();
    }
}
