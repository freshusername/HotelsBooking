using ApplicationCore.DTOs;
using AutoMapper;
using Infrastructure.EF;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class OrderService : IOrderService
    {

        private readonly ApplicationDbContext _context;

        private readonly IMapper _mapper;

        public string Id { get; set; }

        

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

        public async Task<int> AddToOrderItemAsync(HotelRoomDTO room, int qty = 1)
        {
            return await AddOrRemove(room, qty);

        }

        public async Task<int> RemoveFromOrderItemAsync(HotelRoomDTO room)
        {
            return await AddOrRemove(room, -1);
        }

        private async Task<int> AddOrRemove(HotelRoomDTO room, int qty)
        {


            var item  = _mapper.Map<HotelRoomDTO, HotelRoom>(room);

          var  orderItem =  await _context.OrderItems
                 .SingleOrDefaultAsync(s => s.HotelRoom.Id == item.Id && s.OrderId == Id);

            if (orderItem == null)
            {
                orderItem = new OrderItem
                {
                    OrderId = Id,
                    HotelRoom = item,
                    Qty = 0
                };

                await _context.OrderItems.AddAsync(orderItem);
            }

            orderItem.Qty += qty;

            if (orderItem.Qty <= 0)
            {
                orderItem.Qty = 0;
                _context.OrderItems.Remove(orderItem);
            }

            await _context.SaveChangesAsync();

            orderItem = null; // Reset

            return await Task.FromResult(orderItem.Qty);
        }

        public async Task<IEnumerable<OrderItemDTO>> GetOrderItemAsync()
        {
           
            var orderItems = _context.OrderItems.ToList() ?? await _context.OrderItems
                .Where(e => e.OrderId == Id)
                .Include(e => e.HotelRoom)
                .ToListAsync();


            return _mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemDTO>>(orderItems); 
        }

        public async Task<(int ItemCount, decimal TotalAmmount)> GetOrderItemsCountAndTotalAmmountAsync()
        {
            var subTotal = _context.OrderItems.ToList() ?
                .Select(c => c.HotelRoom.Price * c.Qty) ??
                await _context.OrderItems
                .Where(c => c.OrderId == Id)
                .Select(c => c.HotelRoom.Price * c.Qty)
                .ToListAsync();

            return (subTotal.Count(), subTotal.Sum());
        }

    }
}
