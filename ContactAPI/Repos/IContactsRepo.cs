using ContactAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactAPI.Repos
{
    public interface IContactsRepo
    {
        int Delete(int id);
        IEnumerable<ContactsEntity> FindAll();
        ContactsEntity FindOne(int id);
        int Insert(ContactsEntity contact);
        bool Update(ContactsEntity contact);
    }
}
