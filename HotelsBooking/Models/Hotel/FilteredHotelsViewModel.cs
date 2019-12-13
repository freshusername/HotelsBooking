﻿using ApplicationCore.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelsBooking.Models.Hotel
{
    public class FilteredHotelsViewModel
    {

        public IEnumerable<HotelDTO> Hotels { get; set; }
        public HotelFilterDto HotelFilterDto { get; set; }
        public PagingDto PagingDto { get; set; }
    }
}
