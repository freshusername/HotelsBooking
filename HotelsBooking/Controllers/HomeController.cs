using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationCore.DTOs.AppProfile;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using AutoMapper;
using HotelsBooking.Models.AppProfile;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelsBooking.Controllers
{
    public class HomeController : Controller
    {
	    private readonly IMapper _mapper;
	    private readonly IProfileService _profileService;
	    private readonly IProfileManager _profileManager;
	    private readonly UserManager<AppUser> _userManager;

	    public HomeController(IMapper mapper, IProfileService profileService, IProfileManager profileManager, UserManager<AppUser> userManager)
	    {
		    _mapper = mapper;
		    _profileService = profileService;
		    _profileManager = profileManager;
		    _userManager = userManager;
	    }

	    public async Task<IActionResult> Detail()
	    {
		    var result = _userManager.GetUserId(User);
			
		    return RedirectToAction("Detail", "Profile" , new {Id = result});
	    }

		public IActionResult Index()
        {

            return View();
        }


    }
}