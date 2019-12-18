using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.AppProfile.DTOs;
using ApplicationCore.DTOs.AppProfile;
using ApplicationCore.Services;
using AutoMapper;
using HotelsBooking.Models.AppProfile;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Interfaces;
using Infrastructure.Entities;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HotelsBooking.Controllers
{
	public class ProfileController : Controller
	{
		private readonly IMapper _mapper;
		private readonly IProfileService _profileService;
		private readonly IProfileManager _profileManager;
		private readonly IOrderManager _orderManager;

		public ProfileController(IMapper mapper, IProfileService profileService, IProfileManager profileManager, IOrderManager orderManager)
		{
			_mapper = mapper;
			_profileService = profileService;
			_profileManager = profileManager;
			_orderManager = orderManager;
		}

		public async Task<IActionResult> Detail(string id)
		{
			var profile = await _profileService.GetByIdAsync(id);
			var result = BuildProfileViewModel(profile);
			return View(result);
		}

		public ProfileViewModel BuildProfileViewModel(ProfileDto user)
		{
			var orders = _profileService.GetUserOrdersByUserId(user.Id).ToList();
			foreach (var o in orders)
			{
				o.OrderDetails = _orderManager.GetOrderDetails(o.Id);
			}

			foreach (var order in orders)
			{
				foreach (var orderDetail in order.OrderDetails)
				{
					order.Total += orderDetail.TotalPrice;
					order.HotelImage = orderDetail.HotelImage;
				}
			}

			var result = _mapper.Map<ProfileDto, ProfileViewModel>(user);

			result.Orders = orders.Select(o => new ProfileOrderDto
			{

				HotelName = o.OrderDetails.FirstOrDefault().HotelName,
				CheckInDate = o.OrderDetails.FirstOrDefault().CheckInDate,
				CheckOutDate = o.OrderDetails.FirstOrDefault().CheckOutDate,
				Total = o.Total,
				HotelImage = o.HotelImage
			});
			
			return result;
		}


		//Here we build it WITHOUT orders info
		public AllProfilesViewModel BuildAllProfilesViewModel(IEnumerable<ProfileDto> users)
		{
			var profiles = users.Select(pr => new ProfileViewModel
			{
				Id = pr.Id,
				Roles = pr.Roles,
				Email = pr.Email,
				FirstName = pr.FirstName,
				LastName = pr.LastName,
				ProfileImage =  pr.ProfileImage
			});
			
			var model = new AllProfilesViewModel
			{
				ProfilesList = profiles
			};

			return model;
		}

		public async Task<IActionResult> Index()
		{
			var users = await _profileService.GetAllProfilesAsync();

			var model = BuildAllProfilesViewModel(users);

			return View(model);
		}
		
		public async Task<IActionResult> Edit(string id)
		{
			var profile = await _profileService.GetByIdAsync(id);

			return View(new ProfileUpdateViewModel
			{
				FirstName = profile.FirstName,
				LastName = profile.LastName,
				Email = profile.Email
			});
		}

		[HttpPost]
		public async Task<IActionResult> UpdateProfile(ProfileUpdateDTO model)
		{
			var profile = await _profileService.GetByEmailAsync(model.Email);

			await _profileManager.UpdateProfileInfoAsync(model);
			await _profileService.UpdateProfile(profile);
			return RedirectToAction("Detail", "Profile", new { id = profile.Id });
		}


	}
}
