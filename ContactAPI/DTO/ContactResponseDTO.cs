using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactAPI.DTO
{
    public class ContactResponseDTO : ContactRequestDTO
    {
        public int Id { get; set; }

    }
}
