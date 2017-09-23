using System;
using System.IO;
using Gtk;

class MainClass {
	private static string FILENAME = "CONTACTS";
	private static Entry firstnameEntry;
	private static Entry emailEntry;
	private static Entry tagsEntry;
	private static Button saveButton;
	private static ListStore store;

	private static ContactList contactList;

	public static void Main (string[] args) {
		ReadDataFile(args);
		Application.Init();
		SetUpGui ();
		Application.Run ();
	}

	static void ReadDataFile(string[] args) {
		try {
			contactList = new ContactList();
			using (StreamReader reader = File.OpenText(FILENAME)) {
				string line = null;
				while ((line = reader.ReadLine()) != null) {
					string[] split = line.Split(',');
					Contact contact = new Contact(
						split[0], split[1], split[2]);
					contactList.Add(contact);
				}
			} 
		} catch (IndexOutOfRangeException e) {
			Console.Error.WriteLine("No filename specified.");
			Environment.Exit(1);
		} catch (FileNotFoundException e) {
			Console.Error.WriteLine("File \"{0}\" does not exist.",
				FILENAME);
			Environment.Exit(2);
		} catch (Exception e) {
			Console.Error.WriteLine(e);
			Environment.Exit(3);
		}

	}

	static void SetUpGui () {
		Window w = new Window ("Contacts#");
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

		HBox h = new HBox ();
		h.Spacing = 6;
		outerv.Add(h);

		VBox v = new VBox();
		v.Spacing = 6;
		h.PackStart(v, false, false, 0);

		l = new Label ("Full name: ");
		l.Xalign = 0;
		v.PackStart (l, true, false, 0);
		l.MnemonicWidget = firstnameEntry;

		l = new Label ("Email address: ");
		l.Xalign = 0;
		v.PackStart (l, true, false, 0);
		l.MnemonicWidget = emailEntry;

		l = new Label("Tags: ");
		l.Xalign = 0;
		v.PackStart(l, true, true, 0);
		l.MnemonicWidget = tagsEntry;

		v = new VBox ();
		v.Spacing = 6;
		h.PackStart (v, true, true, 0);

		v.PackStart (firstnameEntry, true, true, 0);
		v.PackStart (emailEntry, true, true, 0);
		v.PackStart (tagsEntry, true, true, 0);

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

		foreach (Contact contact in contactList) {
			store.AppendValues(contact.Fullname, contact.Email, contact.Tags);
		}
		saveButton = new Button ("Save");
		saveButton.Sensitive = false;
		saveButton.Clicked += new EventHandler (Button_Clicked);
		outerv.PackStart(saveButton, false, false, 0);

		w.ShowAll ();
	}

	static MenuBar createMenuBar(Window w)
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

	static void SaveContacts() {
		using (StreamWriter writer = File.CreateText(FILENAME)) {
			foreach (Contact contact in contactList) {
				writer.WriteLine(contact);	
			}
		} 
	}

	public static void About_Activated(object o, EventArgs e) {
		System.Console.WriteLine("About");
	}

	public static void Quit_Activated(object o, EventArgs e) {
		SaveContacts();
		Application.Quit();
	}

	static void Window_Delete (object o, DeleteEventArgs args) {
		SaveContacts();
		Application.Quit ();
		args.RetVal = true;
	}

	static void Button_Clicked (object o, EventArgs args) {
		string name = firstnameEntry.Text;
		string email = emailEntry.Text;
		string tags = tagsEntry.Text;
		Contact contact = new Contact(name, email, tags);
		contactList.Add(contact);
		firstnameEntry.Text = "";
		emailEntry.Text = "";
		tagsEntry.Text = "";
		store.AppendValues(contact.Fullname, contact.Email, contact.Tags);
	}

	static void Name_Changed(object o, EventArgs args) {
		saveButton.Sensitive = !string.IsNullOrEmpty(firstnameEntry.Text);
	}
}

