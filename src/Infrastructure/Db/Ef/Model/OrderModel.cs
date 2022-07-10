using System;
using System.Collections.Generic;

namespace src.Infrastructure.Db.Ef.Model
{
    public class OrderModel
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public List<OrderItemModel> Items { get; set; }
    }
}