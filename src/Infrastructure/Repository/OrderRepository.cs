using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DDD.Study.Domain.Entitiy;
using Microsoft.EntityFrameworkCore;
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
            if (entity is null)
                throw new ArgumentNullException(nameof(entity), "An order must be informed.");

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

        public async Task<IList<Order>> FindAllAsync(CancellationToken cancellationToken, int pageNumber = 0, int pageSize = 10)
        {
            var orderModels = await _context
            .Orders
            .Include(x => x.Items)
            .AsNoTracking()
            .Skip(pageNumber)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

            return orderModels.Select(orderModel =>
            {
                var items = orderModel.Items.Select(item
                    => new OrderItem(item.Id, item.Name, item.Price, item.ProductId, item.Quantity));

                return new Order(orderModel.Id, orderModel.CustomerId, items.ToList());
            }).ToList();
        }

        public async Task<Order> FindAsync(Guid id, CancellationToken cancellationToken)
        {
            var orderModel = await _context.Orders.Include(x => x.Items).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (orderModel is null)
                throw new Exception("Order not found.");

            var itemsModel = orderModel.Items.Select(x => new OrderItem(x.Id, x.Name, x.Price, x.ProductId, x.Quantity)).ToList();

            return new Order(orderModel.Id, orderModel.CustomerId, itemsModel);
        }
    }
}