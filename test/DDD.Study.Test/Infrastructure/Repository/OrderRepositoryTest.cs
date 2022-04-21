using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DDD.Study.Domain.Entitiy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using src.Infrastructure.Db.Ef.Context;
using src.Infrastructure.Repository;
using Xunit;

namespace DDD.Study.Test.Infrastructure.Repository
{
    public class OrderRepositoryTest : IDisposable
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
        public async Task CreateAsync_ShouldCreateOrder()
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

            await _subject.CreateAsync(order, _cancellationToken);

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

        [Fact]
        public async Task CreateAsync_PassingNullOrder_ShouldThrowException()
        {
            var result = await Assert.ThrowsAsync<ArgumentNullException>(() => _subject.CreateAsync(
                null,
                _cancellationToken));

            result.Message.Should().Be("An order must be informed. (Parameter 'entity')");
        }

        [Fact]
        public async Task FindAsync_ShouldFindOrder()
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
            _subject.CreateAsync(order, _cancellationToken).Wait();

            var result = await _subject.FindAsync(order.Id, _cancellationToken);
            result.Id.Should().Be(orderId);
            result.CustomerId.Should().Be(customerId);

            result.Items.Should().HaveCount(2);
            var firstItem = result.Items.First();
            firstItem.Id.Should().Be(itemId1);
            firstItem.Name.Should().Be("item1");
            firstItem.Price.Should().Be(100);
            firstItem.ProductId.Should().Be(productId1);
            firstItem.Quantity.Should().Be(2);

            var lastItem = result.Items.Last();
            lastItem.Id.Should().Be(itemId2);
            lastItem.Name.Should().Be("item2");
            lastItem.Price.Should().Be(200);
            lastItem.ProductId.Should().Be(productId2);
            lastItem.Quantity.Should().Be(2);
        }

        [Fact]
        public async Task FindAsync_OrderNotFound_ShouldThrowException()
        {
            var inexistentOrder = Guid.NewGuid();

            var result = await Assert.ThrowsAsync<Exception>(() => _subject.FindAsync(
                inexistentOrder,
                _cancellationToken));

            result.Message.Should().Be("Order not found.");
        }

        [Fact]
        public async Task FindAllAsync_ShouldFindAllOrder()
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
            await _subject.CreateAsync(order, _cancellationToken);

            var productId3 = Guid.NewGuid();
            var itemId3 = Guid.NewGuid();
            var item3 = new OrderItem(itemId3, "item3", 300, productId3, 1);
            var productId4 = Guid.NewGuid();
            var itemId4 = Guid.NewGuid();
            var item4 = new OrderItem(itemId4, "item4", 500, productId4, 5);
            var customerId2 = Guid.NewGuid();
            var orderId2 = Guid.NewGuid();
            var order2 = new Order(orderId2, customerId2, new List<OrderItem> { item3, item4 });
            await _subject.CreateAsync(order2, _cancellationToken);

            var result = await _subject.FindAllAsync(_cancellationToken);
            result.Should().HaveCount(2);

            var firstOrder = result.First();
            firstOrder.Id.Should().Be(orderId);
            firstOrder.CustomerId.Should().Be(customerId);
            firstOrder.Items.Should().HaveCount(2);
            var firstItem = firstOrder.Items.First();
            firstItem.Id.Should().Be(itemId1);
            firstItem.Name.Should().Be("item1");
            firstItem.Price.Should().Be(100);
            firstItem.ProductId.Should().Be(productId1);
            firstItem.Quantity.Should().Be(2);
            var lastItem = firstOrder.Items.Last();
            lastItem.Id.Should().Be(itemId2);
            lastItem.Name.Should().Be("item2");
            lastItem.Price.Should().Be(200);
            lastItem.ProductId.Should().Be(productId2);
            lastItem.Quantity.Should().Be(2);

            var lastOrder = result.Last();
            lastOrder.Id.Should().Be(orderId2);
            lastOrder.CustomerId.Should().Be(customerId2);
            lastOrder.Items.Should().HaveCount(2);
            firstItem = lastOrder.Items.First();
            firstItem.Id.Should().Be(itemId4);
            firstItem.Name.Should().Be("item4");
            firstItem.Price.Should().Be(500);
            firstItem.ProductId.Should().Be(productId4);
            firstItem.Quantity.Should().Be(5);
            lastItem = lastOrder.Items.Last();
            lastItem.Id.Should().Be(itemId3);
            lastItem.Name.Should().Be("item3");
            lastItem.Price.Should().Be(300);
            lastItem.ProductId.Should().Be(productId3);
            lastItem.Quantity.Should().Be(1);
        }

        [Fact]
        public async Task FindAllAsync_Paging_ShouldFindTheOrderThatIsInTheSecondPage()
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
            await _subject.CreateAsync(order, _cancellationToken);

            var productId3 = Guid.NewGuid();
            var itemId3 = Guid.NewGuid();
            var item3 = new OrderItem(itemId3, "item3", 300, productId3, 1);
            var productId4 = Guid.NewGuid();
            var itemId4 = Guid.NewGuid();
            var item4 = new OrderItem(itemId4, "item4", 500, productId4, 5);
            var customerId2 = Guid.NewGuid();
            var orderId2 = Guid.NewGuid();
            var order2 = new Order(orderId2, customerId2, new List<OrderItem> { item3, item4 });
            await _subject.CreateAsync(order2, _cancellationToken);

            var result = await _subject.FindAllAsync(_cancellationToken, 1, 1);
            result.Should().HaveCount(1);
            var lastOrder = result.Last();
            lastOrder.Id.Should().Be(orderId2);
            lastOrder.CustomerId.Should().Be(customerId2);
            lastOrder.Items.Should().HaveCount(2);
            var firstItem = lastOrder.Items.First();
            firstItem.Id.Should().Be(itemId4);
            firstItem.Name.Should().Be("item4");
            firstItem.Price.Should().Be(500);
            firstItem.ProductId.Should().Be(productId4);
            firstItem.Quantity.Should().Be(5);
            var lastItem = lastOrder.Items.Last();
            lastItem.Id.Should().Be(itemId3);
            lastItem.Name.Should().Be("item3");
            lastItem.Price.Should().Be(300);
            lastItem.ProductId.Should().Be(productId3);
            lastItem.Quantity.Should().Be(1);
        }

        [Fact]
        public async Task FindAllAsync_NoOrders_ShouldReturnEmptyList()
        {
            var result = await _subject.FindAllAsync(_cancellationToken);
            result.Should().HaveCount(0);
            result.Should().BeEmpty();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}