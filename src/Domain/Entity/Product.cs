using System;

namespace DDD.Study.Domain.Entitiy
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public int Price { get; private set; }

        public Product(Guid id, string name, int price)
        {
            Name = name;
            Price = price;
            Id = id;
            Validate();
        }

        void Validate()
        {
            if (Id == Guid.Empty)
                throw new InvalidOperationException("Id is required.");

            if (string.IsNullOrWhiteSpace(Name))
                throw new InvalidOperationException("Name is required.");

            if (Price <= 0)
                throw new InvalidOperationException("Price must be greater than zero.");
        }

        public void ChangeName(string newName)
        {
            Name = newName;
            Validate();
        }

        public void ChangePrice(int newPrice)
        {
            Price = newPrice;
            Validate();
        }
    }
}