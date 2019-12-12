using ApplicationCore.DTOs;
using AutoMapper;
using Infrastructure.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Services
{
    public class OrderService : IOrderService
    {

        private readonly ApplicationDbContext _context;

        private readonly IMapper _mapper;

        public string Id { get; set; }

        public IEnumerable<OrderItemDTO> OrderItemDTOs { get; set; }



        public OrderService(ApplicationDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        
        }

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
          
        }


        public static OrderService GetOrderItem(IServiceProvider services)
        {
            var httpContext = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext;
            var context = services.GetRequiredService<ApplicationDbContext>();

            var request = httpContext.Request;
            var response = httpContext.Response;

            var orderId = request.Cookies["OrderId-cookie"] ?? Guid.NewGuid().ToString();

            response.Cookies.Append("OrderId-cookie", orderId, new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddMonths(2)
            });

            return new OrderService(context)
            {
                Id = orderId
            };
        }



    }
}
