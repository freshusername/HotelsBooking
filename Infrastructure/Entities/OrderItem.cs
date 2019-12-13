using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Entities
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public string OrderId { get; set; }
        public int Qty { get; set; }

        public int HotelRoomId { get; set; }
        public virtual HotelRoom HotelRoom { get; set; }
    }
}
