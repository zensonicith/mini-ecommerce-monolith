using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;
using MyApp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Infrastructure.Repositories
{
    internal class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }
        public IQueryable<Order> GetAllOrders()
        {
            return _context.Orders;
        }

        public Task<Order?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(Order order)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Order order)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
