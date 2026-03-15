using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace MyApp.Application.DTOs
{
    public class UpdateProductRequestDto
    {
        public string ProductName { get; set; }
        public string? Description { get; set; }
        public int Unit { get; set; }
        public decimal Price { get; set; }
        public string? NewImageUrl { get; set; }
    }
}
