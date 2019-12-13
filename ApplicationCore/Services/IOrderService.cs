using ApplicationCore.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IOrderService
    {
        string Id { get; set; }

        Task<int> AddToOrderItemAsync(HotelRoomDTO room, int qty = 1);
        Task<int> RemoveFromOrderItemAsync(HotelRoomDTO room);
        Task<IEnumerable<OrderItemDTO>> GetOrderItemAsync();
        Task<(int ItemCount, decimal TotalAmmount)> GetOrderItemsCountAndTotalAmmountAsync();
    }
}
