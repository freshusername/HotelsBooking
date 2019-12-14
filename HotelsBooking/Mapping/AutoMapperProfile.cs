using ApplicationCore.DTOs;
using HotelsBooking.Models;
using HotelsBooking.Models.AppProfile;
using Infrastructure.Entities;
using AutoMapper;

namespace HotelsBooking.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppUser,EditUserViewModel>();
            CreateMap<AppUser, ChangePasswordViewModel>();
            CreateMap<EditUserViewModel, UserDTO>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email)); ;
            CreateMap<ChangePasswordViewModel, UserDTO>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email)); ;

            CreateMap<AppUser, AdminUserDTO>();
            CreateMap<AdminUserDTO, UsersViewModel>();

            CreateMap<AdditionalConvDTO, AdditionalConv>().ReverseMap();

            CreateMap<HotelConvDTO,HotelConvsViewModel>();

            CreateMap<CreateOrEditHotelViewModel, HotelDTO>().ReverseMap();
            CreateMap<HotelDTO, Hotel>().ReverseMap();
            
         
            CreateMap<RegisterViewModel, UserDTO>()
                .ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
            
            CreateMap<UserDTO, AppUser>()
                .ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email)).ReverseMap();

            CreateMap<LoginViewModel, UserDTO>()
                .ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));

            CreateMap<OrderDetail, OrderDetailDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();

            CreateMap<AppUser, ProfileDTO>().ReverseMap();
            CreateMap<ProfileDTO, AllProfilesViewModel>().ReverseMap();
            CreateMap<ProfileDTO, ProfileViewModel>().ReverseMap();

            CreateMap<OrderDTO, OrdersViewModel>();

            CreateMap<OrderDetailDTO, OrderDetailsViewModel>();
            CreateMap<OrderDTO, OrdersViewModel>();

            CreateMap<CreateOrEditOrderViewModel, OrderDTO>();
            CreateMap<OrderDTO, CreateOrEditOrderViewModel>();
            CreateMap<CreateOrEditOrderDetailsViewModel, OrderDetailDTO>();

            CreateMap<CreateAndEditHotelConvViewModel, HotelConvDTO>().ReverseMap();
            CreateMap<CreateOrEditHotelConvViewModel, HotelConvDTO>().ReverseMap();

            CreateMap<HotelConv, HotelConvDTO>()
                .ForMember(hcd => hcd.Name, map =>map.MapFrom(hc => hc.AdditionalConv.Name)).ReverseMap();
            CreateMap<AdditionalConv, AdditionalConvDTO>().ReverseMap();

            CreateMap<HotelRoomDTO, HotelRoomsViewModel>().ReverseMap();
            CreateMap<CreateOrEditHotelRoomViewModel, HotelRoomDTO>().ReverseMap();
            CreateMap<HotelRoom, HotelRoomDTO>().ForMember(hrd => hrd.Type, map=>map.MapFrom(hr => hr.Room.RoomType)).ReverseMap();
        }
    }
}

