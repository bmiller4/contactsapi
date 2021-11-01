using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using LiteDB;
using ContactAPI.Models;
using System.IO;
using System;

namespace ContactAPI.Repos.Tests
{
    /**
     * Not True Repo Unit tests - I was having some issues mocking LiteDB dependencies.
     * 
     * This is really just testing that LiteDb is functioning as intended instead of my REPO code specifically
     * which arguably is not worth it.
     * 
     * Hindsight I could have just used a real instance of the Repo and context, but defined a different database name
     * like I've done below. This would be a true integration test as well.
     */
    [TestFixture()]
    public class ContactsRepoTests
    {        

        ContactsEntity contactsEntity;
        List<PhoneEntity> phoneEntities;

        [OneTimeSetUp]
        public void setup()
        {            

            phoneEntities = new List<PhoneEntity>();
            PhoneEntity phone = new PhoneEntity
            {
                type = "mobile",
                number = "100-100-100"
            };

            phoneEntities.Add(phone);
            contactsEntity = new ContactsEntity
            {
                Id = 1,
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
        public void FindAllTest()
        {

            string fileName = string.Empty;

            try
            {
              
                fileName = Path.GetTempFileName();

              
                FileInfo fileInfo = new FileInfo(fileName);

              
                fileInfo.Attributes = FileAttributes.Temporary;


                using (var db = new LiteDatabase(fileName))
                {

                    var col = db.GetCollection<ContactsEntity>("ContactTest");

                    col.Insert(new ContactsEntity { first = "John" });
                    col.Insert(new ContactsEntity { first = "Doe" });
                    col.Insert(new ContactsEntity { first = "Joana" });

                }
                // close datafile

                using (var db = new LiteDatabase(fileName))
                {
                    var actual = db.GetCollection<ContactsEntity>("ContactTest").Find(Query.All("Fullname", Query.Ascending));

                    Assert.AreEqual(3, actual.Count());
                    Assert.AreEqual("John", actual.First().first);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Test Failed", ex);
            }
        }

        [Test()]
        public void FindOneTest()
        {
            string fileName = string.Empty;

            try
            {

                fileName = Path.GetTempFileName();


                FileInfo fileInfo = new FileInfo(fileName);


                fileInfo.Attributes = FileAttributes.Temporary;


                using (var db = new LiteDatabase(fileName))
                {

                    var col = db.GetCollection<ContactsEntity>("ContactTest");

                    col.Insert(new ContactsEntity { Id = 1, first = "John" });                   

                }
                // close datafile

                using (var db = new LiteDatabase(fileName))
                {
                    var actual = db.GetCollection<ContactsEntity>("ContactTest").Find(x => x.Id == 1).FirstOrDefault();

                    Assert.AreEqual(contactsEntity.Id, actual.Id);                    
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Test Failed", ex);
            }
        }


        [Test()]
        public void UpdateTest()
        {
            string fileName = string.Empty;

            try
            {

                fileName = Path.GetTempFileName();


                FileInfo fileInfo = new FileInfo(fileName);


                fileInfo.Attributes = FileAttributes.Temporary;


                using (var db = new LiteDatabase(fileName))
                {

                    var col = db.GetCollection<ContactsEntity>("ContactTest");

                    col.Insert(new ContactsEntity { Id = 1, first = "John" });                    

                }
                // close datafile

                using (var db = new LiteDatabase(fileName))
                {
                    var actual = db.GetCollection<ContactsEntity>("ContactTest").Update(contactsEntity);

                    Assert.IsTrue(actual);                   
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Test Failed", ex);
            }
        }

        [Test()]
        public void DeleteTest()
        {
            string fileName = string.Empty;

            try
            {

                fileName = Path.GetTempFileName();


                FileInfo fileInfo = new FileInfo(fileName);


                fileInfo.Attributes = FileAttributes.Temporary;


                using (var db = new LiteDatabase(fileName))
                {

                    var col = db.GetCollection<ContactsEntity>("ContactTest");

                    col.Insert(new ContactsEntity { Id = 1, first = "John" });
                    col.Insert(new ContactsEntity { Id = 2, first = "Doe" });       

                }
                // close datafile

                using (var db = new LiteDatabase(fileName))
                {
                    var actual = db.GetCollection<ContactsEntity>("ContactTest").DeleteMany(x => x.Id == 2);

                    var findAll = db.GetCollection<ContactsEntity>("ContactTest").Find(Query.All("Fullname", Query.Ascending));

                    Assert.AreEqual(1, findAll.Count());                  
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Test Failed", ex);
            }
        }
    }
}