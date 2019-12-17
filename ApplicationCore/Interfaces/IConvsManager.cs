using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.DTOs;
using ApplicationCore.Infrastructure;
using Infrastructure.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IConvsManager
    {
        List<AdminRoomConvDTO> GetConvs();
        HotelRoom GetHotelRoom(AdminOrderDetailDTO orderDTO);
        AdminRoomConvDTO GetConvById(int Id);
        Task<OperationDetails> CreateConv(AdminRoomConvDTO convDTO);
        Task<OperationDetails> EditConv(AdminRoomConvDTO convDTO);
        Task DeleteConv(int Id);
    }
}
