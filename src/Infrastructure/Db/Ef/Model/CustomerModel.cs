using System;

namespace src.Infrastructure.Db.Ef.Model
{
    public class CustomerModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public bool IsActive { get; set; }
        public int RewardPoints { get; set; }
    }
}