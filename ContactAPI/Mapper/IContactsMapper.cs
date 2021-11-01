using ContactAPI.DTO;
using ContactAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactAPI.Mapper
{
    public interface IContactsMapper
    {
        ContactsEntity convertRequestToEntity(ContactRequestDTO contact);
        ContactResponseDTO convertEntityToDTO(ContactsEntity contactsEntity);


    }
}
