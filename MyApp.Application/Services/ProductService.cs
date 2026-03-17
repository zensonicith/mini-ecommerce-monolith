using MyApp.Application.DTOs;
using MyApp.Application.Exceptions;
using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;

namespace MyApp.Application.Services
{
    public class ProductService : IProductService
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
            return products.Select(p => (ProductResponseDto)p).ToList();
        }

        public async Task<ProductResponseDto> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new NotFoundException("Product", id);
            }

            return (ProductResponseDto)product;
        }

        public async Task<ProductResponseDto> AddProductAsync(ProductRequestDto request, CancellationToken ct = default)
        {
            var product = new Product
            {
                ProductName = request.ProductName,
                Description = request.Description,
                Unit = request.Unit,
                Price = request.Price,
                ImageUrl = request.ImageUrl
            };

            await _productRepository.AddAsync(product);

            return (ProductResponseDto)product;
        }

        public async Task<bool> UpdateProductAsync(int id, ProductRequestDto request, CancellationToken ct = default)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new NotFoundException("Product", id);
            }

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
            if (product == null)
            {
                throw new NotFoundException("Product", id);
            }

            await _productRepository.DeleteAsync(product);
            return true;
        }
    }
}
