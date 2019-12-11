using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string FullName { get; set; }
        public string  Email { get; set; }
        public string PhoneNumber { get; set; }

        public string AppUserId { get; set; }
        public virtual AppUser User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }

}
