using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Interfaces
{
    public interface IOrderService
    {
       IQueryable<Order> GetAllOrders();
    }
}
