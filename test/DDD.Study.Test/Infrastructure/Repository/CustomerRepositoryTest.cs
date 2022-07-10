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
    public class CustomerRepositoryTest
    {
        private readonly CancellationToken _cancellationToken = new();
        private readonly DDDStudyContext _context;
        private readonly CustomerRepository _subject;

        public CustomerRepositoryTest()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DDDStudyContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            _context = new(dbContextOptions.Options);
            _context.Database.EnsureCreated();

            _subject = new(_context);
        }

        [Fact]
        public void CreateAsync_ShouldCreateCustomer()
        {
            var customer = new Customer(Guid.NewGuid(), "John");
            var address = new Address("Elm Street", 123, "00010", "NY");
            customer.ChangeAddress(address);

            _subject.CreateAsync(customer, _cancellationToken).Wait();

            var customerModel = _context.Customers.FirstOrDefault(x => x.Id == customer.Id);
            customerModel.Name.Should().Be("John");
            customerModel.IsActive.Should().BeTrue();
            customerModel.Number.Should().Be(123);
            customerModel.Zip.Should().Be("00010");
            customerModel.RewardPoints.Should().Be(0);
            customerModel.Street.Should().Be("Elm Street");
            customerModel.City.Should().Be("NY");
        }

        [Fact]
        public void UpdateAsync_ShouldUpdateCustomer()
        {
            var customer = new Customer(Guid.NewGuid(), "John");
            var address = new Address("Elm Street", 123, "00010", "NY");
            customer.ChangeAddress(address);
            _subject.CreateAsync(customer, _cancellationToken).Wait();

            customer.ChangeName("Jane");
            _subject.UpdateAsync(customer, _cancellationToken).Wait();

            var customerModel = _context.Customers.FirstOrDefault(x => x.Id == customer.Id);
            customerModel.Name.Should().Be("Jane");
            customerModel.IsActive.Should().BeTrue();
            customerModel.Number.Should().Be(123);
            customerModel.Zip.Should().Be("00010");
            customerModel.RewardPoints.Should().Be(0);
            customerModel.Street.Should().Be("Elm Street");
            customerModel.City.Should().Be("NY");
        }

        [Fact]
        public void FindAsync_ShouldFindCustomer()
        {
            var customer = new Customer(Guid.NewGuid(), "John");
            var address = new Address("Elm Street", 123, "00010", "NY");
            customer.ChangeAddress(address);
            customer.Deactivate();
            _subject.CreateAsync(customer, _cancellationToken).Wait();

            var result = _subject.FindAsync(customer.Id, _cancellationToken).Result;
            result.Name.Should().Be("John");
            result.IsActive.Should().BeFalse();
            result.RewardPoints.Should().Be(0);
            result.Address.Number.Should().Be(123);
            result.Address.Zip.Should().Be("00010");
            result.Address.Street.Should().Be("Elm Street");
            result.Address.City.Should().Be("NY");
        }

        [Fact]
        public void FindAllAsync_ShouldFindAllCustomers()
        {
            var customer1 = new Customer(Guid.NewGuid(), "John");
            var address1 = new Address("Elm Street", 123, "00010", "NY");
            customer1.ChangeAddress(address1);
            customer1.AddRewardPoints(10);
            _subject.CreateAsync(customer1, _cancellationToken).Wait();

            var customer2 = new Customer(Guid.NewGuid(), "Jane");
            var address2 = new Address("Sesame Street", 90, "10123", "Chicago");
            customer2.ChangeAddress(address2);
            customer2.AddRewardPoints(20);
            customer2.Deactivate();
            _subject.CreateAsync(customer2, _cancellationToken).Wait();

            var result = _subject.FindAllAsync(_cancellationToken).Result;
            result.Should().HaveCount(2);
            result[0].Id.Should().Be(customer1.Id);
            result[0].Name.Should().Be("John");
            result[0].IsActive.Should().BeTrue();
            result[0].RewardPoints.Should().Be(10);
            result[0].Address.Number.Should().Be(123);
            result[0].Address.Zip.Should().Be("00010");
            result[0].Address.Street.Should().Be("Elm Street");
            result[0].Address.City.Should().Be("NY");

            result[1].Id.Should().Be(customer2.Id);
            result[1].Name.Should().Be("Jane");
            result[1].IsActive.Should().BeFalse();
            result[1].RewardPoints.Should().Be(20);
            result[1].Address.Number.Should().Be(90);
            result[1].Address.Zip.Should().Be("10123");
            result[1].Address.Street.Should().Be("Sesame Street");
            result[1].Address.City.Should().Be("Chicago");
        }

        [Fact]
        public void FindAsync_CustomerNotFound_ShouldThrowException()
        {
            var inexistentCustomer = Guid.NewGuid();

            var result = Assert.Throws<Exception>(() => _subject.FindAsync(
                inexistentCustomer,
                _cancellationToken).GetAwaiter().GetResult());

            result.Message.Should().Be("Customer not found.");
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}