using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContactWebApplication;
using ContactWebApplication.Controllers;
using System.Linq;
using ContactWebApplication.Models;
using System;

namespace ContactWebApplication.Tests.Controllers
{
    [TestClass]
    public class ContactInformationControllerTest
    {
        [TestMethod]
        public void GetAllContacts()
        {
            // Arrange
            ContactInformationController controller = new ContactInformationController();

            // Act
            var result = controller.GetAllContacts();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod]
        public void PostContact()
        {
            // Arrange
            ContactInformationController controller = new ContactInformationController();
            var contactInfo = new ContactInformation();
            contactInfo.PersonId = Guid.NewGuid().ToString();
            contactInfo.FirstName = "abc";
            contactInfo.LastName = "bcd";
            contactInfo.PhoneNumber = "123456789";
            contactInfo.Status = true;
            contactInfo.Email = contactInfo.FirstName + "@" + contactInfo.LastName + ".com";

            // Act
            var result = controller.PostContact(contactInfo);
            var dbItem = controller.GetContact(contactInfo.PersonId);

            // Assert
            Assert.IsTrue(Match(dbItem, contactInfo));
        }

        [TestMethod]
        public void DeleteContact()
        {
            // Arrange
            ContactInformationController controller = new ContactInformationController();
            var contactInfo = new ContactInformation();
            contactInfo.PersonId = Guid.NewGuid().ToString();
            contactInfo.FirstName = "abc";
            contactInfo.LastName = "bcd";
            contactInfo.PhoneNumber = "123456789";
            contactInfo.Status = true;
            contactInfo.Email = contactInfo.FirstName + "@" + contactInfo.LastName + ".com";

            // Act
            var result = controller.PostContact(contactInfo);
            var dbItem = controller.GetContact(contactInfo.PersonId);
            controller.DeleteContact(dbItem);
            dbItem = controller.GetContact(contactInfo.PersonId);

            // Assert
            Assert.IsNull(dbItem);
        }

        [TestMethod]
        public void PutContact()
        {
            // Arrange
            ContactInformationController controller = new ContactInformationController();
            var contactInfo = new ContactInformation();
            contactInfo.PersonId = Guid.NewGuid().ToString();
            contactInfo.FirstName = "abc";
            contactInfo.LastName = "bcd";
            contactInfo.PhoneNumber = "123456789";
            contactInfo.Status = true;
            contactInfo.Email = contactInfo.FirstName + "@" + contactInfo.LastName + ".com";

            // Act
            var result = controller.PostContact(contactInfo);
            result.PhoneNumber = string.Empty;
            controller.PutContact(result.PersonId, result);
            var dbItem = controller.GetContact(result.PersonId);
            controller.DeleteContact(dbItem);

            // Assert
            Assert.IsTrue(Match(dbItem, result));
        }

        public static bool Match<T>(T actual, T expected)
        {
            if (typeof(T).IsValueType)
                return Equals(actual, expected);

            if (actual == null != (expected == null))
                return false;

            if (actual == null)
                return true;

            foreach (var property in typeof(T).GetProperties())
            {
                if (!Equals(property.GetValue(actual), property.GetValue(expected)))
                    return false;
            }

            return true;
        }
    }
}
