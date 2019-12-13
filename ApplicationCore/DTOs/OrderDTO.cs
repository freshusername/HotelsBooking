using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string UserId { get; set; }

        public int OrderDetailId { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTimeOffset OrderDate { get; set; }

        public DateTimeOffset CheckInDate { get; set; }

        public DateTimeOffset CheckOutDate { get; set; }
    }
}
