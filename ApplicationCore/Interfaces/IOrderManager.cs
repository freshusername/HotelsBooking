using ApplicationCore.DTOs;
using ApplicationCore.Infrastructure;
using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IOrderManager : IDisposable
    {
        Order FindOrder(string FirstName, string LastName);
        List<Order> GetOrders();
        Task<OperationDetails> CreateOrder(OrderDTO orderDTO);
        Task EditOrder(OrderDTO orderDTO);
        Task DeleteOrder(int id);

        List<OrderDetail> GetOrderDetails(int id);
        Task<OperationDetails> CreateOrderDetails(OrderDetailDTO orderDTO);
        Task EditOrderDetails(OrderDetailDTO orderDTO);
        Task DeleteOrderDetails(int id);
    }
}
