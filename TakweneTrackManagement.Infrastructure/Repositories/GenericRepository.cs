using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakweneTrackManagement.Domain.Common;
using TakweneTrackManagement.Domain.Contracts;
using TakweneTrackManagement.Infrastructure.Data;

namespace TakweneTrackManagement.Infrastructure.Repositories
{
    internal class GenericRepository<TEntity, TKey>(TrackManagerDbContext dbContext) : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public void Add(TEntity entity) => dbContext.Set<TEntity>().Add(entity);

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default)
         => await dbContext.Set<TEntity>().ToListAsync(ct);

        public async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct = default)
         => await dbContext.Set<TEntity>().FindAsync(id, ct);
        public void Remove(TEntity entity) => dbContext.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity) => dbContext.Set<TEntity>().Update(entity);
    }
}
