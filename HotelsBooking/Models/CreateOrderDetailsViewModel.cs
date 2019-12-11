using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelsBooking.Models
{
    public class CreateOrderDetailsViewModel
    {
        public int Id { get; set; }
        public DateTimeOffset CheckInDate { get; set; }
        public DateTimeOffset CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }

        public string HotelName { get; set; }
        public int RoomId { get; set; }
    }
}
