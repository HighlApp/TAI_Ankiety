using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Surveys.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Surveys.Infrastructure.Repositories.Interfaces;

namespace Surveys.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly SurveysContext _context;

        public RepositoryBase(SurveysContext context)
        {
            _context = context;
        }

        public T GetById(Guid id)
            => _context.Set<T>().Find(id);

        public async Task<T> GetByIdAsync(Guid id)
            => await _context.Set<T>().FindAsync(id);

        public IEnumerable<T> GetAll()
            => _context.Set<T>().AsNoTracking().ToList();

        public async Task<IEnumerable<T>> GetAllAsync()
            => await _context.Set<T>().AsNoTracking().ToListAsync();

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
            => _context.Set<T>().Where(expression).ToList();

        public async Task<IEnumerable<T>> FindByConditionAsync(
            Expression<Func<T, bool>> expression)
            => await _context.Set<T>().Where(expression).ToListAsync();

        public EntityEntry<T> Add(T entity)
            => _context.Set<T>().Add(entity);

        public async Task<EntityEntry<T>> AddAsync(T entity)
            => await _context.Set<T>().AddAsync(entity);

        public void Delete(T entity)
            => _context.Remove(entity);

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();

        public void Update(T entity)
            => _context.Update(entity);
    }
}
