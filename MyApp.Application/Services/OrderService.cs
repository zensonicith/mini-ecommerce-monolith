using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;
using MyApp.Application.DTOs;
using MyApp.Application.Exceptions;
using MyApp.Domain.Enum;

namespace MyApp.Application.Services
{
    internal class OrderService : IOrderService
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
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

        public async Task<OrderResponseDto> UpdateOrderAsync( int orderId, UpdateOrderRequestDto request)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order == null)
                throw new NotFoundException(nameof(order), orderId);

            // ===== business rule =====
            if (order.Status != EOrderStatus.Pending)
                throw new ConflictException("Only pending order can be updated");

            // ===== update snapshot =====
            order.ShippingName = request.ShippingName;
            order.ShippingPhone = request.ShippingPhone;
            order.ShippingAddressLine = request.ShippingAddressLine;
            order.ShippingCity = request.ShippingCity;
            order.ShippingCountry = request.ShippingCountry;
            order.ShippingPostalCode = request.ShippingPostalCode;

            // ===== update items =====
            order.OrderProducts.Clear();

            decimal total = 0;

            foreach (var item in request.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);

                if (product == null)
                    throw new Exception($"Product {item.ProductId} not found");

                var detail = new OrderDetails
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                };

                total += item.Quantity * product.Price;

                order.OrderProducts.Add(detail);
            }

            order.TotalAmount = total;

            await _orderRepository.UpdateAsync(order);

            return (OrderResponseDto)order;
        }
        
        public async Task<Order> GetDomainOrderByIdAsync(int id)
        {
            return await _orderRepository.GetByIdAsync(id)
                   ?? throw new NotFoundException(nameof(Order), id);
        }

        public async Task UpdateDomainOrderAsync(Order order)
        {
            await _orderRepository.UpdateAsync(order);
        }
    }
}