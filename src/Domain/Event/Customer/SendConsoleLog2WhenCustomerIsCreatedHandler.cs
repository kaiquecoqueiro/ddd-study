using System;
using src.Domain.Event.Shared;

namespace src.Domain.Event.Customer
{
    public class SendConsoleLog2WhenCustomerIsCreatedHandler : IEventHandler<CustomerCreatedEvent>
    {
        public void Handle(CustomerCreatedEvent @event)
        {
            Console.Write("Esse é o segundo console.log do evento: CustomerCreated");
        }
    }
}