using System.Runtime.CompilerServices;

namespace Contacts.Maui.Views_MVVM.Controls;

public partial class ContactControl_MVVM : ContentView
{
	public bool IsForEdit { get; set; }
	public bool IsForAdd { get; set; }
	public ContactControl_MVVM()
	{
		InitializeComponent();
	}

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

		if (IsForAdd && !IsForEdit)
		{
			btSave.SetBinding(Button.CommandProperty, "AddContactCommand");
		}
		else if (IsForEdit && !IsForAdd)
		{
            btSave.SetBinding(Button.CommandProperty, "EditContactCommand");
        }
    }

}