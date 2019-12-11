using ApplicationCore.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IHotelManager
    {
        HotelDto Get(int id);
        IEnumerable<HotelDto> GetHotels();
        
        void Insert(HotelDto item);
        void Update(HotelDto item);
        void Delete(int id);

    }
}
