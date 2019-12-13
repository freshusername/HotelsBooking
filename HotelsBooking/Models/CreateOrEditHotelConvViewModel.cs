﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelsBooking.Models
{
    public class CreateOrEditHotelConvViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0,100000,ErrorMessage ="Price need to be in range from 0 to 100000")]
        public decimal Price { get; set; }
        public int HotelId { get; set; }
    }
}
