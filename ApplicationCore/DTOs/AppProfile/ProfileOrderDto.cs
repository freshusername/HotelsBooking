using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.DTOs.AppProfile
{
	public class ProfileOrderDto
	{
		public string Hotel { get; set; }
		public DateTimeOffset CheckInDate { get; set; }
		public DateTimeOffset CheckOutDate { get; set; }
		public decimal Total { get; set; }
	}
}
