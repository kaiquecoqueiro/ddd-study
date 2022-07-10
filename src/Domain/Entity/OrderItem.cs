using System;

namespace DDD.Study.Domain.Entitiy
{
    public class OrderItem
    {
        public OrderItem(Guid id, string name, int price, Guid productId, int quantity)
        {
            Id = id;
            Name = name;
            Price = price;
            ProductId = productId;
            Quantity = quantity;
        }
        //TODO: CRIAR UM VALIDATE DESTA ENTIDADE
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public int Price { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }

        public int GetCalculatedPrice() => Price * Quantity;
    }
}