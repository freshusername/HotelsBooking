using ApplicationCore.DTOs;
using ApplicationCore.Infrastructure;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using AutoMapper;
using Infrastructure.EF;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Managers
{
    public class OrderManager : IOrderManager
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IOrderService _orderService ;
        
        private IMapper _mapper;
        public OrderManager(ApplicationDbContext context, IMapper mapper, IOrderService orderService)
        {
            _context = context;
            _orderService = orderService;
            _mapper = mapper;     
        }

        public async Task<OperationDetails> CreateOrderAsync(OrderDTO orderDTO)
        {
            var order = _mapper.Map<OrderDTO, Order>(orderDTO);
            order.OrderDate = DateTimeOffset.UtcNow;
            order.IsActive = true;

            if (order == null)
                return new OperationDetails(false, "Operation did not succeed!", "");
            await _context.Orders.AddAsync(order);

            var items = await _orderService.GetOrderItemAsync();
            var orderItems = _mapper.Map<IEnumerable<OrderItemDTO>, IAsyncEnumerable<OrderItem>>(items);

            orderDTO.TotalPrice = (await _orderService.GetOrderItemsCountAndTotalAmmountAsync()).TotalAmmount;

            var orderDetail = (orderItems.Select(e => new OrderDetail
            {
                HotelRoomId = e.HotelRoom.Id,
                OrderId = order.Id,
                Qty = e.Qty,
                TotalPrice = orderDTO.TotalPrice
            })).ToEnumerable();

            if (orderDetail == null)
                return new OperationDetails(false, "Operation did not succeed!", "");
            await _context.OrderDetails.AddRangeAsync(orderDetail);

            await _context.SaveChangesAsync();

            return new OperationDetails(true, "Operation Succeed!", "");

        }



    }
}
