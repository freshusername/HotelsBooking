using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.DTOs;
using ApplicationCore.Infrastructure;
using ApplicationCore.Interfaces;
using AutoMapper;
using Infrastructure.EF;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Managers
{
    public class ConvsManager :  IConvsManager
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public ConvsManager(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<AdminRoomConvDTO> GetConvs()
        {
            
            throw new NotImplementedException();
        }

        public HotelRoom GetHotelRoom(AdminOrderDetailDTO orderDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationDetails> CreateConv(AdminRoomConvDTO convDTO)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteConv(int Id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<OperationDetails> EditConv(AdminRoomConvDTO convDTO)
        {
            throw new System.NotImplementedException();
        }

        public AdminRoomConvDTO GetConvById(int Id)
        {
            throw new System.NotImplementedException();
        }

        
    }
}
