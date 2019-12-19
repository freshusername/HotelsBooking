using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Entities;

namespace ApplicationCore.DTOs
{
    public class OrderDetailDTO
    {
        public int Id { get; set; }
        public DateTimeOffset CheckInDate { get; set; }
        public DateTimeOffset CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string HotelName { get; set; }
        public int RoomId { get; set; }
    }
}
