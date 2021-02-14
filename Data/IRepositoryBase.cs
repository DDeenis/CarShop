using System;
using System.Linq;
using System.Linq.Expressions;

namespace carShop.Data
{
    public interface IRepositoryBase<T>
    {
        T Get(Expression<Func<T, bool>> expression, bool trackChanges = true);
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression, bool trackChanges = true);
        IQueryable<T> GetAll(bool trackChanges = true);

        void Create(T item);
        void Update(T item);
        void Delete(T item);
    }
}