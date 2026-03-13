using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.DTOs
{
    public class ProductRequestDto
    {
        public string ProductName { get; set; }
        public string? Description { get; set; }
        public int Unit { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}
