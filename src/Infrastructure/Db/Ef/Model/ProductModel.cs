using System;

namespace src.Infrastructure.Db.Ef.Model
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }
}