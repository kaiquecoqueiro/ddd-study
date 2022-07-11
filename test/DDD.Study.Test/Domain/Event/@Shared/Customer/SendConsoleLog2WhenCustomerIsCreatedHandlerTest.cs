using System;
using System.IO;
using FluentAssertions;
using src.Domain.Event.Customer;
using Xunit;

namespace DDD.Study.Test.Domain.Event.@Shared.Customer
{
    public class SendConsoleLog2WhenCustomerIsCreatedHandlerTest
    {
        [Fact]
        public void Handle_WriteConsoleLogWhenCustomerIsCreated()
        {
            var customerCreatedEvent = new CustomerCreatedEvent();

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var subject = new SendConsoleLog2WhenCustomerIsCreatedHandler();
            subject.Handle(customerCreatedEvent);

            var output = stringWriter.ToString();
            output.Should().Be("Esse é o segundo console.log do evento: CustomerCreated");
        }
    }
}