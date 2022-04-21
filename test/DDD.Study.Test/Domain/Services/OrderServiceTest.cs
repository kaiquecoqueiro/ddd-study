using System;
using System.Collections.Generic;
using DDD.Study.Domain.Entitiy;
using FluentAssertions;
using src.Services;
using Xunit;

namespace DDD.Study.Test.Services
{
    public class OrderServiceTest
    {
        [Fact]
        public void Total_SeveralOrder_ShouldGetTotalOfAllOrders()
        {
            var orderItem1 = new OrderItem("Item 1", 100, Guid.NewGuid(), 1);
            var order1 = new Order(Guid.NewGuid(), new List<OrderItem> { orderItem1 });

            var orderItem2 = new OrderItem("Item 2", 200, Guid.NewGuid(), 2);
            var order2 = new Order(Guid.NewGuid(), new List<OrderItem> { orderItem2 });

            var result = OrderService.GetTotal(new List<Order> { order1, order2 });
            result.Should().Be(500);
        }

        [Fact]
        public void PlaceOrder_ShouldPlaceAndOrder()
        {
            var customer = new Customer(Guid.NewGuid(), "Customer 1");
            var orderItem1 = new OrderItem("Item 1", 10, Guid.NewGuid(), 1);

            var order = OrderService.PlaceOrder(customer, new List<OrderItem> { orderItem1 });

            customer.RewardPoints.Should().Be(5);
            order.Total().Should().Be(10);
        }
    }
}