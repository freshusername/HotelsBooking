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
        Task<OrderDTO> GetOrderById(int Id);
        List<OrderDTO> GetOrders();
        Task<OperationDetails> CreateOrder(OrderDTO orderDTO);
        Task<OperationDetails> EditOrder(OrderDTO orderDTO);
        Task DeleteOrder(int id);

        Task<OrderDetailDTO> GetOrderDetailById(int Id);
        List<OrderDetailDTO> GetOrderDetails(int Id);
        Task<OperationDetails> CreateOrderDetails(OrderDetailDTO orderDTO);
        Task<OperationDetails> EditOrderDetails(OrderDetailDTO orderDTO);
        bool IsHotelExists(string HotelName);
        bool IsRoomExists(int RoomID);
        Task DeleteOrderDetails(int id);
        Task<OperationDetails> CreateOrderAsync(OrderDTO orderDTO);
    }
}
