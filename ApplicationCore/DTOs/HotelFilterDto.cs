using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.DTOs
{
    public class HotelFilterDto
    {
        public string KeyWord { get; set; }
        [BindProperty]
        public List<int> HotelConvs { get; set; } = new List<int>();
        [BindProperty]
        public List<int> RoomConvs { get; set; } = new List<int>();
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }


    }
}
