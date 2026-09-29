using System;
using Customer.Domain.ValueObjects;

namespace Customer.Domain.Entities
{
    public class CustomerEntity
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;

        public AddressVO Address { get; private set; } = null!; 

        public DateTime RegistrationDate { get; private set; }

        protected CustomerEntity() { }

        public CustomerEntity(string name, string email, AddressVO address)
        {
            Name = name;
            Email = email;
            Address = address;
            RegistrationDate = DateTime.UtcNow;
        }

        public void UpdateDetails(string name, string email, AddressVO address)
        {
            Name = name;
            Email = email;
            Address = address;
        }
    }
}