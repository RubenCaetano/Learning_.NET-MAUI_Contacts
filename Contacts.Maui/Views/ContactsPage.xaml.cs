using Contacts.Maui.Models;
using Contacts.UseCases.Interfaces;
using System.Collections.ObjectModel;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Maui.Views;

public partial class ContactsPage : ContentPage
{
    public readonly IViewContactsUseCase viewContactUseCase;
    public readonly IDeleteContactUseCase deleteContactUseCase;

    public ContactsPage(IViewContactsUseCase viewContactUseCase, 
                        IDeleteContactUseCase deleteContactUseCase)
	{
		InitializeComponent();
        this.viewContactUseCase = viewContactUseCase;
        this.deleteContactUseCase = deleteContactUseCase;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        SearchBar.Text = string.Empty;
        LoadContacts();
    }

    private void btEditContact_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(EditContactPage));
    }

    private void btAddContact_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(AddContactPage));
    }

    private async void listContacts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (listContacts.SelectedItem != null)
        {
            await Shell.Current.GoToAsync($"{nameof(EditContactPage)}?Id={((Contact)listContacts.SelectedItem).ContactId}");
            listContacts.SelectedItem = null;
        }
    }

    private void btAdd_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(AddContactPage));
    }

    private async void Delete_Clicked(object sender, EventArgs e)
    {
        var menuItem = sender as MenuItem;
        var contact = menuItem.CommandParameter as Contact;
        await deleteContactUseCase.ExecuteAsync(contact.ContactId);

        LoadContacts();
    }

    private async void LoadContacts()
    {
        var contacts = new ObservableCollection<Contact>(await this.viewContactUseCase.ExecuteAsync(string.Empty));
        listContacts.ItemsSource = contacts;
    }

    private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        //var contacts = new ObservableCollection<Contact>(ContactRepository.SearchContact(((SearchBar)sender).Text));
        var contacts = new ObservableCollection<Contact>(await this.viewContactUseCase.ExecuteAsync(((SearchBar)sender).Text));
        listContacts.ItemsSource = contacts;
    }
}