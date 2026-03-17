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
    }
}