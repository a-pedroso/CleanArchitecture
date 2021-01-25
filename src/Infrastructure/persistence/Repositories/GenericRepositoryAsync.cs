using CleanArchitecture.Application.Common.Interfaces.Repositories;
using CleanArchitecture.Application.Common.Wrappers;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Persistence.Repositories
{
    public class GenericRepositoryAsync<T, TKey> : IGenericRepositoryAsync<T, TKey> 
        where T : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepositoryAsync(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> GetByIdAsync(TKey id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<PagedResponse<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            // TODO: refactor this. 2 roundtrips???

            int totalCount = await _dbContext
                .Set<T>()
                .AsNoTracking()
                .CountAsync();

            var data = await _dbContext
                .Set<T>()
                .OrderBy(o => o.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return new PagedResponse<T>(pageNumber, pageSize, totalCount, data);
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext
                 .Set<T>()
                 .ToListAsync();
        }
    }
}
