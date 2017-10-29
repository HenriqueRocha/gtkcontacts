using ContactsSharp.Data;

namespace ContactsSharp
{
	class ContactsPresenter
	{
		ContactsView view;
		ContactsRepository repository;

		public ContactsPresenter(ContactsView view, ContactsRepository repository)
		{
			this.view = view;
			this.repository = repository;
		}

		public void OnViewInitialized()
		{
			ContactList contactList = repository.getContacts();
			view.ShowContacts(contactList);
		}

		public void OnSaveClicked(Contact contact)
		{
			repository.Add(contact);	
		}

		public void OnQuitActivated()
		{
			repository.Save();
		}
	}
}
