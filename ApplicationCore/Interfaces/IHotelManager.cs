﻿using ApplicationCore.DTOs;
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
        IEnumerable<HotelDTO> GetHotels(FilterHotelDto filterHotelDto);
        Task<OperationDetails> Create(HotelDTO hotelDTO);
        Task<OperationDetails> Update(HotelDTO hotelDTO);
        Task Delete(int Id);

        IEnumerable<HotelConvDTO> GetHotelConvs();
        Task<OperationDetails> CreateHotelConv(HotelConvDTO hotelConvDTO);
        Task DeleteHotelConv(int Id);
        HotelConvDTO GetHotelConvById(int Id);
        Task<OperationDetails> UpdateHotelConv(HotelConvDTO hotelConvDTO);

        HotelRoomDTO GetHotelRoomById(int Id);
        IEnumerable<HotelRoomDTO> GetHotelRooms();
        Task<OperationDetails> CreateHotelRoom(HotelRoomDTO hotelRoomDTO);
        Task<OperationDetails> UpdateHotelRoom(HotelRoomDTO hotelRoomDTO);
        Task DeleteHotelRoom(int Id);

        IEnumerable<HotelRoomConvDTO> GetHotelRoomConvs(int Id);
        Task<OperationDetails> CreateHotelRoomConv(HotelRoomConvDTO conv);
        Task DeleteHotelRoomConv(int Id);
    }
}
