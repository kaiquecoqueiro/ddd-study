using System;
using DDD.Study.Domain.Entitiy;
using FluentAssertions;
using Xunit;

namespace DDD.Study.Test.Entity
{
    public class ProductTest
    {
        [Fact]
        public void Product_Create_ShouldCreateProduct()
        {
            var subject = new Product(Guid.NewGuid(), "Product 1", 10);
            subject.Name.Should().Be("Product 1");
            subject.Price.Should().Be(10);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Product_BlankName_ShouldThrowException(string name)
        {
            var result = Assert.Throws<InvalidOperationException>(() => new Product(Guid.NewGuid(), name, 10));
            result.Message.Should().Be("Name is required.");
        }

        [Fact]
        public void Product_EmptyId_ShouldThrowException()
        {
            var result = Assert.Throws<InvalidOperationException>(() => new Product(Guid.Empty, "Product 1", 10));
            result.Message.Should().Be("Id is required.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Product_InvalidPrice_ShouldThrowException(int price)
        {
            var result = Assert.Throws<InvalidOperationException>(() => new Product(Guid.NewGuid(), "Product 1", price));
            result.Message.Should().Be("Price must be greater than zero.");
        }

        [Fact]
        public void Product_ChangeName_ShouldChangeName()
        {
            var subject = new Product(Guid.NewGuid(), "Product 1", 10);
            subject.ChangeName("Product 2");
            subject.Name.Should().Be("Product 2");
        }

        [Fact]
        public void Product_ChangePrice_ShouldChangePrice()
        {
            var subject = new Product(Guid.NewGuid(), "Product 1", 10);
            subject.ChangePrice(20);
            subject.Price.Should().Be(20);
        }
    }
}