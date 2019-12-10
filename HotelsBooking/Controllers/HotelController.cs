using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using HotelsBooking.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HotelsBooking.Controllers
{
    [Route("[controller]/[action]")]
    public class HotelController : Controller
    {
        private readonly IHotelManager _hotelService;
        public HotelController(IHotelManager hotelService)
        {
            this._hotelService = hotelService;
        }

        [HttpGet]
        public IActionResult ShowHotels()
        {
            var hotels = _hotelService.GetHotels();
            return View(hotels);
        }
        public IActionResult template()
        {
            var hotels = _hotelService.GetHotels();

            return View("ShowHotels2", hotels);
        }

        [HttpPost]
        public IActionResult AddHotel(HotelDto hotel)
        {
            _hotelService.Insert(hotel);
            return RedirectToAction("ShowHotels", "Hotel");
        }

        public IActionResult AddHotel()
        {
            return View();
        }





        public IActionResult HotelMain(int hotelId)
        {
            var hotel = _hotelService.Get(hotelId);
            return View(hotel);
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
