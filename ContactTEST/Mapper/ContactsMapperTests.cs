using NUnit.Framework;
using ContactAPI.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactAPI.DTO;
using ContactAPI.Models;

namespace ContactAPI.Mapper.Tests
{
    [TestFixture()]
    public class ContactsMapperTests
    {

        ContactResponseDTO contactResponseDTO;
        ContactAddressDTO address;
        ContactNameDTO name;
        List<ContactPhoneDTO> phones;
        ContactsEntity contactsEntity;
        List<PhoneEntity> phoneEntities;

        [OneTimeSetUp]
        public void setup()
        {           

            //TODO looking to bogus for fake test data generation
            name = new ContactNameDTO
            {
                first = "Paul",
                middle = "Muad Dib",
                last = "Atreides"
            };

            address = new ContactAddressDTO
            {
                state = "Arrakis",
                city = "Sietch Tabr",
                street = "100 sietch lane",
                zip = "10000"
            };

            phones = new List<ContactPhoneDTO>();
            ContactPhoneDTO phoneDTO = new ContactPhoneDTO
            {
                type = "mobile",
                number = "100-100-100"
            };

            phones.Add(phoneDTO);


            contactResponseDTO = new ContactResponseDTO
            {
                Id = 1,
                name = name,
                address = address,
                phone = phones,
                email = "chani4lyfe@gmail.com"
            };

            phoneEntities = new List<PhoneEntity>();
            PhoneEntity phone = new PhoneEntity
            {
                type = "mobile",
                number = "100-100-100"
            };

            phoneEntities.Add(phone);
            contactsEntity = new ContactsEntity
            {
                first = "Paul",
                middle = "Muad Dib",
                last = "Atreides",
                state = "Arrakis",
                city = "Sietch Tabr",
                street = "100 sietch lane",
                zip = "10000",
                phones = phoneEntities,
                email = "chani4lyfe@gmail.com"
            };

        }

        [Test()]
        public void convertRequestToEntityHappyPathTest()
        {
            //arrange
            var mapper = new ContactsMapper();

            ContactRequestDTO request = new ContactRequestDTO
            {
                name = name,
                address = address,
                phone = phones,
                email = "chani4lyfe@gmail.com"
            };

            //act
            var actual = mapper.convertRequestToEntity(request);


            //assert
            Assert.AreEqual(contactsEntity.first, actual.first);
            Assert.AreEqual(contactsEntity.middle, actual.middle);
            Assert.AreEqual(contactsEntity.last, actual.last);
            Assert.AreEqual(contactsEntity.email, actual.email);
        }


        [Test()]
        public void convertDtoToEntityHappyPathTest()
        {
            //arrange
            var mapper = new ContactsMapper();

            //act
            var actual = mapper.convertEntityToDTO(contactsEntity);

            //assert
            Assert.AreEqual(contactResponseDTO.name.first, actual.name.first);
            Assert.AreEqual(contactResponseDTO.name.middle, actual.name.middle);
            Assert.AreEqual(contactResponseDTO.name.last, actual.name.last);
            Assert.AreEqual(contactResponseDTO.email, actual.email);
        }
    }
}