using Contacts.UseCases.PluginInterfaces;
using static System.Net.Mime.MediaTypeNames;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Plugins.DataStore.InMemory
{
    // All the code in this file is included in all platforms.
    public class ContactInMemoryRepository : IContactRepository
    {
        public static List<Contact> _contacts;

        public ContactInMemoryRepository() 
        {
            _contacts = new List<Contact>()
            {
                new Contact {ContactId = 1, Name = "Rúben Caetano", Email = "ruben@gmail.com", Phone = "912 342 345", Address = "Cidade 1"},
                new Contact {ContactId = 2, Name = "Carla Jesus", Email = "carla@gmail.com", Phone = "912 342 123", Address = "Cidade 2"},
                new Contact {ContactId = 3, Name = "Alice Caetano", Email = "alice@gmail.com", Phone = "912 342 456", Address = "Cidade 3"},
                new Contact {ContactId = 4, Name = "Fernando Caetano", Email = "fernando@gmail.com", Phone = "912 342 234", Address = "Cidade 4"},
                new Contact {ContactId = 5, Name = "Tânia Caetano", Email = "tania@gmail.com", Phone = "912 342 567", Address = "Cidade 5"},
                new Contact {ContactId = 6, Name = "Filipe Melo", Email = "filipe@gmail.com", Phone = "912 342 678", Address = "Cidade 6"}
            };
        }

        public Task AddContactAsync(CoreBusiness.Contact contact)
        {
            int maxId = _contacts.Max(x => x.ContactId);
            contact.ContactId = maxId + 1;
            _contacts.Add(contact);

            return Task.CompletedTask;
        }

        public Task DeleteContactAsync(int contactId)
        {
            var contact = _contacts.FirstOrDefault(x => x.ContactId == contactId);
            if (contact != null) 
            { 
                _contacts.Remove(contact); 
            }

            return Task.CompletedTask;
        }

        public Task<Contact> GetContactByIdAsync(int contactId)
        {
            var contact = _contacts.FirstOrDefault(x => x.ContactId == contactId);

            if (contact == null) return null;

            return Task.FromResult(new Contact
            {
                ContactId = contactId,
                Address = contact.Address,
                Email = contact.Email,
                Name = contact.Name,
                Phone = contact.Phone
            });
        }

        public Task<List<Contact>> GetContactsAsync(string filterText)
        {
            if (string.IsNullOrWhiteSpace(filterText))
            {
                return Task.FromResult(_contacts);
            }

            var contacts = _contacts.Where(x => !string.IsNullOrWhiteSpace(x.Name) && x.Name.Contains(filterText, StringComparison.OrdinalIgnoreCase)).ToList();

            if (contacts == null || contacts.Count <= 0)
                contacts = _contacts.Where(x => !string.IsNullOrWhiteSpace(x.Email) && x.Email.Contains(filterText, StringComparison.OrdinalIgnoreCase)).ToList();
            else
                return Task.FromResult(contacts);

            if (contacts == null || contacts.Count <= 0)
                contacts = _contacts.Where(x => !string.IsNullOrWhiteSpace(x.Address) && x.Address.Contains(filterText, StringComparison.OrdinalIgnoreCase)).ToList();
            else
                return Task.FromResult(contacts);

            if (contacts == null || contacts.Count <= 0)
                contacts = _contacts.Where(x => !string.IsNullOrEmpty(x.Phone) && x.Phone.Contains(filterText, StringComparison.OrdinalIgnoreCase)).ToList();
            else
                return Task.FromResult(contacts);

            return Task.FromResult(contacts);
        }

        public Task UpdateContactAsync(int contactId, Contact contact)
        {
            if (contactId != contact.ContactId) return Task.CompletedTask;

            var contactToUpdate = _contacts.FirstOrDefault(x => x.ContactId == contactId);
            if (contactToUpdate != null)
            {
                contactToUpdate.Address = contact.Address;
                contactToUpdate.Email = contact.Email;
                contactToUpdate.Phone = contact.Phone;
                contactToUpdate.Name = contact.Name;
            }

            return Task.CompletedTask;
        }
    }
}