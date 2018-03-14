using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ContactWebApplication.Models;
using ContactWebApplication.Models.Interfaces;

namespace ContactWebApplication.Controllers
{
    public class ContactInformationController : ApiController
    {
        static readonly IRepository<ContactInformation> repository = new ContactInformationRepository();

        [HttpGet]
        public IEnumerable<ContactInformation> GetAllContacts()
        {
            return repository.GetAll();
        }

        [HttpGet]
        public ContactInformation GetContact(string id)
        {
            var item = repository.Get(id);

            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return item;
        }

        [HttpPost]
        public ContactInformation PostContact(ContactInformation item)
        {
            item = repository.Add(item);

            return item;
            //item = repository.Add(item);

            //var response = Request.CreateResponse(HttpStatusCode.Created, item);

            //var uri = Url.Link("ContactApi", new { id = item.PersonId });

            //response.Headers.Location = new Uri(uri);

            //return response;
        }

        [HttpPut]
        public void PutContact(string id, ContactInformation contact)
        {
            contact.PersonId = id;

            if (!repository.Update(contact))
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        public void DeleteContact(ContactInformation contact)
        {
            repository.Delete(contact);
        }
    }
}
