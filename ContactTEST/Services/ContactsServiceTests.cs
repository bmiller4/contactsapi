using NUnit.Framework;
using System.Collections.Generic;
using ContactAPI.Repos;
using Moq;
using ContactAPI.DTO;
using ContactAPI.Mapper;
using ContactAPI.Models;

namespace ContactAPI.Services.Tests
{
    [TestFixture()]
    public class ContactsServiceTests
    {
        private Mock<IContactsRepo> _mockContactsRepo;
        private Mock<IContactsMapper> _mockContactsMapper;

        ContactResponseDTO contactResponseDTO;
        ContactAddressDTO address;
        ContactNameDTO name;
        List<ContactPhoneDTO> phones;
        ContactsEntity contactsEntity;
        List<PhoneEntity> phoneEntities;

        [OneTimeSetUp]
        public void setup()
        {
            _mockContactsRepo = new Mock<IContactsRepo>();
            _mockContactsMapper = new Mock<IContactsMapper>();

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
        public void FindAllHappyPathTest()
        {

            //arrange
            List<ContactResponseDTO> contactResponseDTOs = new List<ContactResponseDTO>();
            contactResponseDTOs.Add(contactResponseDTO);

            _mockContactsRepo.Setup(r => r.FindAll()).Returns(new List<ContactsEntity>());
            _mockContactsMapper.Setup(r => r.convertEntityToDTO(It.IsAny<ContactsEntity>())).Returns(contactResponseDTO);

            var service = new ContactsService(_mockContactsRepo.Object, _mockContactsMapper.Object);

            //act
            var actual = service.FindAll();

            //assert
            Assert.IsNotEmpty(contactResponseDTOs);
        }

        [Test()]
        public void FindAllNoneFoundTest()
        {

            //arrange
            List<ContactResponseDTO> contactResponseDTOs = new List<ContactResponseDTO>();
            contactResponseDTOs.Add(contactResponseDTO);           

            var service = new ContactsService(_mockContactsRepo.Object, _mockContactsMapper.Object);

            //act
            var actual = service.FindAll();

            //assert
            Assert.IsNull(actual);
        }

        [Test()]
        public void FindOneHappyPathTest()
        {
            //arrange            
            _mockContactsRepo.Setup(r => r.FindOne(It.IsAny<int>())).Returns(contactsEntity);
            _mockContactsMapper.Setup(r => r.convertEntityToDTO(It.IsAny<ContactsEntity>())).Returns(contactResponseDTO);

            var service = new ContactsService(_mockContactsRepo.Object, _mockContactsMapper.Object);

            //act
            var actual = service.FindOne(1);

            //assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(contactResponseDTO, actual);
        }

        [Test()]
        public void FindOneNotFoundTest()
        {
            //arrange                                
            _mockContactsRepo.Setup(r => r.FindOne(It.IsAny<int>()));
            var service = new ContactsService(_mockContactsRepo.Object, _mockContactsMapper.Object);

            //act
            var actual = service.FindOne(1);

            //assert
            Assert.IsNull(actual);           
        }

        [Test()]
        public void InsertHappyPathTest()
        {
            //arrange
            _mockContactsMapper.Setup(r => r.convertRequestToEntity(It.IsAny<ContactRequestDTO>())).Returns(contactsEntity);
            _mockContactsRepo.Setup(r => r.Insert(It.IsAny<ContactsEntity>())).Returns(1);
            _mockContactsMapper.Setup(r => r.convertEntityToDTO(It.IsAny<ContactsEntity>())).Returns(contactResponseDTO);

            var service = new ContactsService(_mockContactsRepo.Object, _mockContactsMapper.Object);

            ContactRequestDTO contactRequest = new ContactRequestDTO
            {
                name = name,
                address = address,
                phone = phones,
                email = "chani4lyfe@gmail.com"
            };

            //act
            var actual = service.Insert(contactRequest);

            //assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(contactResponseDTO, actual);
        }
        

        [Test()]
        public void UpdateHappyPathTest()
        {


            //arrange
            _mockContactsRepo.Setup(r => r.FindOne(It.IsAny<int>())).Returns(contactsEntity);
            _mockContactsRepo.Setup(r => r.Update(It.IsAny<ContactsEntity>())).Returns(true);
           

            var service = new ContactsService(_mockContactsRepo.Object, _mockContactsMapper.Object);

            ContactRequestDTO contactRequest = new ContactRequestDTO
            {
                name = name,
                address = address,
                phone = phones,
                email = "chani4lyfe@gmail.com"
            };

            //act
            var actual = service.Update(contactRequest, 1);

            //assert
            _mockContactsRepo.Verify(mock => mock.Update(contactsEntity), Times.Once());
            Assert.IsTrue(actual);           
        }

        [Test()]
        public void UpdateNotFoundTest()
        {

            //arrange                        
            var service = new ContactsService(_mockContactsRepo.Object, _mockContactsMapper.Object);
            _mockContactsRepo.Setup(r => r.FindOne(It.IsAny<int>()));

            ContactRequestDTO contactRequest = new ContactRequestDTO
            {
                name = name,
                address = address,
                phone = phones,
                email = "chani4lyfe@gmail.com"
            };

            //act
            var actual = service.Update(contactRequest, 1);

            //assert
            _mockContactsRepo.Verify(mock => mock.Update(It.IsAny<ContactsEntity>()), Times.Never());          
            Assert.IsFalse(actual);            
        }

        [Test()]
        public void DeleteHappyPathTest()
        {

            //arrange
            _mockContactsRepo.Setup(r => r.FindOne(It.IsAny<int>())).Returns(contactsEntity);
            _mockContactsRepo.Setup(r => r.Delete(It.IsAny<int>())).Returns(1);


            var service = new ContactsService(_mockContactsRepo.Object, _mockContactsMapper.Object);


            //act
            var actual = service.Delete(1);

            //assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual);
            _mockContactsRepo.Verify(mock => mock.Delete(1), Times.Once());
        }

        [Test()]
        public void DeleteNotFoundTest()
        {

            //arrange                        
            _mockContactsRepo.Setup(r => r.FindOne(It.IsAny<int>()));


            var service = new ContactsService(_mockContactsRepo.Object, _mockContactsMapper.Object);

            //act
            var actual = service.Delete(1);

            //assert
            _mockContactsRepo.Verify(mock => mock.Delete(It.IsAny<int>()), Times.Never());            
        }
    }
}