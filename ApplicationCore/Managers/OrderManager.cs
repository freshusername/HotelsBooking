using ApplicationCore.Infrastructure;
using ApplicationCore.Interfaces;
using AutoMapper;
using Infrastructure.EF;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Managers
{
    public class OrderManager : IOrderManager
    {

        private readonly ApplicationDbContext _context;

        private readonly IMapper _mapper;

        public UserManager<AppUser> UserManager { get; private set ; }



        public OrderManager(ApplicationDbContext context , UserManager<AppUser> userManager , IMapper mapper)
        {
            _context = context;
            UserManager = userManager;
            _mapper = mapper;
        }


        //public async Task<OperationDetails> Create()
        //{
                
        //}



    }
}
