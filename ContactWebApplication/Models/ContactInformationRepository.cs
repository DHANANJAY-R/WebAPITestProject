using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactWebApplication.Models.Interfaces;

namespace ContactWebApplication.Models
{
    public class ContactInformationRepository : IRepository<ContactInformation>
    {
        public ContactInformation Add(ContactInformation contactInfoItem)
        {
            if (contactInfoItem == null)
            {
                throw new ArgumentNullException("item");
            }

            using (var dbContext = new ContactEntities())
            {
                dbContext.ContactInformations.Add(contactInfoItem);
                dbContext.SaveChanges();
            }

            return contactInfoItem;
        }

        public void Delete(ContactInformation entity)
        {
            using (var dbContext = new ContactEntities())
            {
                dbContext.ContactInformations.FirstOrDefault(p => p.PersonId == entity.PersonId.ToString());
                dbContext.ContactInformations.Remove(entity);
                dbContext.SaveChanges();
            }
        }

        public ContactInformation Get(object id)
        {
            using (var dbContext = new ContactEntities())
            {
                return dbContext.ContactInformations.FirstOrDefault(p => p.PersonId == id.ToString());
            }
        }

        public IEnumerable<ContactInformation> GetAll()
        {
            using (var dbContext = new ContactEntities())
            {
                return dbContext.ContactInformations.AsEnumerable();
            }
        }

        public bool Update(ContactInformation entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("item");
            }
            var itemsUpdated = 0;

            using (var dbContext = new ContactEntities())
            {
                var dbEntity = dbContext.ContactInformations.Find(entity.PersonId);
                dbContext.Entry(dbEntity).CurrentValues.SetValues(entity);
                itemsUpdated = dbContext.SaveChanges();
            }

            return itemsUpdated > 0;
        }
    }
}