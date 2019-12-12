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

        public int HotelRoomId { get; set; }
        public virtual HotelRoom HotelRoom { get; set; }

    }
}
