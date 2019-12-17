﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.AppProfile.DTOs;
using ApplicationCore.DTOs.AppProfile;
using ApplicationCore.Services;
using AutoMapper;
using HotelsBooking.Models.AppProfile;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Interfaces;

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
			var result = _mapper.Map<ProfileDto, ProfileViewModel>(profile);
			return View(result);
		}

		public AllProfilesViewModel BuildProfileViewModel(IEnumerable<ProfileDto> users)
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

			var userOrders = _profileService.GetUserOrdersByUserId();

			var orderDetails = _orderManager.GetOrderDetails()

			var model = new AllProfilesViewModel
			{
				ProfilesList = profiles
			};

			return model;
		}

		public async Task<IActionResult> Index()
		{
			var users = await _profileService.GetAllProfilesAsync();

			var model = BuildProfileViewModel(users);

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
