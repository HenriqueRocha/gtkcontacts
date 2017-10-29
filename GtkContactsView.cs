using System;
using ContactsSharp.Data;
using Gtk;

namespace ContactsSharp
{
	class GtkContactsView : ContactsView
	{
		private Entry firstnameEntry;
		private Entry emailEntry;
		private Entry tagsEntry;
		private Button saveButton;
		private ListStore store;
		private ContactsPresenter presenter;
		public void setPresenter(ContactsPresenter presenter)
		{
			this.presenter = presenter;
		}

		public void Init()
		{
			SetUpGui();
			presenter.OnViewInitialized();
		}

		public void ShowContacts(ContactList contactList)
		{
			for (int i = 0; i < contactList.Size(); i++)
			{
				Contact contact = contactList.Get(i);
				store.AppendValues(contact.Fullname, contact.Email, contact.Tags);
			}
		}

		void SetUpGui()
		{
			Window w = new Window("Contacts#");
			w.DeleteEvent += Window_Delete;

			firstnameEntry = new Entry();
			firstnameEntry.Changed += Name_Changed;
			emailEntry = new Entry();
			tagsEntry = new Entry();

			VBox container = new VBox();
			w.Add(container);

			container.PackStart(createMenuBar(w), false, false, 0);

			VBox outerv = new VBox();
			outerv.BorderWidth = 12;
			outerv.Spacing = 12;
			container.PackStart(outerv, true, true, 0);

			Label l = new Label("<span weight=\"bold\" size=\"larger\">" +
				"Enter name, email and tags</span>");
			l.Xalign = 0;
			l.UseMarkup = true;
			outerv.PackStart(l, false, false, 0);

			HBox h = new HBox();
			h.Spacing = 6;
			outerv.Add(h);

			VBox v = new VBox();
			v.Spacing = 6;
			h.PackStart(v, false, false, 0);

			l = new Label("Full name: ");
			l.Xalign = 0;
			v.PackStart(l, true, false, 0);
			l.MnemonicWidget = firstnameEntry;

			l = new Label("Email address: ");
			l.Xalign = 0;
			v.PackStart(l, true, false, 0);
			l.MnemonicWidget = emailEntry;

			l = new Label("Tags: ");
			l.Xalign = 0;
			v.PackStart(l, true, true, 0);
			l.MnemonicWidget = tagsEntry;

			v = new VBox();
			v.Spacing = 6;
			h.PackStart(v, true, true, 0);

			v.PackStart(firstnameEntry, true, true, 0);
			v.PackStart(emailEntry, true, true, 0);
			v.PackStart(tagsEntry, true, true, 0);

			TreeView tv = new TreeView();
			tv.HeadersVisible = true;
			outerv.Add(tv);

			TreeViewColumn col = new TreeViewColumn();
			CellRenderer colr = new CellRendererText();
			col.Title = "Name";
			col.PackStart(colr, true);
			col.AddAttribute(colr, "text", 0);
			tv.AppendColumn(col);

			col = new TreeViewColumn();
			colr = new CellRendererText();
			col.Title = "E-mail";
			col.PackStart(colr, true);
			col.AddAttribute(colr, "text", 1);
			tv.AppendColumn(col);

			col = new TreeViewColumn();
			colr = new CellRendererText();
			col.Title = "Tags";
			col.PackStart(colr, true);
			col.AddAttribute(colr, "text", 2);
			tv.AppendColumn(col);

			store = new ListStore(typeof(string), typeof(string), typeof(string));
			tv.Model = store;

			saveButton = new Button("Save");
			saveButton.Sensitive = false;
			saveButton.Clicked += new EventHandler(Button_Clicked);
			outerv.PackStart(saveButton, false, false, 0);

			w.ShowAll();
		}

		MenuBar createMenuBar(Window w)
		{
			MenuBar mb = new MenuBar();

			AccelGroup agrp = new AccelGroup();
			w.AddAccelGroup(agrp);

			// File menu
			Menu fileMenu = new Menu();
			MenuItem item = new MenuItem("_File");
			item.Submenu = fileMenu;
			mb.Append(item);

			item = new ImageMenuItem(Stock.Quit, agrp);
			item.Activated += Quit_Activated;
			fileMenu.Append(item);

			// Help menu
			Menu helpMenu = new Menu();
			item = new MenuItem("_Help");
			item.Submenu = helpMenu;
			mb.Append(item);

			item = new MenuItem("_About");
			item.Activated += About_Activated;
			helpMenu.Append(item);

			return mb;
		}

		void About_Activated(object o, EventArgs e)
		{
			System.Console.WriteLine("About");
		}

		void Quit_Activated(object o, EventArgs e)
		{
			presenter.OnQuitActivated();
			Application.Quit();
		}

		void Window_Delete(object o, DeleteEventArgs args)
		{
			presenter.OnQuitActivated();
			Application.Quit();
			args.RetVal = true;
		}

		void Button_Clicked(object o, EventArgs args)
		{
			string name = firstnameEntry.Text;
			string email = emailEntry.Text;
			string tags = tagsEntry.Text;
			Contact contact = new Contact(name, email, tags);
			presenter.OnSaveClicked(contact);
			firstnameEntry.Text = "";
			emailEntry.Text = "";
			tagsEntry.Text = "";
			store.AppendValues(contact.Fullname, contact.Email, contact.Tags);
		}

		void Name_Changed(object o, EventArgs args)
		{
			saveButton.Sensitive = !string.IsNullOrEmpty(firstnameEntry.Text);
		}
	}
}
