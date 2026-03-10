using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.DTOs
{
    internal class ProductDto
    {
        public string? ProductName { get; set; }
        public int Unit { get; set; }
        public decimal Price { get; set; }
    }
}
