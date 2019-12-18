using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Entities;

namespace ApplicationCore.DTOs
{
    public class OrderDTO
    {
	    public string UserId { get; set; }
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }


        public ICollection<OrderDetailDTO> OrderDetails { get; set; }
		public decimal Total { get; set; } = 0;
		public byte[] HotelImage { get; set; }
	}
}
