using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ContactAPI.Services;
using ContactAPI.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ContactAPI.Controllers.Tests
{
    [TestFixture()]
    public class ContactsControllerTests
    {       
        private Mock<IContactsService> _mockContactsService;

        ContactResponseDTO contactResponseDTO;
        ContactAddressDTO address;
        ContactNameDTO name;
        List<ContactPhoneDTO> phones;
        

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
        }

        [Test()]
        public void GetAllHappyPathTest()
        {
            //arrange
            List<ContactResponseDTO> contactResponseDTOs = new List<ContactResponseDTO>();
            contactResponseDTOs.Add(contactResponseDTO);

            _mockContactsService = new Mock<IContactsService>();
            _mockContactsService.Setup(r => r.FindAll()).Returns(contactResponseDTOs);

            var controller = new ContactsController(_mockContactsService.Object);

            //act
            var actual = controller.Get();


            //assert
            Assert.IsNotEmpty(contactResponseDTOs);
        }

        [Test()]
        public void GetOneHappyPathTest()
        {           

            _mockContactsService = new Mock<IContactsService>();
            _mockContactsService.Setup(r => r.FindOne(It.IsAny<int>())).Returns(contactResponseDTO);

            var controller = new ContactsController(_mockContactsService.Object);


            //act
            var actual = controller.Get(1);


            //assert            
            Assert.IsNotNull(actual);           
            Assert.AreEqual(contactResponseDTO, actual.Value);           
        }

        [Test()]
        public void GetOneNotFoundTest()
        {
            //arrange
            _mockContactsService = new Mock<IContactsService>();            
            var controller = new ContactsController(_mockContactsService.Object);


            //act
            var actual = controller.Get(1);


            //assert
            var result = actual.Result as NotFoundObjectResult;
            Assert.IsNotNull(actual);
            Assert.AreEqual("Contact Not Found for ID: 1", result.Value);
        }

        [Test()]
        public void InsertHappyPathTest()
        {
            //arrange
            _mockContactsService = new Mock<IContactsService>();
            _mockContactsService.Setup(r => r.Insert(It.IsAny<ContactRequestDTO>())).Returns(contactResponseDTO);
            var controller = new ContactsController(_mockContactsService.Object);

            ContactRequestDTO contactRequest = new ContactRequestDTO
            {
                name = name,
                address = address,
                phone = phones,
                email = "chani4lyfe@gmail.com"
            };


            //act
            var actual = controller.Insert(contactRequest);


            //assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(contactResponseDTO, actual.Value);
        }

        [Test()]
        public void InsertBadRequestTest()
        {
            //arrange
            _mockContactsService = new Mock<IContactsService>();            
            var controller = new ContactsController(_mockContactsService.Object);

            ContactRequestDTO contactRequest = new ContactRequestDTO
            {
                name = name,
                address = address,
                phone = phones,
                email = "chani4lyfe@gmail.com"
            };


            //act
            var actual = controller.Insert(contactRequest);


            //assert
            var result = actual.Result as BadRequestObjectResult;
            Assert.IsNotNull(actual);
            Assert.AreEqual("Request Body Malformed", result.Value);
        }

        [Test()]
        public void UpdateHappyPathTest()
        {
            //arrange
            _mockContactsService = new Mock<IContactsService>();
            _mockContactsService.Setup(r => r.Update(It.IsAny<ContactRequestDTO>(), It.IsAny<int>())).Returns(true);
            _mockContactsService.Setup(r => r.FindOne(It.IsAny<int>())).Returns(contactResponseDTO);
            
            var controller = new ContactsController(_mockContactsService.Object);

            ContactRequestDTO contactRequest = new ContactRequestDTO
            {
                name = name,
                address = address,
                phone = phones,
                email = "chani4lyfe@gmail.com"
            };

            //act
            var actual = controller.Update(contactRequest, 1);


            //assert                         
            Assert.IsNotNull(actual);
            Assert.AreEqual(contactResponseDTO.name.first, actual.Value.name.first);
            _mockContactsService.Verify(mock => mock.Update(It.IsAny<ContactRequestDTO>(), It.IsAny<int>()), Times.Once());
            _mockContactsService.Verify(mock => mock.FindOne(It.IsAny<int>()), Times.Once());

        }

        [Test()]
        public void UpdateNotFoundTest()
        {
            //arrange
            _mockContactsService = new Mock<IContactsService>();
            var controller = new ContactsController(_mockContactsService.Object);

            ContactRequestDTO contactRequest = new ContactRequestDTO
            {
                name = name,
                address = address,
                phone = phones,
                email = "chani4lyfe@gmail.com"
            };

            //act
            var actual = controller.Update(contactRequest, 1);


            //assert
            var result = actual.Result as NotFoundObjectResult;           
            Assert.AreEqual("Contact Not Found for ID: 1", result.Value);
        }

        [Test()]
        public void DeleteHappyPathTest()
        {
            //arrange
            _mockContactsService = new Mock<IContactsService>();
            _mockContactsService.Setup(r => r.Delete(It.IsAny<int>())).Returns(1);

            var controller = new ContactsController(_mockContactsService.Object);


            //act
            var actual = controller.Delete(1);


            //assert            
            var result = actual as OkObjectResult;
            Assert.IsNotNull(actual);
            Assert.AreEqual("Contact Successfully Deleted for ID: 1", result.Value);
        }

        [Test()]
        public void DeleteNotFoundTest()
        {
            //arrange
            _mockContactsService = new Mock<IContactsService>();
            _mockContactsService.Setup(r => r.Delete(It.IsAny<int>())).Returns(0);

            var controller = new ContactsController(_mockContactsService.Object);


            //act
            var actual = controller.Delete(1);


            //assert            
            var result = actual as NotFoundObjectResult;
            Assert.IsNotNull(actual);
            Assert.AreEqual("Contact Not Found for ID: 1", result.Value);
        }
    }
}