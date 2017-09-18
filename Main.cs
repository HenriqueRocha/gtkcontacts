using System;
using Gtk;

class MainClass {
	private static Entry firstnameEntry;
	private static Entry emailEntry;
	private static Entry tagsEntry;

	public static void Main (string[] args)
	{
		Application.Init();
		SetUpGui ();
		Application.Run ();
	}

	static void SetUpGui ()
	{
		Window w = new Window ("Contacts#");
		w.DeleteEvent += Window_Delete;

		firstnameEntry = new Entry();
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

		Button b = new Button ("Save");
		b.Clicked += new EventHandler (Button_Clicked);
		outerv.PackStart(b, false, false, 0);

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

	public static void About_Activated(object o, EventArgs e)
	{
		System.Console.WriteLine("About");
	}

	public static void Quit_Activated(object o, EventArgs e)
	{
		Application.Quit();
	}

	static void Window_Delete (object o, DeleteEventArgs args)
	{
		Application.Quit ();
		args.RetVal = true;
	}

	static void Button_Clicked (object o, EventArgs args)
	{
		System.Console.WriteLine ("Hello, World!");
	}
}

