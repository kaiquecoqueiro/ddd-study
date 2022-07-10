using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DDD.Study.Domain.Entitiy;
using Microsoft.EntityFrameworkCore;
using src.Domain.Repository;
using src.Infrastructure.Db.Ef.Context;

namespace src.Infrastructure.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DDDStudyContext _context;

        public CustomerRepository(DDDStudyContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Customer entity, CancellationToken cancellationToken)
        {
            await _context.Customers.AddAsync(new Db.Ef.Model.CustomerModel
            {
                Id = entity.Id,
                Name = entity.Name,
                IsActive = entity.IsActive,
                RewardPoints = entity.RewardPoints,
                City = entity.Address.City,
                Zip = entity.Address.Zip,
                Number = entity.Address.Number,
                Street = entity.Address.Street
            }, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<Customer>> FindAllAsync(CancellationToken cancellationToken, int pageNumber = 0, int pageSize = 10)
        {
            var customerModel = await _context
                        .Customers
                        .AsNoTracking()
                        .Skip(pageNumber)
                        .Take(pageSize)
                        .ToListAsync(cancellationToken);


            return customerModel.Select(x =>
            {
                var customer = new Customer(x.Id, x.Name);
                customer.AddRewardPoints(x.RewardPoints);
                customer.ChangeAddress(new Address(x.Street, x.Number, x.Zip, x.City));
                if (!x.IsActive)
                    customer.Deactivate();

                return customer;
            }).ToList();
        }

        public async Task<Customer> FindAsync(Guid id, CancellationToken cancellationToken)
        {
            var customerModel = await _context.Customers.FindAsync(new object[] { id }, cancellationToken);
            if (customerModel is null)
                throw new Exception("Customer not found.");

            var customer = new Customer(customerModel.Id, customerModel.Name);
            customer.ChangeAddress(new Address(customerModel.Street, customerModel.Number, customerModel.Zip, customerModel.City));
            customer.AddRewardPoints(customerModel.RewardPoints);

            if (!customerModel.IsActive)
                customer.Deactivate();

            return customer;
        }

        public async Task UpdateAsync(Customer entity, CancellationToken cancellationToken)
        {
            var customerModel = await _context.Customers.FindAsync(new object[] { entity.Id }, cancellationToken);
            customerModel.Name = entity.Name;
            customerModel.IsActive = entity.IsActive;
            customerModel.RewardPoints = entity.RewardPoints;
            customerModel.City = entity.Address.City;
            customerModel.Zip = entity.Address.Zip;
            customerModel.Number = entity.Address.Number;
            customerModel.Street = entity.Address.Street;
            _context.Customers.Update(customerModel);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}