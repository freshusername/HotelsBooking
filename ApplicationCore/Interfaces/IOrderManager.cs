using ApplicationCore.DTOs;
using ApplicationCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IOrderManager
    {
        Task<OperationDetails> CreateOrderAsync(OrderDTO orderDTO);
    }
}
