using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contacts.Maui.Models;
using Contacts.Maui.Views_MVVM;
using Contacts.UseCases.Interfaces;
using System.Collections.ObjectModel;
using Contact = Contacts.CoreBusiness.Contact;



namespace Contacts.Maui.ViewModels
{
    public partial class ContactViewModel : ObservableObject
    {
        private Contact contact;
        private readonly IViewContactUseCase viewContactUseCase;
        private readonly IEditContactUseCase editContactUseCase;
        private readonly IAddContactUseCase addContactUseCase;

        public Contact Contact { 
            get => contact; 
            set
            {
                SetProperty(ref contact, value);
            } 
        }


        public ContactViewModel(IViewContactUseCase viewContactUseCase,
                                IEditContactUseCase editContactUseCase,
                                IAddContactUseCase addContactUseCase)
        {
            this.Contact = new Contact();// ContactRepository.GetContactById(1);
            this.viewContactUseCase = viewContactUseCase;
            this.editContactUseCase = editContactUseCase;
            this.addContactUseCase = addContactUseCase;
        }

        public async Task LoadContact(int contactId)
        {
            this.Contact = await this.viewContactUseCase.ExecuteAsync(contactId);
        }

        [RelayCommand]
        public async Task EditContact()
        {
            await this.editContactUseCase.ExecuteAsync(this.Contact.ContactId, this.Contact);
            await Shell.Current.GoToAsync($"{nameof(ContactsPage_MVVM)}");
        }

        [RelayCommand]
        public async Task AddContact()
        {
            await this.addContactUseCase.ExecuteAsync(this.Contact);
            await Shell.Current.GoToAsync($"{nameof(ContactsPage_MVVM)}");
        }


        [RelayCommand]
        public async Task BackToContacts()
        {
            await Shell.Current.GoToAsync($"{nameof(ContactsPage_MVVM)}");
        }
    }
}
