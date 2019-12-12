using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HotelsBooking.Models
{
    public class CreateOrEditOrderDetailsViewModel
    {
        public int Id { get; set; }
        [Required]
        public DateTimeOffset CheckInDate { get; set; }
        [Required]
        public DateTimeOffset CheckOutDate { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
        [Required]
        public string HotelName { get; set; }
        [Required]
        public int RoomId { get; set; }
    }
}
