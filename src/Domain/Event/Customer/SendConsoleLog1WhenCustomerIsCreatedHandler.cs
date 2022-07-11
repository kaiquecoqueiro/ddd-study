using System;
using src.Domain.Event.Shared;

namespace src.Domain.Event.Customer
{
    public class SendConsoleLog1WhenCustomerIsCreatedHandler : IEventHandler<CustomerCreatedEvent>
    {
        public void Handle(CustomerCreatedEvent @event)
        {
            Console.Write("Esse é o primeiro console.log do evento: CustomerCreated");
        }
    }
}