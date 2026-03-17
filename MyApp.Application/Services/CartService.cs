using MyApp.Application.DTOs;
using MyApp.Application.Exceptions;
using MyApp.Application.Interfaces;
using MyApp.Application.Interfaces.Repo;
using MyApp.Domain.Entities;

namespace MyApp.Application.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;

    public CartService(
        ICartRepository cartRepository,
        ICustomerRepository customerRepository,
        IProductRepository productRepository)
    {
        _cartRepository = cartRepository;
        _customerRepository = customerRepository;
        _productRepository = productRepository;
    }

    public async Task<List<CartResponseDto>> GetAllCartsByUserIdAsync(int userId)
    {
        var carts = await _cartRepository.GetAllCartsByUserIdAsync(userId);
        return carts.Select(cart => (CartResponseDto)cart).ToList();
    }

    public async Task<CartResponseDto> AddCartAsync(int userId, CartRequestDto request)
    {
        var customer = await _customerRepository.GetByIdAsync(userId);
        if (customer == null)
        {
            throw new NotFoundException("Customer", userId);
        }

        var product = await _productRepository.GetByIdAsync(request.ProductId);
        if (product == null)
        {
            throw new NotFoundException("Product", request.ProductId);
        }

        var cart = new Cart
        {
            CustomerId = userId,
            ProductId = request.ProductId,
        };

        await _cartRepository.AddCartsAsync(cart);
        return (CartResponseDto)cart;
    }

    public async Task<bool> UpdateCartAsync(int id, int userId, CartRequestDto request)
    {
        var cart = await _cartRepository.GetByIdAsync(id);
        if (cart == null)
        {
            return false;
        }

        if (cart.CustomerId != userId)
        {
            throw new ForbiddenException();
        }

        var customer = await _customerRepository.GetByIdAsync(userId);
        if (customer == null)
        {
            throw new NotFoundException("Customer", userId);
        }

        var product = await _productRepository.GetByIdAsync(request.ProductId);
        if (product == null)
        {
            throw new NotFoundException("Product", request.ProductId);
        }

        cart.CustomerId = userId;
        cart.ProductId = request.ProductId;
        cart.Customer = customer;
        cart.Product = product;

        await _cartRepository.UpdateCartAsync(cart);
        return true;
    }

    public async Task<bool> RemoveCartAsync(int id, int userId)
    {
        var cart = await _cartRepository.GetByIdAsync(id);
        if (cart == null)
        {
            return false;
        }

        if (cart.CustomerId != userId)
        {
            throw new ForbiddenException();
        }

        await _cartRepository.RemoveCartAsync(cart);
        return true;
    }
}
