using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DDD.Study.Domain.Entitiy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using src.Infrastructure.Db.Ef.Context;
using src.Infrastructure.Repository;
using Xunit;

namespace DDD.Study.Test.Infrastructure.Repository
{
    public class OrderRepositoryTest
    {
        private readonly CancellationToken _cancellationToken = new();
        private readonly DDDStudyContext _context;
        private readonly OrderRepository _subject;

        public OrderRepositoryTest()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DDDStudyContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            _context = new(dbContextOptions.Options);
            _context.Database.EnsureCreated();

            _subject = new(_context);
        }

        [Fact]
        public void CreateAsync_ShouldCreateOrder()
        {
            var productId1 = Guid.NewGuid();
            var itemId1 = Guid.NewGuid();
            var item1 = new OrderItem(itemId1, "item1", 100, productId1, 2);

            var productId2 = Guid.NewGuid();
            var itemId2 = Guid.NewGuid();
            var item2 = new OrderItem(itemId2, "item2", 200, productId2, 2);

            var customerId = Guid.NewGuid();

            var orderId = Guid.NewGuid();
            var order = new Order(orderId, customerId, new List<OrderItem> { item1, item2 });

            _subject.CreateAsync(order, _cancellationToken).GetAwaiter().GetResult();

            var orderModel = _context.Orders.First(x => x.Id == orderId);
            orderModel.Id.Should().Be(orderId);
            orderModel.CustomerId.Should().Be(customerId);

            orderModel.Items.Should().HaveCount(2);
            orderModel.Items[0].Id.Should().Be(itemId1);
            orderModel.Items[0].Name.Should().Be("item1");
            orderModel.Items[0].Price.Should().Be(100);
            orderModel.Items[0].ProductId.Should().Be(productId1);
            orderModel.Items[0].Quantity.Should().Be(2);

            orderModel.Items[1].Id.Should().Be(itemId2);
            orderModel.Items[1].Name.Should().Be("item2");
            orderModel.Items[1].Price.Should().Be(200);
            orderModel.Items[1].ProductId.Should().Be(productId2);
            orderModel.Items[1].Quantity.Should().Be(2);
        }
    }
}