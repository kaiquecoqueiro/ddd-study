using System;

namespace src.Infrastructure.Db.Ef.Model
{
    public class OrderItemModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }

        public Guid ProductId { get; set; }
        public ProductModel Product { get; set; }
        public Guid OrderId { get; set; }
        public OrderModel Order { get; set; }
    }
}