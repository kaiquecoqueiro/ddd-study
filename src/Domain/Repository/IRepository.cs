using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace src.Domain.Repository
{
    public interface IRepository<T>
    {
        Task CreateAsync(T entity, CancellationToken cancellationToken);
        Task UpdateAsync(T entity, CancellationToken cancellationToken);
        Task<T> FindAsync(Guid id, CancellationToken cancellationToken);
        Task<IList<T>> FindAllAsync(CancellationToken cancellationToken, int pageNumber, int pageSize);
    }
}