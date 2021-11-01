using ContactAPI.DTO;
using ContactAPI.Mapper;
using ContactAPI.Models;
using ContactAPI.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactAPI.Services
{
    public class ContactsService : IContactsService
    {
        private readonly IContactsRepo _contactsRepo;
        private readonly IContactsMapper _contactsMapper;

        public ContactsService(IContactsRepo contactsRepo, IContactsMapper contactsMapper)
        {
            _contactsRepo = contactsRepo;
            _contactsMapper = contactsMapper;
        }
        
        public IEnumerable<ContactResponseDTO> FindAll()
        {
            IEnumerable<ContactsEntity> contacts = _contactsRepo.FindAll();

            if(contacts.Count() == 0)
            {
                return null;
            }

            List<ContactResponseDTO> contactResponse = new List<ContactResponseDTO>();
            foreach (ContactsEntity contact in contacts)
            {
                ContactResponseDTO contactResponseDTO = _contactsMapper.convertEntityToDTO(contact);
                contactResponse.Add(contactResponseDTO);
            }

            return contactResponse;
        }

        public ContactResponseDTO FindOne(int id)
        {
            ContactsEntity contactsEntity = _contactsRepo.FindOne(id);
            if(contactsEntity == null)
            {
                return null;
            }

            ContactResponseDTO contactResponseDTO = _contactsMapper.convertEntityToDTO(contactsEntity);            

            return contactResponseDTO;
        }

        public ContactResponseDTO Insert(ContactRequestDTO contact)
        {

            ContactsEntity contactsEntity;
            try
            {
                contactsEntity = _contactsMapper.convertRequestToEntity(contact);

                var id = _contactsRepo.Insert(contactsEntity);

                var foundInsert = _contactsRepo.FindOne(id);

                ContactResponseDTO contactResponseDTO = _contactsMapper.convertEntityToDTO(foundInsert);

                return contactResponseDTO;
            } 
            catch(Exception e)
            {
                return null;
            }
                                             
        }

        public bool Update(ContactRequestDTO contact)
        {
            ContactsEntity contactsEntity = _contactsMapper.convertRequestToEntity(contact);           

            return _contactsRepo.Update(contactsEntity);
        }

        public int Delete(int id)
        {
            int deletedId = _contactsRepo.Delete(id);
            return id;
        }

    }
}
