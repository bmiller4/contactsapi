using ContactAPI.DTO;
using ContactAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactAPI.Mapper
{
    public class ContactsMapper : IContactsMapper
    {

        public ContactsEntity convertRequestToEntity(ContactRequestDTO contact)
        {

            List<PhoneEntity> phones = new List<PhoneEntity>();
            foreach (ContactPhoneDTO phoneDTO in contact.phone)
            {
                PhoneEntity phoneEntity = new PhoneEntity
                {
                    type = phoneDTO.type,
                    number = phoneDTO.number
                };

                phones.Add(phoneEntity);
            }

            ContactsEntity contactEntity = new ContactsEntity
            {
                first = contact.name.first,
                middle = contact.name.middle,
                last = contact.name.last,
                state = contact.address.state,
                street = contact.address.street,
                city = contact.address.city,
                zip = contact.address.zip,
                phones = phones,
                email = contact.email
            };

            return contactEntity;
        }

        public ContactResponseDTO convertEntityToDTO(ContactsEntity contactsEntity)
        {
            ContactAddressDTO address = new ContactAddressDTO
            {
                state = contactsEntity.state,
                street = contactsEntity.street,
                city = contactsEntity.city,
                zip = contactsEntity.zip
            };

            ContactNameDTO name = new ContactNameDTO
            {
                first = contactsEntity.first,
                middle = contactsEntity.middle,
                last = contactsEntity.last
            };


            List<ContactPhoneDTO> phones = new List<ContactPhoneDTO>();
            foreach (PhoneEntity phone in contactsEntity.phones)
            {
                ContactPhoneDTO phoneDTO = new ContactPhoneDTO
                {
                    type = phone.type,
                    number = phone.number
                };

                phones.Add(phoneDTO);
            }
            

            ContactResponseDTO response = new ContactResponseDTO
            {
                Id = contactsEntity.Id,
                name = name,
                address = address,
                phone = phones,
                email = contactsEntity.email
            };


            return response;
        }

    }
}
