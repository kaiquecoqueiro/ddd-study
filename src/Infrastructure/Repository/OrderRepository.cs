using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DDD.Study.Domain.Entitiy;
using src.Domain.Repository;
using src.Infrastructure.Db.Ef.Context;
using src.Infrastructure.Db.Ef.Model;

namespace src.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DDDStudyContext _context;

        public OrderRepository(DDDStudyContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Order entity, CancellationToken cancellationToken)
        {
            var orderModel = new OrderModel
            {
                Id = entity.Id,
                CustomerId = entity.CustomerId,
                Items = entity.Items.Select(x => new OrderItemModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity
                }).ToList()
            };
            await _context.AddAsync(orderModel, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task<IList<Order>> FindAllAsync(CancellationToken cancellationToken, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<Order> FindAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Order entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}