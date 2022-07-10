using System;
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
    public class ProductRepositoryTest : IDisposable
    {
        private readonly CancellationToken _cancellationToken = new();
        private readonly DDDStudyContext _context;
        private readonly ProductRepository _subject;

        public ProductRepositoryTest()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DDDStudyContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            _context = new(dbContextOptions.Options);
            _context.Database.EnsureCreated();

            _subject = new(_context);
        }

        [Fact]
        public void CreateAsync_ShouldCreateProduct()
        {
            var product = new Product(Guid.NewGuid(), "Product1", 10);

            _subject.CreateAsync(product, _cancellationToken).Wait();

            var productModel = _context.Products.FirstOrDefault(x => x.Id == product.Id);
            productModel.Name.Should().Be("Product1");
            productModel.Price.Should().Be(10);
        }

        [Fact]
        public void UpdateAsync_ShouldUpdateProduct()
        {
            var product = new Product(Guid.NewGuid(), "Product1", 10);
            _subject.CreateAsync(product, _cancellationToken).Wait();

            product.ChangeName("Product2");
            product.ChangePrice(20);
            _subject.UpdateAsync(product, _cancellationToken).Wait();

            var productModel = _context.Products.FirstOrDefault(x => x.Id == product.Id);
            productModel.Name.Should().Be("Product2");
            productModel.Price.Should().Be(20);
        }

        [Fact]
        public void FindAsync_ShouldFindProduct()
        {
            var product = new Product(Guid.NewGuid(), "Product1", 10);
            _subject.CreateAsync(product, _cancellationToken).Wait();

            var foundProduct = _subject.FindAsync(product.Id, _cancellationToken).Result;
            foundProduct.Name.Should().Be("Product1");
            foundProduct.Price.Should().Be(10);
        }

        [Fact]
        public void FindAllAsync_ShouldFindAllProducts()
        {
            var product1 = new Product(Guid.NewGuid(), "Product1", 10);
            _subject.CreateAsync(product1, _cancellationToken).Wait();

            var product2 = new Product(Guid.NewGuid(), "Product2", 20);
            _subject.CreateAsync(product2, _cancellationToken).Wait();

            var result = _subject.FindAllAsync(_cancellationToken).Result;
            result.Should().HaveCount(2);
            result[0].Name.Should().Be("Product1");
            result[0].Price.Should().Be(10);
            result[1].Name.Should().Be("Product2");
            result[1].Price.Should().Be(20);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}