using System;
using System.Collections.Generic;
using System.Linq;

namespace DDD.Study.Domain.Entitiy
{
    public class Order
    {
        public Order(Guid id, Guid customerId, List<OrderItem> items)
        {
            Id = id;
            CustomerId = customerId;
            _items = items;
            Validate();
        }

        private void Validate()
        {
            if (CustomerId == Guid.Empty)
                throw new InvalidOperationException("CustomerId is required.");

            if (Items?.Count == 0)
                throw new InvalidOperationException("Must have at least one item.");

            if (Id == Guid.Empty)
                throw new InvalidOperationException("Id is required.");
        }

        public Guid Id { get; set; }
        public Guid CustomerId { get; private set; }
        private List<OrderItem> _items { get; set; }
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

        public int Total() => _items.Sum(x => x.GetCalculatedPrice());
    }
}