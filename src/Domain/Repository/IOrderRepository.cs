using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DDD.Study.Domain.Entitiy;

namespace src.Domain.Repository
{
    public interface IOrderRepository
    {
        Task CreateAsync(Order entity, CancellationToken cancellationToken);
        Task<Order> FindAsync(Guid id, CancellationToken cancellationToken);
        Task<IList<Order>> FindAllAsync(CancellationToken cancellationToken, int pageNumber = 0, int pageSize = 10);
    }
}