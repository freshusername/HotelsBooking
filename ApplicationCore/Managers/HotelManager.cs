using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using AutoMapper;
using Infrastructure.EF;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ApplicationCore.Services
{
    public class HotelManager : IHotelManager
    {
        protected readonly ApplicationDbContext _context;
        private readonly DbSet<Hotel> _hotels;
        private readonly IMapper _mapper;
        public HotelManager(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _hotels = context.Set<Hotel>();
        }
        //load data from relative tables(available hotel convs, etc.)
        public HotelDto Get(int hotelId)
        {
            var hotel = this.GetHotel(h => h.Id == hotelId);
            return _mapper.Map<Hotel, HotelDto>(hotel.FirstOrDefault());
        }

        private IEnumerable<Hotel> GetHotel(
            Expression<Func<Hotel, bool>> filter = null,
            Func<IQueryable<Hotel>,
            IOrderedQueryable<Hotel>> orderBy = null,
            Func<IQueryable<Hotel>, IIncludableQueryable<Hotel, object>> includeProperties = null)
        {
            IQueryable<Hotel> query = _hotels;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                query = includeProperties(query);
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public int GetHotelCount(string searchValue)
        {
            return this.GetHotel(hc => (hc.Name.Contains(searchValue) || hc.Location.Contains(searchValue))).Count();
        }

        public IEnumerable<HotelDto> GetHotels()
        {
            IEnumerable<Hotel> hotels = _hotels.ToList();
            return _mapper.Map<IEnumerable<Hotel>, IEnumerable<HotelDto>>(hotels);
        }

        public IEnumerable<HotelDto> GetHotels(int page, int countOnPage, string searchValue)
        {
            IEnumerable<Hotel> hotels = this.GetHotel(h =>
            h.Name.Contains(searchValue) || h.Location.Contains(searchValue))
                .Skip((page - 1) * countOnPage)
                .Take(countOnPage);

            return _mapper.Map<IEnumerable<Hotel>, IEnumerable<HotelDto>>(hotels);
        }

        public void Insert(HotelDto hotel)
        {
            Hotel hotel_to_add = _mapper.Map<HotelDto, Hotel>(hotel);
            _hotels.Add(hotel_to_add);
            _context.SaveChanges();
        }

        public void Update(HotelDto hotel)
        {
            Hotel hotel_to_update = _mapper.Map<HotelDto, Hotel>(hotel);
            try
            {
                _hotels.Attach(hotel_to_update);
            }
            catch { }
            finally
            {
                _hotels.Update(hotel_to_update);
            }
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            Hotel hotel_to_delete = _hotels.Find(id);
            if (_context.Entry(hotel_to_delete).State == EntityState.Detached)
            {
                _hotels.Attach(hotel_to_delete);
            }
            _hotels.Remove(hotel_to_delete);
        }
    }
}
