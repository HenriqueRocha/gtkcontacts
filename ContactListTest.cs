using NUnit.Framework;

[TestFixture]
public class ContactListTest
{
	[Test]
	public void AddAddsContact()
	{
		ContactList contactList = new ContactList();

		var contact = new Contact("Foo Bar", "foo@foobar.com", "");
		contactList.Add(contact);

		Assert.AreEqual(contact, contactList.Get(0));
	}

	[Test]
	public void NewListIsEmpty()
	{
		ContactList contactList = new ContactList();

		Assert.True(contactList.IsEmpty());
	}

	[Test]
	public void ListWithOneItemHasSizeOne()
	{
		ContactList contactList = new ContactList();

		var contact = new Contact("Foo Bar", "foo@foobar.com", "");
		contactList.Add(contact);

		Assert.AreEqual(1, contactList.Size());
	}

	[Test]
	public void NewListHasSizeZero()
	{
		ContactList contactList = new ContactList();

		Assert.AreEqual(0, contactList.Size());
	}

	[Test]
	public void GetGetsTheCorrectElement()
	{
		ContactList contactList = new ContactList();

		Contact contact1 = new Contact("Alice", "alice@example.com", "");
		Contact contact2 = new Contact("Bob", "bob@example.com", "");
		Contact contact3 = new Contact("Charles", "charles@example.com", "");

		contactList.Add(contact1);
		contactList.Add(contact2);
		contactList.Add(contact3);

		Assert.AreEqual(contact1, contactList.Get(0));
		Assert.AreEqual(contact2, contactList.Get(1));
		Assert.AreEqual(contact3, contactList.Get(2));
	}

}
