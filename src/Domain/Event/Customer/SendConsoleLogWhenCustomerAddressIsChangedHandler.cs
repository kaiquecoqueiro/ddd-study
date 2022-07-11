using System;
using src.Domain.Event.Shared;

namespace src.Domain.Event.Customer
{
    public class SendConsoleLogWhenCustomerAddressIsChangedHandler : IEventHandler<CustomerAddressChangedEvent>
    {
        public void Handle(CustomerAddressChangedEvent @event)
        {
            Console.Write($"Endereço do cliente: {@event.Id}, {@event.Name} alterado para: {@event.Address}");
        }
    }
}