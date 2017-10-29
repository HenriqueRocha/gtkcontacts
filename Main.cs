using ContactsSharp.Data;
using Gtk;

namespace ContactsSharp
{
	class MainClass
	{
		private static string FILENAME = "CONTACTS";

		public static void Main(string[] args)
		{
			Application.Init();
			ContactsRepository repository = new ContactsRepositoryImpl(FILENAME);
			ContactsView view = new GtkContactsView();
			ContactsPresenter presenter = new ContactsPresenter(view, repository);
			view.setPresenter(presenter);
			view.Init();
			Application.Run();
		}
	}
}
