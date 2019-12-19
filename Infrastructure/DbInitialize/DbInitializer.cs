using Infrastructure.EF;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using static Infrastructure.Enums;

namespace Infrastructure.DbInitialize
{
    public static class DbInitializer
    {
        public static void SeedData(UserManager<AppUser> userManager , RoleManager<IdentityRole> roleManager , ApplicationDbContext db)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
            SeedHotels(db);       
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                 roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!roleManager.RoleExistsAsync("Owner").Result)
            {
                roleManager.CreateAsync(new IdentityRole("Owner"));
            }

            if (!roleManager.RoleExistsAsync("User").Result)
            {
                roleManager.CreateAsync(new IdentityRole("User"));
            }
        }

        public static void SeedUsers(UserManager<AppUser> userManager)
        {
            var admin = new AppUser
            {
                Email = "admin@admin.com",
                UserName = "admin@admin.com",
                FirstName = "Admin",
                LastName = "Admin",
                PhoneNumber = "+380930000000",
                EmailConfirmed = true
            };

            var owner = new AppUser
            {
                Email = "owner@owner.com",
                UserName = "owner@owner.com",
                FirstName = "Owner",
                LastName = "Owner",
                PhoneNumber = "+380930000000",
                EmailConfirmed = true
            };

            var user = new AppUser
            {
                Email = "user@user.com",
                UserName = "user@user.com",
                FirstName = "User",
                LastName = "User",
                PhoneNumber = "+380930000000",
                EmailConfirmed = true
            };

            if (userManager.FindByNameAsync(admin.Email).Result == null)
            {
                IdentityResult result;
                result = userManager.CreateAsync(admin, "admin12345").Result;
                
                if (result.Succeeded)              
                    userManager.AddToRoleAsync(admin, "Admin").Wait();             
            }

            if (userManager.FindByNameAsync(owner.Email).Result == null)
            {
                IdentityResult result;
                result = userManager.CreateAsync(owner, "owner12345").Result;

                if (result.Succeeded)
                    userManager.AddToRoleAsync(owner, "Owner").Wait();
            }

            if (userManager.FindByNameAsync(user.Email).Result == null)
            {
                IdentityResult result;
                result = userManager.CreateAsync(user, "user12345").Result;

                if (result.Succeeded)
                    userManager.AddToRoleAsync(user, "User").Wait();
            }
        }

        public static void SeedHotels(ApplicationDbContext db)
        {
            var Hotels = new List<Hotel>
            {
                new Hotel {Name = "Parize" , Location= "Parize , Holovna 15" , Description = "Recently, the US Federal government banned online casinos from operating in America by making it illegal to transfer money to them through any US bank or payment system. As a result of this law, most of the popular online casino networks such as Party Gaming and PlayTech left the United States. Overnight, online casino players found themselves being chased by the Federal government. But, after a fortnight, the online casino industry came up with" , Season = Season.Cold }

            };
            db.AddRange(Hotels);


            var Room = new List<Room>
            {
               new Room { RoomType = RoomType.Double }

            };
            db.AddRange(Room);


            var HotelRooms = new List<HotelRoom>
            {
                new HotelRoom { Price = 4500 , Number = 12 , MaxAdults = 9 , MaxChildren = 27 , RoomId = 1 , HotelId =1 }
            };
            db.AddRange(HotelRooms);

            var AddConv = new List<AdditionalConv>
            {
                new AdditionalConv{Name="Lanch"}
            };
            db.AddRange(AddConv);

            var RoomConv = new List<RoomConv>
            {
                new RoomConv {Price = 300, AdditionalConvId = 1 , HotelRoomId = 1 }
            };
            db.AddRange(RoomConv);

            var HotelConv = new List<HotelConv>
            {
                new HotelConv {Price = 3000 , AdditionalConvId = 1 , HotelId = 1 } 
            };
            db.AddRange(HotelConv);

            db.SaveChanges();

        }
    }
}
