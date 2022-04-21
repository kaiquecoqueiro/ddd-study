using System;

namespace DDD.Study.Domain.Entitiy
{
    public class OrderItem
    {
        public OrderItem(string name, int price, Guid productId, int quantity)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            ProductId = productId;
            Quantity = quantity;
        }

        private Guid Id { get; set; }
        public string Name { get; private set; }
        private int Price { get; set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }

        public int GetPrice() => Price * Quantity;
    }
}