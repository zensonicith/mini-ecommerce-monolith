using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public ICollection<OrderProducts> OrderProducts { get; } = [];
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
