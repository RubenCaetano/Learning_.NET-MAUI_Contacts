using Contacts.Maui.ViewModels;

namespace Contacts.Maui.Views_MVVM;

public partial class ContactsPage_MVVM : ContentPage
{
    private readonly ContactsViewModel contactsViewModel;

    public ContactsPage_MVVM(ContactsViewModel contactsViewModel)
	{
		InitializeComponent();

        this.contactsViewModel = contactsViewModel;
        this.BindingContext = contactsViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await this.contactsViewModel.LoadContactAsync();
    }
}