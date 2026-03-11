using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;

namespace MyApp.Application.Services
{
    internal class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            var response = products.Select(p => (ProductDto)p).ToList();
            return response;
        }
        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            var response = (ProductDto)product;

            return response;
        }
        public async Task AddProductAsync(Product product)
        {
            await _productRepository.AddAsync(product);
        }
        public Task UpdateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }
        public Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
