using Microsoft.AspNetCore.Mvc;
using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;

namespace MyApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var allProduct = await _productService.GetAllProductsAsync();
            return Ok(allProduct);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById([FromRoute] int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound($"Product {id} is not found!");
            }

            return Ok(product);
        }

        [HttpPost]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductRequestDto request)
        {
            var result = await _productService.AddProductAsync(request);
            return CreatedAtAction(nameof(GetProductById), new { id = result.Id}, result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromForm] UpdateProductRequestDto request)
        {
            var success = await _productService.UpdateProductAsync(id, request);

            if (!success)
                return BadRequest();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            var success = await _productService.DeleteProductAsync(id);

            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
