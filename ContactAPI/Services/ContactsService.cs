using ContactAPI.DTO;
using ContactAPI.Mapper;
using ContactAPI.Models;
using ContactAPI.Repos;
using System;
using System.Collections.Generic;
using System.Linq;

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
            ContactResponseDTO contactResponseDTO;
            if (contactsEntity != null)
            {
                contactResponseDTO = _contactsMapper.convertEntityToDTO(contactsEntity);

                return contactResponseDTO;
            }

            return null;            
        }

        public ContactResponseDTO Insert(ContactRequestDTO contact)
        {

            //TODO need to rethink for bad request submission -
            //is it even possible to send a bad request currently? (no integer id)
            ContactsEntity contactsEntity = _contactsMapper.convertRequestToEntity(contact);            

            var id = _contactsRepo.Insert(contactsEntity);          

            var foundInsert = _contactsRepo.FindOne(id);

            ContactResponseDTO contactResponseDTO = _contactsMapper.convertEntityToDTO(foundInsert);

            return contactResponseDTO;
           
                                             
        }

        public bool Update(ContactRequestDTO dto, int id)
        {

            var foundRecord = _contactsRepo.FindOne(id);
            if(foundRecord != null)
            {
                var entityToUpdate = _contactsMapper.convertRequestToEntity(dto);
                entityToUpdate.Id = id;
                return _contactsRepo.Update(entityToUpdate);
            }        

            return false;
        }

        public int Delete(int id)
        {
            var recordToDelete = _contactsRepo.FindOne(id);
           
            if(recordToDelete != null)
            {
                return _contactsRepo.Delete(id);
            }
            
            return id;
        }

    }
}
