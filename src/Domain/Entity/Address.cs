using System;

namespace DDD.Study.Domain.Entitiy
{
    public class Address
    {
        public Address(string street, int number, string zip, string city)
        {
            Street = street;
            Number = number;
            Zip = zip;
            City = city;
            Validate();
        }

        public string Street { get; private set; }
        public int Number { get; private set; }
        public string Zip { get; private set; }
        public string City { get; private set; }

        public override string ToString()
        {
            return $"{Street} {Number}, {Zip} - {City}";
        }

        void Validate()
        {
            if (string.IsNullOrEmpty(Street))
                throw new InvalidOperationException("Street is required");

            if (Number <= 0)
                throw new InvalidOperationException("Number is required");

            if (string.IsNullOrEmpty(Zip))
                throw new InvalidOperationException("Zip is required");

            if (string.IsNullOrEmpty(City))
                throw new InvalidOperationException("City is required");
        }
    }
}