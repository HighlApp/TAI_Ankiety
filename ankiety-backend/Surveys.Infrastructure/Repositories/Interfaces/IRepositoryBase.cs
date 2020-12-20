using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Surveys.Infrastructure.Repositories.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        T GetById(int id);

        Task<T> GetByIdAsync(int id);

        IEnumerable<T> GetAll();

        Task<IEnumerable<T>> GetAllAsync();

        IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression);

        Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);

        EntityEntry<T> Add(T item);

        Task<EntityEntry<T>> AddAsync(T item);

        void Delete(T item);

        void Update(T item);

        Task SaveAsync();
    }
}
