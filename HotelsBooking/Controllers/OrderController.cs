using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using HotelsBooking.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelsBooking.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderManager _orderManager;

        public OrderController(IOrderService orderService,IOrderManager orderManager)
        {
            _orderManager = orderManager;
            _orderService = orderService;
        }

        public IActionResult Checkout()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Checkout(OrderViewModel model)
        {
            return View();
        }


    }
}
