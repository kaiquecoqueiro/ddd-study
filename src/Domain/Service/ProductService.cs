using System.Collections.Generic;
using DDD.Study.Domain.Entitiy;

namespace src.Services
{
    public class ProductService
    {
        public static void IncreasePrice(IList<Product> products, int percentage)
        {
            foreach (var product in products)
                product.ChangePrice(product.Price * percentage / 100 + product.Price);
        }
    }
}