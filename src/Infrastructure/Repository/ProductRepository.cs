using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DDD.Study.Domain.Entitiy;
using Microsoft.EntityFrameworkCore;
using src.Domain.Repository;
using src.Infrastructure.Db.Ef.Context;
using src.Infrastructure.Db.Ef.Model;

namespace src.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository, IDisposable
    {
        private readonly DDDStudyContext _context;

        public ProductRepository(DDDStudyContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Product entity, CancellationToken cancellationToken)
        {
            await _context.Products.AddAsync(new ProductModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price
            }, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<Product>> FindAllAsync(CancellationToken cancellationToken, int pageNumber = 0, int pageSize = 10)
        {
            var productsModel = await _context
                                    .Products
                                    .AsNoTracking()
                                    .Skip(pageNumber)
                                    .Take(pageSize)
                                    .ToListAsync(cancellationToken);

            return productsModel.Select(product => new Product(product.Id, product.Name, product.Price)).ToList();
        }

        public async Task<Product> FindAsync(Guid id, CancellationToken cancellationToken)
        {
            var productModel = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            return new Product(productModel.Id, productModel.Name, productModel.Price);
        }

        public async Task UpdateAsync(Product entity, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FindAsync(new object[] { entity.Id }, cancellationToken: cancellationToken);
            product.Name = entity.Name;
            product.Price = entity.Price;
            _context.Products.Update(product);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}