using System;
using System.Linq;
using System.Linq.Expressions;
using carShop.Context;
using Microsoft.EntityFrameworkCore;

namespace carShop.Data
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T: class
    {
        private readonly CarShopContext context;

        public RepositoryBase(CarShopContext context)
        {
            this.context = context;
        }
        public void Create(T item)
        {
            context.Add(item);
        }

        public void Delete(T item)
        {
            context.Remove(item);
        }

        public T Get(Expression<Func<T, bool>> expression, bool trackChanges = true)
        {
            return trackChanges ?
                context.Set<T>().FirstOrDefault(expression) :
                context.Set<T>().AsNoTracking().FirstOrDefault(expression);
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression, bool trackChanges = true)
        {
            return trackChanges ?
                context
                    .Set<T>()
                    .AsQueryable()
                    .Where(expression) :
                context
                    .Set<T>()
                    .AsNoTracking()
                    .AsQueryable()
                    .Where(expression);
        }

        public IQueryable<T> GetAll(bool trackChanges = true)
        {
            return trackChanges ?
                context
                    .Set<T>()
                    .AsQueryable() :
                context
                    .Set<T>()
                    .AsNoTracking()
                    .AsQueryable();
        }

        public void Update(T item)
        {
            context.Update(item);
        }
    }
}