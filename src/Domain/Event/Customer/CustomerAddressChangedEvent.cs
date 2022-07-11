using System;
using DDD.Study.Domain.Entitiy;
using src.Domain.Event.Shared;

namespace src.Domain.Event.Customer
{
    public class CustomerAddressChangedEvent : IEvent
    {
        public CustomerAddressChangedEvent(Guid id, string name, Address address)
        {
            Id = id;
            Name = name;
            Address = address;
        }

        public Guid Id { get; }
        public string Name { get; }
        public Address Address { get; }

        public DateTime DateTimeOcurred => DateTime.Now;
    }
}