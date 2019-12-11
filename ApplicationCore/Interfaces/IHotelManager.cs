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
        Task<Hotel> GetHotelById(int Id);
        List<Hotel> GetHotels();
        Task<OperationDetails> Create(HotelDTO hotelDTO);
        Task<OperationDetails> Update(HotelDTO hotelDTO);
        Task Delete(int Id);
        IEnumerable<HotelConvDTO> GetHotelConvs();
        Task<OperationDetails> CreateHotelConv(HotelConvDTO hotelConvDTO);
    }
}
