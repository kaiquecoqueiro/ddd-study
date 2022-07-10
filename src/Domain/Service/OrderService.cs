using System;
using System.Collections.Generic;
using System.Linq;
using DDD.Study.Domain.Entitiy;

namespace src.Services
{
    public class OrderService
    {
        public static int GetTotal(List<Order> orders)
            => orders.Sum(order => order.Total());

        public static Order PlaceOrder(Customer customer, List<OrderItem> items)
        {
            if (!items.Any())
                throw new ArgumentException("Order must have at least one item", nameof(items));

            var order = new Order(Guid.NewGuid(), customer.Id, items);
            customer.AddRewardPoints(order.Total() / 2);

            return order;
        }
    }
}