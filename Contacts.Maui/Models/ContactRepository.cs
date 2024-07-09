using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Maui.Models
{
    public static class ContactRepository
    {
        public static List<Contact> _contacts = new List<Contact>()
        {
            new Contact {ContactId = 1, Name = "Rúben Caetano", Email = "ruben@gmail.com", Phone = "912 342 345", Address = "Cidade 1"},
            new Contact {ContactId = 2, Name = "Carla Jesus", Email = "carla@gmail.com", Phone = "912 342 123", Address = "Cidade 2"},
            new Contact {ContactId = 3, Name = "Alice Caetano", Email = "alice@gmail.com", Phone = "912 342 456", Address = "Cidade 3"},
            new Contact {ContactId = 4, Name = "Fernando Caetano", Email = "fernando@gmail.com", Phone = "912 342 234", Address = "Cidade 4"},
            new Contact {ContactId = 5, Name = "Tânia Caetano", Email = "tania@gmail.com", Phone = "912 342 567", Address = "Cidade 5"},
            new Contact {ContactId = 6, Name = "Filipe Melo", Email = "filipe@gmail.com", Phone = "912 342 678", Address = "Cidade 6"}
        };

        public static List<Contact> GetContacts() => _contacts;

        public static Contact GetContactById(int contactId)
        {
            var contact = _contacts.FirstOrDefault(x => x.ContactId == contactId);

            if (contact == null) return null;

            return new Contact
            {
                ContactId = contactId,
                Address = contact.Address,
                Email = contact.Email,
                Name = contact.Name,
                Phone = contact.Phone
            };
        }

        public static void UpdateContact(int contactId, Contact contact)
        {
            if (contactId != contact.ContactId) return;

            var contactToUpdate = _contacts.FirstOrDefault(x => x.ContactId == contactId);
            if (contactToUpdate == null) return;

            contactToUpdate.Address = contact.Address;
            contactToUpdate.Email = contact.Email;
            contactToUpdate.Phone = contact.Phone;
            contactToUpdate.Name = contact.Name;
        }

        public static void AddContact(Contact contact)
        {
            int maxId = _contacts.Max(x => x.ContactId);
            contact.ContactId = maxId + 1;
            _contacts.Add(contact);
        }

        public static void DeleteContact(int contactId)
        {
            var contact = _contacts.FirstOrDefault(x => x.ContactId == contactId);
            if(contact == null) return;
            _contacts.Remove(contact);
        }

        public static List<Contact> SearchContact(string text)
        {
            var contacts = _contacts.Where(x => !string.IsNullOrWhiteSpace(x.Name) && x.Name.Contains(text, StringComparison.OrdinalIgnoreCase)).ToList();

            if (contacts == null || contacts.Count <= 0)
                contacts = _contacts.Where(x => !string.IsNullOrWhiteSpace(x.Email) && x.Email.Contains(text, StringComparison.OrdinalIgnoreCase)).ToList();
            else
                return contacts;

            if (contacts == null || contacts.Count <= 0)
                contacts = _contacts.Where(x => !string.IsNullOrWhiteSpace(x.Address) && x.Address.Contains(text, StringComparison.OrdinalIgnoreCase)).ToList();
            else
                return contacts;

            if (contacts == null || contacts.Count <= 0)
                contacts = _contacts.Where(x => !string.IsNullOrEmpty(x.Phone) && x.Phone.Contains(text, StringComparison.OrdinalIgnoreCase)).ToList();
            else
                return contacts;

            return contacts;
        }
    }
}
