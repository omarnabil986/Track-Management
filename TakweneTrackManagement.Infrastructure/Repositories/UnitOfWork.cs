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
    internal class UnitOfWork(TrackManagerDbContext dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> repositories = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var typeName = typeof(TEntity).Name;
            if (repositories.TryGetValue(typeName, out object? value))
                return (IGenericRepository<TEntity, TKey>)value;
            else
            {
                var repo = new GenericRepository<TEntity, TKey>(dbContext);
                repositories[typeName] = repo;
                return repo;
            }
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct = default) => await dbContext.SaveChangesAsync(ct);
    }
}
