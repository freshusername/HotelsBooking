using System;
using System.Collections.Generic;
using System.Text;
using ApplicationCore.Interfaces;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Managers
{
    class MyUserManager : IUserManager
    {
        private DbSet<AppUser> appUsers;
        public AppUser FindByFirstLastName(string FirstName, string LastName)
        {
            throw new NotImplementedException();
        }
    }
}
