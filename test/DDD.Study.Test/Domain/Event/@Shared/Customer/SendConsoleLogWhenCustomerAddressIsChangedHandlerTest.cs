using System;
using System.IO;
using DDD.Study.Domain.Entitiy;
using FluentAssertions;
using src.Domain.Event.Customer;
using Xunit;

namespace DDD.Study.Test.Domain.Event.@Shared.Customer
{
    public class SendConsoleLogWhenCustomerAddressIsChangedHandlerTest
    {
        [Fact]
        public void Handle_WriteConsoleLogWhenCustomerAddressIsChanged()
        {
            var id = Guid.NewGuid();
            var name = "Kaique";
            var address = new Address("Str. Something", 29, "4000-234", "San Cisco");
            var customerAddressChangedEvent = new CustomerAddressChangedEvent(id, name, address);

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var stringReader = new StringReader(name);
            Console.SetIn(stringReader);

            var subject = new SendConsoleLogWhenCustomerAddressIsChangedHandler();
            subject.Handle(customerAddressChangedEvent);

            var output = stringWriter.ToString();
            output.Should().Be($"Endereço do cliente: {id}, {name} alterado para: {address}");
        }
    }
}