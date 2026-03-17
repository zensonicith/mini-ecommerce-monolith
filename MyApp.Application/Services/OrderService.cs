using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;
using MyApp.Application.DTOs;
using MyApp.Application.Exceptions;

namespace MyApp.Application.Services
{
    internal class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IQueryable<Order> GetAllOrders()
        {
            return _orderRepository.GetAllOrders();
        }

        public async Task<OrderResponseDto> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id)
                        ?? throw new NotFoundException(nameof(Order), id);

            return (OrderResponseDto)order;
        }
    }
}