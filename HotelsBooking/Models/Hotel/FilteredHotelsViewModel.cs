using ApplicationCore.DTOs;
using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelsBooking.Models.Hotel
{
    public class FilteredHotelsViewModel
    {
        public IEnumerable<HotelDTO> Hotels { get; set; }
        public IEnumerable<HotelConvDTO> HotelConvs { get; set; }
        public IEnumerable<AdditionalConvDTO> RoomConvs { get; set; }
        public FilterHotelDto FilterHotelDto { get; set; }
    }
}
