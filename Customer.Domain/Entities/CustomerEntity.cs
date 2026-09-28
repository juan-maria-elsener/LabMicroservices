using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Domain.Entities
{
    public class CustomerEntity
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Address { get; private set; } = string.Empty;
        public DateTime RegistrationDate { get; private set; }

        protected CustomerEntity() { }

        public CustomerEntity(string name, string email, string address)
        {
            Name = name;
            Email = email;
            Address = address;
            RegistrationDate = DateTime.UtcNow; 
        }

        public void UpdateDetails(string name, string email, string address)
        {
            Name = name;
            Email = email;
            Address = address;
        }

    }
}
