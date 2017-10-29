using ContactsSharp.Data;
using Gtk;

namespace ContactsSharp
{
	class MainClass
	{
		private static string FILENAME = "CONTACTS";
		private static ContactsRepository repository;
		private static GtkContactsView view;

		public static void Main(string[] args)
		{
			Application.Init();
			repository = new ContactsRepositoryImpl(FILENAME);
			view = new GtkContactsView(repository);
			Application.Run();
		}
	}
}
