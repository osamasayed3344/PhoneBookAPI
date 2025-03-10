using System;
using System.Collections.Generic;
using System.Text;

namespace Sharded.DTOs
{
    public class ContactDTO
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string PhoneNumber { set; get; }
        public string Email { set; get; }
    }
}
