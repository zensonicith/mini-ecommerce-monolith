using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.DTOs
{
    public class ProductDto
    {
        public string? ProductName { get; set; }
        public int Unit { get; set; }
        public decimal Price { get; set; }

        public static explicit operator ProductDto(Product product)
        {
            if (product is null) return null;

            return new ProductDto
            {
                ProductName = product.ProductName,
                Unit = product.Unit,
                Price = product.Price
            };
        }
    }
}
