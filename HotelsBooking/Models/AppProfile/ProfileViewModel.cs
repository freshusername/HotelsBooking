﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Entities;
using ApplicationCore.DTOs.AppProfile;

namespace HotelsBooking.Models.AppProfile
{
	public class ProfileViewModel
	{
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public byte[] ProfileImage { get; set; }
		public string Email { get; set; }
		public string Location { get; set; }

		public List<string> Roles { get; set; }

		public ICollection<ProfileOrderDto> Orders { get; set; }
	}
}
