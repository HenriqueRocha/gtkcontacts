using System;
using System.IO;
using System.Xml;

namespace ContactsSharp.Data
{
	public class ContactsRepositoryImpl : ContactsRepository
	{
		private string filename;
		private ContactList contactList;

		public ContactsRepositoryImpl(string filename)
		{
			this.filename = filename;
			contactList = loadContacts();
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
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(File.CreateText(filename))) {
				xmlTextWriter.Formatting = Formatting.Indented;
				xmlTextWriter.Indentation = 2;
				xmlTextWriter.IndentChar = ' ';
				xmlTextWriter.WriteStartElement("contacts");
				for (int i = 0; i < contactList.Size(); i++)
				{
					Contact c = contactList.Get(i);
					xmlTextWriter.WriteStartElement("contact");
					xmlTextWriter.WriteElementString("name", c.Fullname);
					xmlTextWriter.WriteElementString("email", c.Email);
					xmlTextWriter.WriteElementString("tags", c.Tags);
					xmlTextWriter.WriteEndElement();
				}
				xmlTextWriter.WriteEndElement();
			}
		}

		private ContactList loadContacts()
		{
			ContactList loadedContacts = new ContactList();

			if (!File.Exists(filename))
			{
				using (TextWriter writer = File.CreateText(filename)) { }
			}

			try
			{
				using (StreamReader reader = File.OpenText(filename))
				{
					string line = null;
					while ((line = reader.ReadLine()) != null)
					{
						string[] split = line.Split(',');
						Contact contact = new Contact(split[0], split[1], split[2]);
						loadedContacts.Add(contact);
					}
				}
			}
			catch (IndexOutOfRangeException e)
			{
				Console.Error.WriteLine("No filename specified.");
				Environment.Exit(1);
			}
			catch (FileNotFoundException e)
			{
				Console.Error.WriteLine("File \"{0}\" does not exist.", filename);
				Environment.Exit(2);
			}
			catch (Exception e)
			{
				Console.Error.WriteLine(e);
				Environment.Exit(3);
			}

			return loadedContacts;
		}
	}
}
