using ContactAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactAPI.Services
{
    public interface IContactsService
    {
        int Delete(int id);
        IEnumerable<ContactResponseDTO> FindAll();
        ContactResponseDTO FindOne(int id);
        ContactResponseDTO Insert(ContactRequestDTO contact);
        bool Update(ContactRequestDTO contact, int id);
    }
}
