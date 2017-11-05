using System;
using System.IO;
using System.Xml;

namespace ContactsSharp.Data
{
	public class ContactsRepositoryImpl : ContactsRepository
	{
		private const String NAME_TAG = "name";
		private const String EMAIL_TAG = "email";
		private const String TAGS_TAG = "tags";

		private string filename;
		private ContactList contactList;

		public ContactsRepositoryImpl(string filename)
		{
			this.filename = filename;

			if (!File.Exists(filename))
			{
				using (TextWriter writer = File.CreateText(filename)) { }
				contactList = new ContactList();
			}
			else
			{
				contactList = loadContacts();
			}
		}

		public ContactList getContacts()
		{
			return contactList;
		}

		public void Add(Contact contact)
		{
			contactList.Add(contact);
		}

		public void Save()
		{
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(File.CreateText(filename)))
			{
				xmlTextWriter.Formatting = Formatting.Indented;
				xmlTextWriter.Indentation = 2;
				xmlTextWriter.IndentChar = ' ';
				xmlTextWriter.WriteStartElement("contacts");
				for (int i = 0; i < contactList.Size(); i++)
				{
					Contact c = contactList.Get(i);
					xmlTextWriter.WriteStartElement("contact");
					xmlTextWriter.WriteElementString(NAME_TAG, c.Fullname);
					xmlTextWriter.WriteElementString(EMAIL_TAG, c.Email);
					xmlTextWriter.WriteElementString(TAGS_TAG, c.Tags);
					xmlTextWriter.WriteEndElement();
				}
				xmlTextWriter.WriteEndElement();
			}
		}

		private ContactList loadContacts()
		{
			ContactList loadedContacts = new ContactList();

			using (XmlTextReader reader = new XmlTextReader(File.OpenText(filename)))
			{
				loadedContacts = ReadContacts(reader);
			}

			return loadedContacts;
		}

		private ContactList ReadContacts(XmlTextReader reader)
		{
			ContactList contacts = new ContactList();
			SkipWhitespace(reader);
			reader.Read(); // <contacts>
			reader.Read(); // skip <contacts>
			while (reader.NodeType != XmlNodeType.EndElement)
			{
				contacts.Add(ReadContact(reader));
			}
			reader.Read(); // </contacts>
			SkipWhitespace(reader);
			return contacts;
		}

		private Contact ReadContact(XmlTextReader reader)
		{
			String name = null;
			String email = null;
			String tags = null;

			SkipWhitespace(reader);
			reader.Read(); // <contact>
			while (name == null || email == null || tags == null)
			{
				SkipWhitespace(reader);
				switch (reader.Name)
				{
					case NAME_TAG:
						name = ReadText(reader);
						break;
					case EMAIL_TAG:
						email = ReadText(reader);
						break;
					case TAGS_TAG:
						tags = ReadText(reader);
						break;
				}
			}
			reader.Read(); // </contact>
			SkipWhitespace(reader);

			return new Contact(name, email, tags);
		}

		private String ReadText(XmlTextReader reader)
		{
			SkipWhitespace(reader);
			reader.Read(); // skip tag
			String text = reader.Value;
			reader.Read(); // ending tag
			reader.Read(); // skip ending tag
            SkipWhitespace(reader);
			return text;
		}

		private void SkipWhitespace(XmlTextReader reader)
		{
			while (reader.NodeType == XmlNodeType.Whitespace)
			{
				reader.Read();
			}
		}
	}
}
