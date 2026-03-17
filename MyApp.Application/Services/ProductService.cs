using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;

namespace MyApp.Application.Services
{
    internal class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileStorageService _fileStorage;

        public ProductService(IProductRepository productRepository, IFileStorageService fileStorage)
        {
            _productRepository = productRepository;
            _fileStorage = fileStorage;
        }

        public async Task<List<ProductResponseDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            var response = products.Select(p => (ProductResponseDto)p).ToList();
            return response;
        }

        public async Task<ProductResponseDto> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            var response = (ProductResponseDto)product!;
            return response;
        }

        public async Task<ProductResponseDto> AddProductAsync(ProductRequestDto request,
            CancellationToken ct = default)
        {
            var product = new Product
            {
                ProductName = request.ProductName,
                Description = request.Description,
                Unit = request.Unit,
                Price = request.Price,
                ImageUrl = request.ImageUrl ?? null
            };

            await _productRepository.AddAsync(product);

            var response = (ProductResponseDto)product;
            return response;
        }

        public async Task<bool> UpdateProductAsync(int id, ProductRequestDto request,
            CancellationToken ct = default)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null) return false;

            product.ProductName = request.ProductName;
            product.Description = request.Description;
            product.Unit = request.Unit;
            product.Price = request.Price;

            if (request.ImageUrl != null)
            {
                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    await _fileStorage.DeleteImageAsync(product.ImageUrl, ct);
                }

                product.ImageUrl = request.ImageUrl;
            }

            await _productRepository.UpdateAsync(product);
            return true;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null) return false;

            await _productRepository.DeleteAsync(product);
            return true;
        }
    }
}