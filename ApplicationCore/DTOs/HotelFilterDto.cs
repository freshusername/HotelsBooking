using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.DTOs
{
    public class HotelFilterDto
    {
        public string KeyWord { get; set; }

        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }

        public PagingDto PagingDto { get; set; }
    }
}
