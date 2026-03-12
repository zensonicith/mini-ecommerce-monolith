using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.DTOs
{
    public class ProductResponseDto
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public int Unit { get; set; }
        public decimal Price { get; set; }

        public static explicit operator ProductResponseDto(Product product)
        {
            if (product is null) return null;

            return new ProductResponseDto
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Description = product.Description,
                Unit = product.Unit,
                Price = product.Price
            };
        }
    }
}
