using ApplicationCore.DTOs;
using ApplicationCore.Infrastructure;
using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IHotelManager : IDisposable
    {
        Task<HotelDTO> GetHotelById(int Id);
        IEnumerable<HotelDTO> GetHotels(HotelFilterDto filterHotelDto);
        Task<OperationDetails> Create(HotelDTO hotelDTO);
        Task<OperationDetails> Update(HotelDTO hotelDTO);
        Task Delete(int Id);
        IEnumerable<HotelConvDTO> GetHotelConvs();
        IEnumerable<AdditionalConvDTO> GetRoomConvs();
        Task<OperationDetails> CreateHotelConv(HotelConvDTO hotelConvDTO);
        Task DeleteHotelConv(int Id);
    }
}
