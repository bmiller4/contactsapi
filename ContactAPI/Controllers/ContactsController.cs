using ContactAPI.DTO;
using ContactAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactsController : ControllerBase
    {

        private readonly IContactsService _contactsService;       

        public ContactsController(IContactsService contactsService)
        {
            _contactsService = contactsService; 
        }

        [HttpGet]
        public ActionResult<IEnumerable<ContactResponseDTO>> Get()
        {

            IEnumerable<ContactResponseDTO> contacts = _contactsService.FindAll();

            if(contacts == null)
            {
                return Ok("No Contacts Found");
            }

            return Ok(contacts);

            
        }

        [HttpGet("{id}", Name = "FindOne")]
        public ActionResult<ContactResponseDTO> Get(int id)
        {
            var result = _contactsService.FindOne(id);
            if (result != null)
                return _contactsService.FindOne(id);
            else
                return NotFound("Contact Not Found for ID: " + id);

        }

        [HttpPost]
        public ActionResult<ContactResponseDTO> Insert(ContactRequestDTO dto)
        {
            var contactResponseDTO = _contactsService.Insert(dto);
            if (contactResponseDTO != null)
                return contactResponseDTO;
            else
                return BadRequest("Request Body Malformed");
        }

        [HttpPut("{id:int}")]
        public ActionResult<ContactResponseDTO> Update(ContactRequestDTO dto, int id)
        {
            var result = _contactsService.Update(dto, id);
            if (result)
                return _contactsService.FindOne(id);
            else
                return NotFound("Contact Not Found for ID: " + id);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var result = _contactsService.Delete(id);
            if (result > 0)
                return Ok("Contact Successfully Deleted for ID: " + id);
            else
                return NotFound("Contact Not Found for ID: " + id);
        }
    }
}
