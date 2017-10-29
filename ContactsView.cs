using System;
using ContactsSharp.Data;

namespace ContactsSharp
{
	interface ContactsView
	{
		void setPresenter(ContactsPresenter presenter);
		void Init();
		void ShowContacts(ContactList contactList);
	}
}
