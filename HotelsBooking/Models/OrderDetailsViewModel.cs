using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Entities;

namespace HotelsBooking.Models
{
    public class OrderDetailsViewModel
    {
        public int Id { get; set; }
        public DateTimeOffset CheckInDate { get; set; }
        public DateTimeOffset CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string HotelName { get; set; }
        public int RoomId { get; set; }
    }
}
