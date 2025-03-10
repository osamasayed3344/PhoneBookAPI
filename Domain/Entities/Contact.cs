using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entities
{
    public class Contact
    {
        public int id { set; get; }
        public string name { set; get; }
        public string phonenumber { set; get; }
        public string email { set; get; }
    }
}
