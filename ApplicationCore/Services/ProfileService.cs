using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.DTOs;
using ApplicationCore.DTOs.AppProfile;
using ApplicationCore.Infrastructure;
using AutoMapper;
using Infrastructure.EF;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ApplicationCore.Services
{
  public class ProfileService : IProfileService
  {
    private ApplicationDbContext _context;
    private UserManager<AppUser> _userManager;
    private RoleManager<IdentityRole> _roleManager;
    private IMapper _mapper;

    public ProfileService(
      ApplicationDbContext context, 
      IMapper mapper, 
      UserManager<AppUser> userManager,
      RoleManager<IdentityRole> roleManager)
    {
      _context = context;
      _mapper = mapper;
      _userManager = userManager;
      _roleManager = roleManager;
    }
    
    public async Task<ProfileDto> GetByIdAsync(string id)
    {
      var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
      if (user == null)
        return null;

      var profile = _mapper.Map<AppUser, ProfileDto>(user);
      return profile;
    }

    public async Task<ProfileDto> GetByEmailAsync(string email)
    {
      var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == email);
      if (user == null)
        return null;

      var profile = _mapper.Map<AppUser, ProfileDto>(user);
      //profile.Role = await GetRole(profile.Id);
      return profile;
    }

    public async Task<string> GetRole(string id)
    {
      var user = _context.Users.SingleOrDefaultAsync(x => x.Id == id);
      if (user == null)
        return null;
      var roleId = _context.UserRoles.FirstOrDefault(r => r.UserId == id)?.RoleId;

      var role = await _roleManager.FindByIdAsync(roleId);

      return role.ToString();
    }
    
    public async Task<OperationDetails> UpdateProfile(ProfileDto model)
    {
      var user = await _userManager.FindByEmailAsync(model.Email);

      if (user == null)
      {
        return new OperationDetails(false, "Something gone wrong", "Email");
      }

      user.FirstName = model.FirstName;
      user.LastName = model.LastName;
      user.Email = model.Email;

      await _context.SaveChangesAsync();

      return new OperationDetails(true, "Your profile has been successfully updated", "Email");
    }

    public async Task<IEnumerable<ProfileDto>> GetAllProfilesAsync()
    {
      var users = _context.Users.ToList();
      var userRoles = _context.UserRoles.ToList();

      // List<ProfileDto> users_with_roles = new List<ProfileDto>();



      //users.Select(u => new ProfileDto
      //{
      //  FirstName = u.FirstName,
      //  LastName = u.LastName,
      //  Email = u.Email,
      //  Roles = _roleManager.Roles.Where(r => userRoles.Where(ur => ur.UserId == u.Id).ToList())  // FindByIdAsync(userRole.RoleId).Result.Name
      //});
      IEnumerable<ProfileDto> result = new List<ProfileDto>();
      var users_with_roles = userRoles.GroupBy(ur => ur.UserId)
                              .Select(g => new ProfileDto()
                              {
                                FirstName = users.FirstOrDefault(u => u.Id == g.Key).FirstName,
                                LastName = users.FirstOrDefault(u => u.Id == g.Key).LastName,
                                Email = users.FirstOrDefault(u => u.Id == g.Key).Email,
                                Roles = g.Select(p => _roleManager.Roles.FirstOrDefault(r => r.Id == p.RoleId).Name).ToList()
                              });



      //foreach (var user_with_role in users_with_roles)
      //{
      //  ProfileDto profiledto = new ProfileDto();
      //  profiledto.FirstName = users.FirstOrDefault(u => u.Id == user_with_role.Key).FirstName;
      //  profiledto.LastName = users.FirstOrDefault(u => u.Id == user_with_role.Key).LastName;
      //  profiledto.Email = users.FirstOrDefault(u => u.Id == user_with_role.Key).Email;
         
      //  foreach (var role in user_with_role)
      //  {

      //    profiledto.Roles.Add(_roleManager.Roles.); = _roleManager.Roles.Where(r => r.Id == role.RoleId);
      //  }
        
      //}

      /*foreach (var userRole in userRoles)
      {
        users_with_roles.Add(new ProfileDto()
                            {
                              FirstName = users.FirstOrDefault(u => u.Id == userRole.UserId).FirstName,
                              LastName = users.FirstOrDefault(u => u.Id == userRole.UserId).LastName,
                              Email = users.FirstOrDefault(u => u.Id == userRole.UserId).Email,
                              Roles = _roleManager.Roles.Where(r => r.Id)  // FindByIdAsync(userRole.RoleId).Result.Name
                            });
      }*/
      //var profiles = _mapper.Map<IEnumerable<AppUser>, IEnumerable<ProfileDto>>(users);
      /*IEnumerable<ProfileDto> users_with_roles = users.Select(u => new ProfileDto()
                              {
                                FirstName = u.FirstName,
                                LastName = u.LastName,
                                Email = u.Email,
                                Roles = _userManager.GetRolesAsync(u).Result
                              });*/
      //var profiles = _mapper.Map<IEnumerable<>, IEnumerable<ProfileDto>>(users_with_roles);

      return users_with_roles; 
    }
  }
}
