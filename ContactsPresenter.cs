using System;
using System.Runtime.Remoting.Messaging;
using ContactsSharp.Data;

namespace ContactsSharp
{
	class ContactsPresenter
	{
		ContactsView view;
		ContactsRepository repository;
		delegate ContactList ContactsAsyncLoader(ContactsRepository repo);

		public ContactsPresenter(ContactsView view, ContactsRepository repository)
		{
			this.view = view;
			this.repository = repository;
		}

		public void OnViewInitialized()
		{
			ContactsAsyncLoader contactsLoader = new ContactsAsyncLoader(LoadContacts);
			AsyncCallback callback = new AsyncCallback(ShowContacts);
			view.ShowContacts(new ContactList());
			contactsLoader.BeginInvoke(repository, callback, repository);
		}

		private ContactList LoadContacts(ContactsRepository repo)
		{
			return repo.getContacts();
		}

		void ShowContacts(IAsyncResult asyncResult)
		{
			ContactsAsyncLoader contactsLoader = (ContactsAsyncLoader)((AsyncResult)asyncResult).AsyncDelegate;
			view.ShowContacts(contactsLoader.EndInvoke(asyncResult));			
		}

		public void OnSaveClicked(Contact contact)
		{
			repository.Add(contact);	
			view.ShowSavedContact(contact);
		}

		public void OnQuitActivated()
		{
			repository.Save();
		}
	}
}
