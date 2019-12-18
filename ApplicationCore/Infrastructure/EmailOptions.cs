using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Infrastructure
{
    public class EmailOptions
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }

        public bool EnableSsl { get; set; }
    }
}
