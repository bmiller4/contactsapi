using System.Collections.Generic;

namespace ContactAPI.DTO
{
    public class ContactRequestDTO
    {        
        public ContactNameDTO name { get; set; }
        public ContactAddressDTO address { get; set; }
        public List<ContactPhoneDTO> phone { get; set; }
        public string email { get; set; }
    }
}
