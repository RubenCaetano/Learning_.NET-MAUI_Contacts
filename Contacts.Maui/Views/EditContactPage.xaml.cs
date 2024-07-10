using Contacts.UseCases.Interfaces;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Maui.Views;

[QueryProperty(nameof(ContactId),"Id")]
public partial class EditContactPage : ContentPage
{
	private Contact contact;
    private readonly IViewContactUseCase viewContactUseCase;
    private readonly IEditContactUseCase editContactUseCase;

    public EditContactPage(IViewContactUseCase viewContactUseCase, 
						   IEditContactUseCase editContactUseCase)
	{
		InitializeComponent();
        this.viewContactUseCase = viewContactUseCase;
        this.editContactUseCase = editContactUseCase;
    }

	public string ContactId
	{
		set
		{
			contact = viewContactUseCase.ExecuteAsync(int.Parse(value)).GetAwaiter().GetResult();
			
			if (contact != null)
			{
				contactCtrl.Name = contact.Name;
				contactCtrl.Email = contact.Email;
				contactCtrl.Phone = contact.Phone;
				contactCtrl.Address = contact.Address;
			}
		}
	}

    private async void btUpdate_Clicked(object sender, EventArgs e)
    {
		contact.Name = contactCtrl.Name;
		contact.Email = contactCtrl.Email;
		contact.Phone = contactCtrl.Phone;
		contact.Address = contactCtrl.Address;

		//ContactRepository.UpdateContact(contact.ContactId, contact);
		await editContactUseCase.ExecuteAsync(contact.ContactId, contact);
        await Shell.Current.GoToAsync("..");
    }

    private void btCancel_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }

    private void contactCtrl_OnError(object sender, string e)
    {
        DisplayAlert("Error", e, "OK");
    }
}