using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Infrastructure.Enums;

namespace HotelsBooking.Models
{
    public class CreateHotelViewModel
    {
        public int Id { get; set; }
        public string Name{ get; set; }
        public string Location { get; set; }
        public Season Season { get; set; }
    }
}
