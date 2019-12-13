using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.DTOs
{
    public class PagingDto
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Amount { get; set; }
    }
}
