using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.DTOs.AppProfile
{
	public class ProfileOrderDto
	{
		public byte[] HotelImage { get; set; }
		public string HotelName { get; set; }
		public DateTimeOffset CheckInDate { get; set; }
		public DateTimeOffset CheckOutDate { get; set; }
		public decimal Total { get; set; }
	}
}
