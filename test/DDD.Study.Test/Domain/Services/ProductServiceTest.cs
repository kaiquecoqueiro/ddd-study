using System;
using System.Collections.Generic;
using DDD.Study.Domain.Entitiy;
using FluentAssertions;
using src.Services;
using Xunit;

namespace DDD.Study.Test.Services
{
    public class ProductServiceTest
    {
        [Fact]
        public void IncreasePrice_ChangePricesOfAllProducts()
        {
            var product1 = new Product(Guid.NewGuid(), "P1", 10);
            var product2 = new Product(Guid.NewGuid(), "P2", 20);
            var products = new List<Product> { product1, product2 };

            ProductService.IncreasePrice(products, 100);

            product1.Price.Should().Be(20);
            product2.Price.Should().Be(40);
        }
    }
}