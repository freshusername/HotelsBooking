using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.DTOs
{
    public class OrderItemDTO
    {

        public int OrderItemId { get; set; }
        public string OrderId { get; set; }

        public int Qty { get; set; }
               
        public string Name { get; set; }
        public string Description { get; set; }

        public int HotelRoomId { get; set; }

        public decimal Price { get; set; }

    }
}
