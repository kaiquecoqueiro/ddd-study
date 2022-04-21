using System;

namespace DDD.Study.Domain.Entitiy
{
    public class Customer
    {
        public Customer(Guid id, string name)
        {
            Id = id;
            IsActive = true;
            Name = name;
            Address = null;
            RewardPoints = 0;
            Validate();
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Address Address { get; private set; }
        public bool IsActive { get; private set; }
        public int RewardPoints { get; private set; }

        public void ChangeName(string newName)
        {
            Name = newName;
            Validate();
        }

        public void AddRewardPoints(int points)
        {
            if (points < 0)
                throw new ArgumentException("Reward points cannot be negative", nameof(points));

            if (!IsActive)
                throw new InvalidOperationException("Cannot add reward points to inactive customer");

            RewardPoints += points;
        }

        public void Activate()
        {
            if (Address is null)
                throw new InvalidOperationException("Address is required.");

            IsActive = true;
        }

        public void Deactivate() => IsActive = false;

        public void ChangeAddress(Address newAddress) => Address = newAddress;

        void Validate()
        {
            if (Id == Guid.Empty)
                throw new InvalidOperationException("Id is required.");

            if (string.IsNullOrWhiteSpace(Name))
                throw new InvalidOperationException("Name is required");
        }

    }
}