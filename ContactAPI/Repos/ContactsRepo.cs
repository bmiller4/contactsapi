using ContactAPI.LiteDb;
using ContactAPI.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactAPI.Repos
{
    public class ContactsRepo : IContactsRepo
    {

        private LiteDatabase _liteDb;

        public ContactsRepo(ILiteDbContext liteDbContext)
        {
            _liteDb = liteDbContext.Database;
        }
        
        public IEnumerable<ContactsEntity> FindAll()
        {
            return _liteDb.GetCollection<ContactsEntity>("Contact")
            .FindAll();
        }
        public ContactsEntity FindOne(int id)
        {
            return _liteDb.GetCollection<ContactsEntity>("Contact")
            .Find(x => x.Id == id).FirstOrDefault();
        }
        public int Insert(ContactsEntity contact)
        {
            return _liteDb.GetCollection<ContactsEntity>("Contact")
            .Insert(contact);
        }
        public bool Update(ContactsEntity contact)
        {
            return _liteDb.GetCollection<ContactsEntity>("Contact")
                .Update(contact);
        }
        public int Delete(int id)
        {
            return _liteDb.GetCollection<ContactsEntity>("Contact")
                .DeleteMany(x => x.Id == id);
        }
    }
}
