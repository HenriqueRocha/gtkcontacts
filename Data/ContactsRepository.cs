namespace ContactsSharp.Data
{
	public interface ContactsRepository
	{
		ContactList getContacts();
		void Add(Contact contact);
		void Save();
	}
}
