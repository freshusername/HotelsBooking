using ApplicationCore.DTOs;
using ApplicationCore.Infrastructure;
using ApplicationCore.Interfaces;
using AutoMapper;
using Infrastructure.EF;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Managers
{
    public class OrderManager : IOrderManager
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;
        private readonly DbSet<Order> _orders;
        private readonly DbSet<OrderDetail> _orderDetails;
        public OrderManager(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _orders = _context.Orders;
            _orderDetails = _context.OrderDetails;
            _mapper = mapper;
        }

        public List<Order> GetOrders()
        {
            return _orders.ToList();
        }

        public async Task<OperationDetails> CreateOrder(OrderDTO orderDTO)
        {
            Order orderCheck = _orders.FirstOrDefault(x => x.Id == orderDTO.Id);
            if (orderCheck == null)
            {
                Order order = _mapper.Map<OrderDTO, Order>(orderDTO);
                await _orders.AddAsync(order);
                await _context.SaveChangesAsync();
                return new OperationDetails(true, "Order added", "Id");
            }
            return new OperationDetails(false, "Order with the same id already exists", "Id");
        }

        public async Task DeleteOrder(int id)
        {
            Order order = _orders.Find(id);
            _orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            
        }

        public async Task EditOrder(OrderDTO orderDTO)
        {
            Order orderCheck = _orders.FirstOrDefault(x => x.Id == orderDTO.Id);
            _orders.Update(orderCheck);
            await _context.SaveChangesAsync();
        } 

        public List<OrderDetail> GetOrderDetails(int id)
        {
            return _orderDetails.Where(i => i.OrderId == id).ToList();
        }

        public async Task<OperationDetails> CreateOrderDetails(OrderDetailDTO orderDetailDTO)
        {
            OrderDetail orderCheck = _orderDetails.FirstOrDefault(x => x.Id == orderDetailDTO.Id);
            if (orderCheck == null)
            {
                OrderDetail order = _mapper.Map<OrderDetailDTO, OrderDetail>(orderDetailDTO);
                await _orderDetails.AddAsync(order);
                await _context.SaveChangesAsync();
                return new OperationDetails(true, "Order details added", "Id");
            }
            return new OperationDetails(false, "Order details with the same id already exists", "Id");
        }

        public async Task EditOrderDetails(OrderDetailDTO orderDetailDTO)
        {
            OrderDetail orderDetailCheck = _orderDetails.FirstOrDefault(x => x.Id == orderDetailDTO.Id);
            _orderDetails.Update(orderDetailCheck);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderDetails(int id)
        {
            OrderDetail order = _orderDetails.Find(id);
            _orderDetails.Remove(order);
            await _context.SaveChangesAsync();
        }

        public Order FindOrder(string FirstName, string LastName)
        {
            AppUser user = _context.Users.FirstOrDefault(x => x.FirstName == FirstName && x.LastName == LastName);
            if (user != null)
            {
                return _context.Orders.FirstOrDefault(x => x.AppUserId == user.Id);
            }
            else return null;
        }
    }
}
