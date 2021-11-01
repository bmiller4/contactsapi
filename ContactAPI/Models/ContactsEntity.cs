using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactAPI.Models
{
    public class ContactsEntity
    {
        public int Id { get; set; }
        public string first { get; set; }
        public string middle { get; set; }
        public string last { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public List<PhoneEntity> phones { get; set; }
        public string email { get; set; }
    }
}
