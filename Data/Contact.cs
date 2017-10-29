namespace ContactsSharp.Data
{
	public class Contact
	{
		private string fullname;
		private string email;
		private string tags;

		public Contact(string fullname, string email, string tags)
		{
			this.fullname = fullname;
			this.email = email;
			this.tags = tags;
		}

		public string Fullname
		{
			get { return fullname; }
		}

		public string Email
		{
			get { return email; }
		}

		public string Tags
		{
			get { return tags; }
		}

		public override string ToString()
		{
			return Fullname + "," + Email + "," + Tags;
		}
	}
}
