using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string? City { get; set; }
        public ICollection<Order> Orders { get; } = [];
        
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
