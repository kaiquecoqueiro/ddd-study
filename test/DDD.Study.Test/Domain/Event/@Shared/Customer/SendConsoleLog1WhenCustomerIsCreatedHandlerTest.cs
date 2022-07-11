using System;
using System.IO;
using FluentAssertions;
using src.Domain.Event.Customer;
using Xunit;

namespace DDD.Study.Test.Domain.Event.@Shared.Customer
{
    public class SendConsoleLog1WhenCustomerIsCreatedHandlerTest
    {
        [Fact]
        public void Handle_WriteConsoleLogWhenCustomerIsCreated()
        {
            var customerCreatedEvent = new CustomerCreatedEvent();

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var subject = new SendConsoleLog1WhenCustomerIsCreatedHandler();
            subject.Handle(customerCreatedEvent);

            var output = stringWriter.ToString();
            output.Should().Be("Esse é o primeiro console.log do evento: CustomerCreated");
        }
    }
}