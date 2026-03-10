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
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
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
