using System;
using System.Collections.Generic;
using System.Text;

namespace Sharded.DTOs
{
    public class ContactRequestDTO
    {
        public string Name { set; get; }
        public string PhoneNumber { set; get; }
        public string Email { set; get; }
    }
}
