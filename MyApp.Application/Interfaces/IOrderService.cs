using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using MyApp.Application.DTOs;

namespace MyApp.Application.Interfaces
{
    public interface IOrderService
    {
        IQueryable<Order> GetAllOrders();
        Task<OrderResponseDto> GetOrderByIdAsync(int id);
        Task<OrderResponseDto> UpdateOrderAsync( int orderId, UpdateOrderRequestDto request);
        Task<Order> GetDomainOrderByIdAsync(int orderId);
        Task UpdateDomainOrderAsync(Order order);
    }
}