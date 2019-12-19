using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HotelsBooking.Models
{
    public class CreateOrEditOrderViewModel
    {
        public int Id { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        [StringLength(40, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(40, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 2)]
        public string LastName { get; set; }
    }
}
