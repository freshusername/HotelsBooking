using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IUserManager
    {
        AppUser FindByFirstLastName(string FirstName, string LastName);
    }
}
