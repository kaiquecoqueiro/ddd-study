using System;
using System.Collections.Generic;
using DDD.Study.Domain.Entitiy;
using FluentAssertions;
using Xunit;

namespace DDD.Study.Test.Entity
{
    public class OrderTest
    {
        [Fact]
        public void Order_Create_ShouldCreateOrder()
        {
            var productId = Guid.NewGuid();
            var item1Id = Guid.NewGuid();
            var item1 = new OrderItem(item1Id, "item1", 100, productId, 10);
            var item2Id = Guid.NewGuid();
            var item2 = new OrderItem(item2Id, "item2", 200, productId, 20);
            var customerId = Guid.NewGuid();

            var orderId = Guid.NewGuid();
            var subject = new Order(orderId, customerId, new List<OrderItem> { item1, item2 });

            subject.CustomerId.Should().Be(customerId);
            subject.Items.Should().HaveCount(2);
            subject.Items.Should().Contain(item1);
            subject.Items.Should().Contain(item2);
        }

        [Fact]
        public void Order_CustomerIdIsEmpty_ShouldThrowException()
        {
            var productId = Guid.NewGuid();
            var item1Id = Guid.NewGuid();
            var item1 = new OrderItem(item1Id, "item1", 100, productId, 10);
            var item2Id = Guid.NewGuid();
            var item2 = new OrderItem(item2Id, "item2", 200, productId, 20);

            var subject = Assert.Throws<InvalidOperationException>(()
                => new Order(Guid.NewGuid(), Guid.Empty, new List<OrderItem> { item1, item2 }));
            subject.Message.Should().Be("CustomerId is required.");
        }

        [Fact]
        public void Order_ItemsAreEmpty_ShouldThrowException()
        {
            var customerId = Guid.NewGuid();

            var subject = Assert.Throws<InvalidOperationException>(()
                => new Order(Guid.NewGuid(), customerId, new List<OrderItem>()));
            subject.Message.Should().Be("Must have at least one item.");
        }

        [Fact]
        public void Order_IdIsEmpty_ShouldThrowException()
        {
            var productId = Guid.NewGuid();
            var item1Id = Guid.NewGuid();
            var item1 = new OrderItem(item1Id, "item1", 100, productId, 10);
            var item2Id = Guid.NewGuid();
            var item2 = new OrderItem(item2Id, "item2", 200, productId, 20);

            var subject = Assert.Throws<InvalidOperationException>(()
                => new Order(Guid.Empty, Guid.NewGuid(), new List<OrderItem> { item1, item2 }));
            subject.Message.Should().Be("Id is required.");
        }

        [Fact]
        public void Order_Total_ShouldCalculateTotal()
        {
            var productId1 = Guid.NewGuid();
            var item1 = new OrderItem(Guid.NewGuid(), "item1", 100, productId1, 2);

            var productId2 = Guid.NewGuid();
            var item2 = new OrderItem(Guid.NewGuid(), "item2", 200, productId2, 2);
            var customerId = Guid.NewGuid();

            var subject = new Order(Guid.NewGuid(), customerId, new List<OrderItem> { item1, item2 });

            subject.Total().Should().Be(600);
        }
    }
}