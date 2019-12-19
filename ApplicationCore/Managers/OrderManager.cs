using ApplicationCore.DTOs;
using ApplicationCore.Infrastructure;
using ApplicationCore.Interfaces;
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
        private IMapper _mapper;
        public OrderManager(ApplicationDbContext context, IMapper mapper, UserManager<AppUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }
        #region Order

        public async Task<OrderDTO> GetOrderById(int Id) 
        {
            Order orderFind = await _context.Orders.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == Id);
            if (orderFind != null)
            {
                OrderDTO orderDTO = new OrderDTO
                {
                    Id = orderFind.Id,
                    IsActive = orderFind.IsActive,
                    FirstName = orderFind.User.FirstName,
                    LastName = orderFind.User.LastName
                };
                return orderDTO;
            }
            else
                return null;
        } 

        public List<OrderDTO> GetOrders()
        {
            List<OrderDTO> orderDTOs = new List<OrderDTO>();
            List<Order> res = _context.Orders.Include(p => p.User).ToList();
            foreach(var s in res)
            {
                orderDTOs.Add(new OrderDTO
                {
                    Id = s.Id,
                    IsActive = s.IsActive,
                    FirstName = s.User.FirstName,
                    LastName = s.User.LastName
                });
            }
            return orderDTOs;
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

        public async Task DeleteOrder(int id)
        {
            Order order = _context.Orders.Find(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region OrderDetails
        public async Task<OrderDetailDTO> GetOrderDetailById(int id)
        {
            OrderDetail orderFind = await _context.OrderDetails.Include(p => p.HotelRoom)
                                                .ThenInclude(p => p.Hotel)
                                                .ThenInclude(p => p.Name)
                                           .Include(p => p.HotelRoom)
                                                .ThenInclude(p => p.Room)
                                                .ThenInclude(p => p.Id)
                                           .FirstOrDefaultAsync(c => c.OrderId == id);
            if (orderFind != null)
            {
                OrderDetailDTO orderDetailDTO = new OrderDetailDTO
                {
                    Id = orderFind.Id,
                    CheckInDate = orderFind.CheckInDate,
                    CheckOutDate = orderFind.CheckOutDate,
                    HotelName = orderFind.HotelRoom.Hotel.Name,
                    RoomId = orderFind.HotelRoom.Room.Id,
                    TotalPrice = orderFind.TotalPrice
                };
                return orderDetailDTO;
            }
            else
                return null;
        }
        public List<OrderDetailDTO> GetOrderDetails(int id)
        {
            List<OrderDetailDTO> orderDTOs = new List<OrderDetailDTO>();
            var res = _context.OrderDetails.Where(c => c.OrderId == id)
                                            .Include(p => p.HotelRoom)
                                                .ThenInclude(p => p.Hotel)
                                                .ThenInclude(p => p.Name)
                                           .Include(p => p.HotelRoom)
                                                .ThenInclude(p => p.Room)
                                                .ThenInclude(p => p.Id)
                                           .ToList();
            foreach (var s in res)
            {
                orderDTOs.Add(new OrderDetailDTO
                {
                    Id = s.Id,
                    CheckInDate = s.CheckInDate,
                    CheckOutDate = s.CheckOutDate,
                    TotalPrice = s.TotalPrice,
                    HotelName = s.HotelRoom.Hotel.Name,
                    RoomId = s.HotelRoom.Room.Id
                });
            }
            return orderDTOs;
        }

        

        public async Task<OperationDetails> CreateOrderDetails(OrderDetailDTO orderDetailDTO)
        {
            OrderDetail orderCheck = _context.OrderDetails.FirstOrDefault(x => x.Id == orderDetailDTO.Id);
            if (orderCheck == null)
            {
                OrderDetail order = _mapper.Map<OrderDetailDTO, OrderDetail>(orderDetailDTO);
                await _context.OrderDetails.AddAsync(order);
                await _context.SaveChangesAsync();
                return new OperationDetails(true, "Order details are added", "Id");
            }
            return new OperationDetails(false, "Order details with the same ID already exists", "Id");
        }

        public async Task<OperationDetails> EditOrderDetails(OrderDetailDTO orderDetailDTO)
        {
            OrderDetail orderDetailCheck = _context.OrderDetails.FirstOrDefault(x => x.Id == orderDetailDTO.Id);
            _context.OrderDetails.Update(orderDetailCheck);
            await _context.SaveChangesAsync();
            return new OperationDetails(true, "Order details are updated", "ID");
        }

        public async Task DeleteOrderDetails(int id)
        {
            OrderDetail order = _context.OrderDetails.Find(id);
            _context.OrderDetails.Remove(order);
            await _context.SaveChangesAsync();
        }
        

        public bool IsHotelExists(string HotelName)
        {
            Hotel hotel = _context.Hotels.FirstOrDefault(p => p.Name == HotelName);
            if (hotel == null)
                return false;
            else
                return true;
        }

        public bool IsRoomExists(int RoomID)
        {
            Room room = _context.Rooms.FirstOrDefault(p => p.Id == RoomID);
            if (room == null)
                return false;
            else
                return true;
        }
        #endregion

        public void Dispose()
        {

        }
    }
}
