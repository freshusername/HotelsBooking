using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.DTOs.AppProfile;
using ApplicationCore.Infrastructure;
using AutoMapper;
using Infrastructure.EF;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Services
{
	public class ProfileService : IProfileService
	{
		private ApplicationDbContext _context;
		private UserManager<AppUser> _userManager;
		private RoleManager<IdentityRole> _roleManager;
		private SignInManager<AppUser> _signInManager;
		private IMapper _mapper;


		public ProfileService(
		  ApplicationDbContext context,
		  IMapper mapper,
		  UserManager<AppUser> userManager,
		  RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
		{
			_context = context;
			_mapper = mapper;
			_userManager = userManager;
			_roleManager = roleManager;
			_signInManager = signInManager;
		}

		public async Task<IEnumerable<ProfileDto>> GetAllProfilesAsync()
		{
			var users = _context.Users.ToList();
			var userRoles = _context.UserRoles.ToList();
			IEnumerable<ProfileDto> result = new List<ProfileDto>();
			var users_with_roles = userRoles.GroupBy(ur => ur.UserId)
				.Select(g => new ProfileDto()
				{
					Id = users.FirstOrDefault(u => u.Id == g.Key)?.Id,
					FirstName = users.FirstOrDefault(u => u.Id == g.Key)?.FirstName,
					LastName = users.FirstOrDefault(u => u.Id == g.Key)?.LastName,
					Email = users.FirstOrDefault(u => u.Id == g.Key)?.Email,
					Roles = g.Select(role => _roleManager.Roles.FirstOrDefault(r => r.Id == role.RoleId)?.Name).ToList(),
					ProfileImage = users.FirstOrDefault(u => u.Id == g.Key)?.ProfileImage
				});

			return users_with_roles;
		}

		public async Task<ProfileDto> GetByIdAsync(string id)
		{
			var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
			if (user == null)
				return null;

			var profile = _mapper.Map<AppUser, ProfileDto>(user);
			profile.Roles = await GetRoles(profile.Id);
			return profile;
		}

		public async Task<ProfileDto> GetByEmailAsync(string email)
		{
			var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == email);
			if (user == null)
				return null;

			var profile = _mapper.Map<AppUser, ProfileDto>(user);
			profile.Roles = await GetRoles(profile.Id);
			return profile;
		}

		public async Task<List<string>> GetRoles(string id)
		{
			var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
			if (user == null)
				return null;
			var roles = _context.UserRoles.Where(x => x.UserId == id).ToList();


			List<IdentityRole> results = new List<IdentityRole>();

			foreach (var role in roles)
			{
				results.Add(await _roleManager.FindByIdAsync(role.RoleId));
			}

			List<string> roleNames = new List<string>();

			foreach (var result in results)
			{
				roleNames.Add(await _roleManager.GetRoleNameAsync(result));
			}

			return roleNames;
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

		public async Task<IEnumerable<Order>> GetUserOrdersByUserId(string id)
		{
			return _context.Orders.Where(o => o.AppUserId == id).ToList();
		}
	}
}